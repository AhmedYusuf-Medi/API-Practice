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
       public Task<Response<Paginate<ExceptionLog>>> FilterByAsync(ExceptionSortAndFilterRequestModel requestModel);
       public Task<Response<Paginate<ExceptionLog>>> GetAllAsync(PaginationRequestModel request);
       public Task<InfoResponse> MarkAsCheckedAsync(Guid exceptionId);
       public Task<Response<Paginate<ExceptionLog>>> SortByAsync(ExceptionSortRequestModel requestModel, IQueryable<ExceptionLog> query = null);
    }
}