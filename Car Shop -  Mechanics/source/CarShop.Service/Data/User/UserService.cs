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
    using CarShop.Service.Common.Extensions.Reflection;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class UserService : BaseService, IUserService
    {
        public UserService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<Paginate<UserResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var users = UserQueries.GetAllUserResponse(this.db.Users).AsQueryable();

            var payload = await Paginate<UserResponseModel>.ToPaginatedCollection(users, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<UserResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users));

            return response;
        }

        public async Task<Response<UserResponseModel>> GetByIdAsync(long id)
        {
            var response = new Response<UserResponseModel>();

            var user = await UserQueries.UserByIdAsync(id, this.db);

            response.Payload = user;

            EntityValidator.ValidateForNull(response, ResponseMessages.Entity_Get_Succeed, Constants.User);

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();
            var user = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id);

            EntityValidator.ValidateForNull(user, response, ResponseMessages.Entity_Delete_Succeed, Constants.User);

            if (response.IsSuccess)
            {
                this.db.Users.Remove(user);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<long> GetCountAsync()
        {
            var userCount = await this.db.Users.CountAsync();

            return userCount;
        }

        public async Task<InfoResponse> BlockAsync(long userId)
        {
            var user = await this.db.Users.Where(user => user.Id == userId)
                                          .Select(user => new User 
                                          {
                                              Id = user.Id,
                                              Roles = user.Roles
                                          })
                                          .FirstOrDefaultAsync();

            var response = new InfoResponse();

            //Does the user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.User_Block_Succeed, Constants.User);

            //If it exists does it have any roles
            if (response.IsSuccess && !user.Roles.Any())
            {
                ResponseSetter.SetResponse(response, false, ExceptionMessages.No_Roles);
            }

            //Check is already blocked
            if (response.IsSuccess && user.Roles.Any(role => role.RoleId == Constants.Blocked_Id && role.UserId == user.Id))
            {
                ResponseSetter.SetResponse(response, false, ExceptionMessages.Already_Blocked);
            }

            //Check is there previous block so can undelete it
            var userRoleWithoutFilter = await this.db.UserRoles
                .IgnoreQueryFilters()
                .Where(role => role.RoleId == Constants.Blocked_Id && role.UserId == userId)
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
                .Where(user => user.Id == userId)
                .Select(user => new User
                {
                    Id = user.Id,
                    Roles = user.Roles
                })
                .FirstOrDefaultAsync();

            var response = new InfoResponse();

            //Checks does user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.User_UnBlock_Succeed, Constants.User);

            //Have any roles
            bool doesHaveRoles = response.IsSuccess && !user.Roles.Any();

            if (doesHaveRoles)
            {
                ResponseSetter.SetResponse(response, false, ExceptionMessages.No_Roles);
            }

            //Check is blocked
            bool isBlocked = response.IsSuccess && !user.Roles.Any(role => role.RoleId == Constants.Blocked_Id);
            if (isBlocked)
            {
                ResponseSetter.SetResponse(response, false, ExceptionMessages.User_Is_Not_Blocked);
            }

            //If all states are valid unblock user and return old roles
            if (response.IsSuccess)
            {
                var userRemovedRoles = await this.db.UserRoles
                    .IgnoreQueryFilters()
                    .Where(userRole => userRole.UserId == userId && userRole.IsDeleted == true)
                    .ToListAsync();

                foreach (var role in userRemovedRoles)
                {
                    this.db.Undelete(role);
                }

                await this.db.SaveChangesAsync();

                var userRoleToRemove = await this.db.UserRoles
                   .Where(role => role.RoleId == Constants.Blocked_Id && role.UserId == userId)
                   .FirstOrDefaultAsync();

                this.db.UserRoles.Remove(userRoleToRemove);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> RemoveRole(long userId, long roleId)
        {
            var response = new InfoResponse();

            var userRole = await this.db.UserRoles.FirstOrDefaultAsync(userRole => userRole.UserId == userId && userRole.RoleId == roleId);

            EntityValidator.ValidateForNull(userRole, response, ResponseMessages.Entity_Delete_Succeed, Constants.Role);

            if (response.IsSuccess)
            {
                this.db.UserRoles.Remove(userRole);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<UserResponseModel>>> SearchByAsync(UserSearchAndSortRequestModel requestModel)
        {
            var query = this.db.Users.AsQueryable();

            query = UserQueries.Filter(requestModel, query);

            var response = new Response<Paginate<UserResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users));

            var IsSortingNeeded = ClassScanner.IsThereAnyTrueProperty(requestModel);

            if (IsSortingNeeded)
            {
                var sortByResponse = await this.SortByAsync(Mapper.ToRequest(requestModel), query);

                var sb = new StringBuilder();
                sb.AppendLine(response.Message);
                sb.AppendLine(sortByResponse.Message);

                response.Message = sb.ToString();

                response.Payload = sortByResponse.Payload;
            }
            else
            {
                var filtered = UserQueries.GetAllUserResponse(query);

                var payload = await Paginate<UserResponseModel>.ToPaginatedCollection(filtered, requestModel.Page, requestModel.PerPage);

                response.Payload = payload;
            }

            return response;
        }

        public async Task<Response<Paginate<UserResponseModel>>> SortByAsync(UserSortRequestModel model, IQueryable<User> query = null)
        {
            IQueryable<User> result;

            if (query != null)
            {
                result = query;
            }
            else
            {
                result = this.db.Users.AsQueryable();
            }

            var sortedQuery = UserQueries.Sort(model, query);

            var userResponses = UserQueries.GetAllUserResponse(sortedQuery);

            var payload = await Paginate<UserResponseModel>.ToPaginatedCollection(userResponses, model.Page, model.PerPage);

            var response = new Response<Paginate<UserResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users));

            return response;
        }

        public async Task<InfoResponse> RegisterRoleAsync(long userId, long roleId)
        {
            var response = new InfoResponse();
            var user = await this.db.Users.FirstOrDefaultAsync(user => user.Id == userId);

            //Validates does the user exist
            EntityValidator.ValidateForNull(user, response, ResponseMessages.Role_Register_Succeed, Constants.Role);

            //Search for role without query filter so if there is
            //option to revive role instead of creating new
            var roleWithoutFilter = await this.db.UserRoles
                .IgnoreQueryFilters()
                .Where(role => role.UserId == userId && role.RoleId == roleId)
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
            foreach (var role in user.Roles)
            {
                this.db.UserRoles.Remove(role);
            }

            await this.db.SaveChangesAsync();
        }
    }
}