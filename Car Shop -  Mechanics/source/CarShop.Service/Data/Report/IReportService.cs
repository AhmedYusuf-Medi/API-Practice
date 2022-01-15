namespace CarShop.Service.Data.Report
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IReportService
    {
        public Task<InfoResponse> CreateAsync(ReportCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<ReportResponseModel>>> FilterByAsync(ReportFilterAndSortRequestModel requestModel);
        public Task<Response<Paginate<ReportResponseModel>>> GetAllAsync(PaginationRequestModel request);
        public Task<Response<ReportResponseModel>> GetByIdAsync(long reportId);
        public Task<Response<Paginate<ReportResponseModel>>> SortByAsync(ReportSortRequestModel requestModel, IQueryable<Models.Base.Report> reports = null);
    }
}