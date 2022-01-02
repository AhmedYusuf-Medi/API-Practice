using CarShop.Models.Pagination;
using CarShop.Models.Request.User;
using CarShop.Models.Response;
using CarShop.Models.Response.User;
using CarShop.Service.Common.Extensions.Pager;
using System.Threading.Tasks;

namespace CarShop.Service.Data.User
{
    public interface IUserService
    {
        Task<InfoResponse> BlockAsync(long id);
        Task<InfoResponse> ChangeRole(long userId, long roleId);
        Task<InfoResponse> DeleteAsync(long id);
        Task<Response<Paginate<UserResponseModel>>> GetAllAsync(PaginationRequestModel request);
        Task<Response<UserResponseModel>> GetByIdAsync(long id);
        Task<long> GetCountAsync();
        Task<Response<Paginate<UserResponseModel>>> SearchByAsync(UserSearchRequestModel model);
        Task<InfoResponse> UnBlockAsync(long id);
    }
}