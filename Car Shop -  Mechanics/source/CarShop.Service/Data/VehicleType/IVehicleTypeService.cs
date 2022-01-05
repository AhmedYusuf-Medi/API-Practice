namespace CarShop.Service.Data.VehicleType
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleType;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Threading.Tasks;

    public interface IVehicleTypeService
    {
        public Task<InfoResponse> CreateAsync(VehicleTypeCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<VehicleTypeResponseModel>>> GetAllAsync(PaginationRequestModel requestModel);
        public Task<Response<VehicleTypeResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<VehicleTypeResponseModel>>> SortByAsync(VehicleTypeSortRequestModel requestModel);
        public Task<InfoResponse> UpdateAsync(long id, VehicleTypeCreateRequestModel requestModel);
    }
}