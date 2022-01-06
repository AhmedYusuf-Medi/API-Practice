using CarShop.Models.Pagination;
using CarShop.Models.Request.Vehicle;
using CarShop.Models.Response;
using CarShop.Models.Response.Vehicle;
using CarShop.Service.Common.Extensions.Pager;
using System.Threading.Tasks;

namespace CarShop.Service.Data.Vehicle
{
    public interface IVehicleService
    {
        Task<InfoResponse> CreateAsync(VehicleCreateRequestModel requestModel);
        Task<InfoResponse> DeleteAsync(long id);
        Task<Response<Paginate<VehicleResponseModel>>> GetAllAsync(PaginationRequestModel request);
        Task<Response<VehicleResponseModel>> GetByIdAsync(long id);
        Task<InfoResponse> UpdateAsync(long id, VehicleUpdateRequestModel requestModel);
    }
}