namespace CarShop.Service.Data.VehicleBrand
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleBrand;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Threading.Tasks;

    public interface IVehicleBrandService
    {
        public Task<InfoResponse> CreateAsync(VehicleBrandCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<VehicleBrandResponseModel>>> GetAllAsync(PaginationRequestModel requestModel);
        public Task<Response<VehicleBrandResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<VehicleBrandResponseModel>>> SortByAsync(VehicleBrandSortRequestModel requestModel);
        public Task<InfoResponse> UpdateAsync(long id, VehicleBrandCreateRequestModel requestModel);
    }
}