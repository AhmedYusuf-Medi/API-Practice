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

        public async Task<InfoResponse> BlockAsync(long id)
        {
            var responce = await this.RegisterRoleAsync(id, Constants.Blocked_Id, ResponseMessages.User_Block_Succeed);

            return responce;
        }

        public async Task<InfoResponse> UnBlockAsync(long id)
        {
            var responce = await this.RegisterRoleAsync(id, Constants.User_Id, ResponseMessages.User_UnBlock_Succeed);

            return responce;
        }

        public async Task<InfoResponse> ChangeRole(long userId, long roleId)
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

        private async Task<InfoResponse> RegisterRoleAsync(long id, int roleId, string msg)
        {
            var response = new InfoResponse();
            var forBlock = await this.db.Users.FirstOrDefaultAsync(u => u.Id == id);

            EntityValidator.ValidateForNull(forBlock, response, msg, Constants.User);

            if (response.IsSuccess)
            {
                var userRole = new UserRole
                {
                    RoleId = roleId,
                    UserId = id
                };

                await this.db.UserRoles.AddAsync(userRole);
                await this.db.SaveChangesAsync();
            }

            return response;
        }
    }
}