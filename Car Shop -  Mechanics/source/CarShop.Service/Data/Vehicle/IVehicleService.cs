namespace CarShop.Service.Data.Vehicle
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVehicleService
    {
        public Task<InfoResponse> CreateAsync(VehicleCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<VehicleResponseModel>>> GetAllAsync(PaginationRequestModel request);
        public Task<Response<VehicleResponseModel>> GetByIdAsync(long id);
        public Task<InfoResponse> UpdateAsync(long id, VehicleUpdateRequestModel requestModel);
        public Task<Response<Paginate<VehicleResponseModel>>> FilterByAsync(VehicleFilterAndSortRequestModel requestModel);
        public Task<Response<Paginate<VehicleResponseModel>>> SortByAsync(VehicleSortRequestModel requestModel, IQueryable<Models.Base.Vehicle> vehicles = null);
    }
}