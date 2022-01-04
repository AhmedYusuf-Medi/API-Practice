namespace CarShop.Service.Data.Exception
{
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Extensions.Pager;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IExceptionLogService
    {
       public Task<InfoResponse> CreateAsync(ExceptionLog exceptionLog);
       public Task<InfoResponse> DeleteAsync(Guid exceptionId);
       public Task<Response<Paginate<ExceptionLog>>> FilterByAsync(SortAndFilterRequestModel requestModel);
       public Task<InfoResponse> GetAllAsync(PaginationRequestModel request);
       public Task<InfoResponse> MarkAsChecked(Guid exceptionId);
       public Task<Response<Paginate<ExceptionLog>>> SortByAsync(SortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> query = null);
    }
}