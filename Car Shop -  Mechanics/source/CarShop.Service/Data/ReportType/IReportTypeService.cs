namespace CarShop.Service.Data.ReportType
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.ReportType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.ReportType;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Threading.Tasks;

    public interface IReportTypeService
    {
        public Task<InfoResponse> CreateAsync(ReportTypeCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<ReportTypeResponseModel>>> GetAllAsync(PaginationRequestModel requestModel);
        public Task<Response<ReportTypeResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<ReportTypeResponseModel>>> SortByAsync(ReportTypeSortRequestModel requestModel);
        public Task<InfoResponse> UpdateAsync(long id, ReportTypeUpdateRequestModel requestModel);
    }
}