namespace CarShop.Service.Data.IssuePriority
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssuePriority;
    using CarShop.Service.Common.Extensions.Pager;
    using System.Threading.Tasks;

    public interface IIssuePriorityService
    {
        public Task<InfoResponse> CreateAsync(IssuePriorityCreateRequestModel requestModel);
        public Task<InfoResponse> DeleteAsync(long id);
        public Task<Response<Paginate<IssuePriorityResponseModel>>> GetAllAsync(PaginationRequestModel requestModel);
        public Task<Response<IssuePriorityResponseModel>> GetByIdAsync(long id);
        public Task<Response<Paginate<IssuePriorityResponseModel>>> SortByAsync(IssuePrioritySortRequestModel requestModel);
        public Task<InfoResponse> UpdateAsync(long id, IssuePriorityUpdateRequestModel requestModel);
    }
}