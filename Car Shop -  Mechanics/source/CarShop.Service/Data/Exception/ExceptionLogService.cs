namespace CarShop.Service.Data.Exception
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Messages;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class ExceptionLogService : BaseService, IExceptionLogService
    {
        public ExceptionLogService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<InfoResponse> GetAllAsync(PaginationRequestModel request)
        {
            var result = this.db.ExceptionLogs.AsQueryable();

            var payload = await Paginate<ExceptionLog>.ToPaginatedCollection(result, request.Page, request.PerPage);

            var response = new Response<Paginate<ExceptionLog>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Exceptions));

            return response;
        }

        public async Task<InfoResponse> CreateAsync(ExceptionLog exceptionLog)
        {
            var response = new InfoResponse();

            await this.db.ExceptionLogs.AddAsync(exceptionLog);
            await this.db.SaveChangesAsync();

            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Exception));

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(Guid exceptionId)
        {
            var response = new InfoResponse();

            var exceptionToDelete = await this.db.ExceptionLogs.FirstOrDefaultAsync(e => e.Id == exceptionId);

            EntityValidator.ValidateForNull(exceptionToDelete, response, ResponseMessages.Entity_Delete_Succeed, Constants.Exception);

            if (response.IsSuccess)
            {
                this.db.ExceptionLogs.Remove(exceptionToDelete);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> MarkAsChecked(Guid exceptionId)
        {
            var response = new InfoResponse();

            var exception = await this.db.ExceptionLogs.FirstOrDefaultAsync(e => e.Id == exceptionId);

            EntityValidator.ValidateForNull(exception, response, ResponseMessages.Exception_Checked, Constants.Exception);

            if (response.IsSuccess)
            {
                exception.IsChecked = true;
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> GetAllMostRecentlyAsync(PaginationRequestModel request)
        {
            var result = this.db.ExceptionLogs.OrderByDescending(e => e.CreatedOn).AsQueryable();

            var payload = await Paginate<ExceptionLog>.ToPaginatedCollection(result, request.Page, request.PerPage);

            var response = new Response<Paginate<ExceptionLog>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Exceptions));

            return response;
        }
    }
}