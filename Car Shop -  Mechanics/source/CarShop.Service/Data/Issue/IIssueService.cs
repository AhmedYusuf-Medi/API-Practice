namespace CarShop.Service.Data.Issue
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IIssueService
    {
        public Task<InfoResponse> ChangeStatusAsync(long issueId, long statusId);
        public Task<InfoResponse> CreateAsync(IssueCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<IssueResponseModel>>> FilterByAsync(IssueFilterRequestModel requestModel);
        public Task<Response<Paginate<IssueResponseModel>>> GetAllAsync(PaginationRequestModel request);
        public Task<Response<IssueResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<IssueResponseModel>>> SortByAsync(IssueSortRequestModel requestModel, IQueryable<Models.Base.Issue> issues = null);
        public Task<InfoResponse> UpdateAsync(long id, IssueUpdateRequestModel requestModel);
    }
}