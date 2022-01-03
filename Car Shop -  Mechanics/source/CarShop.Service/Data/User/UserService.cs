namespace CarShop.Service.Data.User
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Messages;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : BaseService, IUserService
    {
        public UserService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<Paginate<UserResponseModel>>> GetAllAsync(PaginationRequestModel request)
        {
            var result = UserQueries.GetAllUserResponse(this.db.Users).AsQueryable();

            var payload = await Paginate<UserResponseModel>.ToPaginatedCollection(result, request.Page, request.PerPage);

            var response = new Response<Paginate<UserResponseModel>>();
            response.Payload = payload;
            response.IsSuccess = true;
            response.Message = string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users);

            return response;
        }

        public async Task<Response<UserResponseModel>> GetByIdAsync(long id)
        {
            var response = new Response<UserResponseModel>();

            var result = await UserQueries.UserByIdAsync(id, this.db);

            response.Payload = result;

            EntityValidator.ValidateForNull(response, ResponseMessages.Entity_Get_Succeed, Constants.User);

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();
            var forDelete = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id);

            EntityValidator.ValidateForNull(forDelete, response, ResponseMessages.Entity_Delete_Succeed, Constants.User);

            if (response.IsSuccess)
            {
                this.db.Users.Remove(forDelete);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<long> GetCountAsync()
        {
            var result = await this.db.Users.CountAsync();

            return result;
        }

        public async Task<InfoResponse> BlockAsync(long userId)
        {
            var user = await this.db.Users.Where(u => u.Id == userId)
                                          .Select(u => new User 
                                          {
                                              Id = u.Id,
                                              Roles = u.Roles
                                          })
                                          .FirstOrDefaultAsync();

            var response = new InfoResponse();

            //Does the user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.User_Block_Succeed, Constants.User);

            //If it exists does it have any roles
            if (response.IsSuccess && !user.Roles.Any())
            {
                response.Message = ExceptionMessages.No_Roles;
                response.IsSuccess = false;
            }

            //Check is already blocked
            if (response.IsSuccess && user.Roles.Any(r => r.RoleId == Constants.Blocked_Id && r.UserId == user.Id))
            {
                response.Message = ExceptionMessages.Already_Blocked;
                response.IsSuccess = false;
            }

            //Check is there previous block so can undelete it
            var userRoleWithoutFilter = await this.db.UserRoles
                .IgnoreQueryFilters()
                .Where(r => r.RoleId == Constants.Blocked_Id && r.UserId == userId)
                .FirstOrDefaultAsync();

            //If its possible instead of creating new user role we revive
            if (response.IsSuccess && userRoleWithoutFilter != null)
            {
                await this.RemoveAllRoles(user);

                this.db.Undelete(userRoleWithoutFilter);
                await this.db.SaveChangesAsync();

                return response;
            }

            //If there is no previous block and other states are valid 
            //user status wents to blocked and all other roles are removed
            if (response.IsSuccess)
            {
                await this.RemoveAllRoles(user);

                var userRole = new UserRole
                {
                    UserId = user.Id,
                    RoleId = Constants.Blocked_Id
                };

                await this.db.UserRoles.AddAsync(userRole);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> UnBlockAsync(long userId)
        {
            var user = await this.db.Users
                .Where(u => u.Id == userId)
                .Select(u => new User
                {
                    Id = u.Id,
                    Roles = u.Roles
                })
                .FirstOrDefaultAsync();

            var response = new InfoResponse();

            //Checks does user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.User_UnBlock_Succeed, Constants.User);

            //Have any roles
            bool doesHaveRoles = response.IsSuccess && !user.Roles.Any();

            if (doesHaveRoles)
            {
                response.Message = ExceptionMessages.No_Roles;
                response.IsSuccess = false;
            }

            //Check is blocked
            bool isBlocked = response.IsSuccess && !user.Roles.Any(r => r.RoleId == Constants.Blocked_Id);
            if (isBlocked)
            {
                response.Message = "Choosen user is not even blocked!";
                response.IsSuccess = false;
            }

            //If all states are valid unblock user and return old roles
            if (response.IsSuccess)
            {
                var userRemovedRoles = await this.db.UserRoles
                    .IgnoreQueryFilters()
                    .Where(ur => ur.UserId == userId && ur.IsDeleted == true)
                    .ToListAsync();

                foreach (var role in userRemovedRoles)
                {
                    this.db.Undelete(role);
                }

                await this.db.SaveChangesAsync();

                var userRoleToRemove = await this.db.UserRoles
                   .Where(r => r.RoleId == Constants.Blocked_Id && r.UserId == userId)
                   .FirstOrDefaultAsync();

                this.db.UserRoles.Remove(userRoleToRemove);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> RemoveRole(long userId, long roleId)
        {
            var response = new InfoResponse();

            var role = await this.db.UserRoles.FirstOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            EntityValidator.ValidateForNull(role, response, ResponseMessages.Entity_Delete_Succeed, Constants.Role);

            if (response.IsSuccess)
            {
                this.db.UserRoles.Remove(role);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<UserResponseModel>>> SearchByAsync(UserSearchRequestModel model)
        {
            var query = this.db.Users.AsQueryable();

            if (!string.IsNullOrEmpty(model.Username))
            {
                query = query.Where(u => u.Username.Contains(model.Username));
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                query = query.Where(u => u.Email.Contains(model.Email));
            }

            var filtered = UserQueries.GetAllUserResponse(query);

            var payload = await Paginate<UserResponseModel>.ToPaginatedCollection(filtered, model.Page, model.PerPage);

            var response = new Response<Paginate<UserResponseModel>>();
            response.Payload = payload;
            response.IsSuccess = true;
            response.Message = string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users);

            return response;
        }

        public async Task<InfoResponse> RegisterRoleAsync(long userId, long roleId)
        {
            var response = new InfoResponse();
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == userId);

            //Validates does the user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.Role_Register, Constants.Role);

            //Search for role without query filter so if there is
            //option to revive role instead of creating new
            var roleWithoutFilter = await this.db.UserRoles
                .IgnoreQueryFilters()
                .Where(r => r.UserId == userId && r.RoleId == roleId)
                .FirstOrDefaultAsync();

            //If there is user role deleted it is gonna undelete it
            if (response.IsSuccess && roleWithoutFilter != null)
            {
                this.db.Undelete(roleWithoutFilter);
                await this.db.SaveChangesAsync();

                return response;
            }

            //if there is no already existing user role and user exist
            //creates new user role
            if (response.IsSuccess)
            {
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = userId
                };

                await this.db.UserRoles.AddAsync(userRole);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        private async Task RemoveAllRoles(User user)
        {
            //UserRole userRole;

            //var rolesToRemove = new HashSet<UserRole>();

            //foreach (var role in user.Roles)
            //{
            //    userRole = new UserRole
            //    {
            //        UserId = role.UserId,
            //        RoleId = role.RoleId
            //    };

            //    rolesToRemove.Add(userRole);
            //}

            foreach (var role in user.Roles)
            {
                this.db.UserRoles.Remove(role);
            }

            await this.db.SaveChangesAsync();
        }
    }
}