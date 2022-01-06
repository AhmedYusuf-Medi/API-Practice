namespace CarShop.Service.Data.IssueStatus
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Threading.Tasks;

    public interface IIssueStatusService
    {
        public Task<InfoResponse> CreateAsync(IssueStatusCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<IssueStatusResponseModel>>> GetAllAsync(PaginationRequestModel requestModel);
        public Task<Response<IssueStatusResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<IssueStatusResponseModel>>> SortByAsync(IssueStatusSortRequestModel requestModel);
        public Task<InfoResponse> UpdateAsync(long id, IssueStatusCreateRequestModel requestModel);
    }
}