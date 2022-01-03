namespace CarShop.Service.Data.Exception
{
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Response;
    using System;
    using System.Threading.Tasks;

    public interface IExceptionLogService
    {
        public Task<InfoResponse> CreateAsync(ExceptionLog exceptionLog);
        public Task<InfoResponse> DeleteAsync(Guid exceptionId);
        public Task<InfoResponse> MarkAsChecked(Guid exceptionId);
        public Task<InfoResponse> GetAllAsync(PaginationRequestModel request);
        public Task<InfoResponse> GetAllMostRecentlyAsync(PaginationRequestModel request);
    }
}