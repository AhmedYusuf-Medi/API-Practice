namespace CarShop.Service.Data.Exception
{
    //Public
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Reflection;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ExceptionLogService : BaseService, IExceptionLogService
    {
        public ExceptionLogService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<Paginate<ExceptionLog>>> GetAllAsync(PaginationRequestModel request)
        {
            var exceptions = this.db.ExceptionLogs.AsQueryable();

            var payload = await Paginate<ExceptionLog>.ToPaginatedCollection(exceptions, request.Page, request.PerPage);

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

        public async Task<InfoResponse> MarkAsCheckedAsync(Guid exceptionId)
        {
            var response = new InfoResponse();

            var exception = await this.db.ExceptionLogs.FirstOrDefaultAsync(e => e.Id == exceptionId);

            EntityValidator.ValidateForNull(exception, response, ResponseMessages.Exception_Checked, Constants.Exception);

            if (response.IsSuccess)
            {
                if (!exception.IsChecked)
                {
                    exception.IsChecked = true;
                    await this.db.SaveChangesAsync();
                }
            }

            return response;
        }

        public async Task<Response<Paginate<ExceptionLog>>> FilterByAsync(ExceptionSortAndFilterRequestModel requestModel)
        {
            IQueryable<ExceptionLog> result = this.db.ExceptionLogs.AsQueryable();

            result = ExceptionQueries.Filter(requestModel, result);

            var response = new Response<Paginate<ExceptionLog>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Exceptions));


            var IsSortingNeeded = ClassScanner.IsThereAnyTrueProperty(requestModel);

            if (IsSortingNeeded)
            { 
                var sortByResponse = await this.SortByAsync(Mapper.ToRequest(requestModel), result);

                var sb = new StringBuilder();
                sb.AppendLine(response.Message);
                sb.AppendLine(sortByResponse.Message);

                response.Message = sb.ToString();

                ResponseSetter.ReworkMessageResult(response);
                response.Payload = sortByResponse.Payload;
            }
            else
            {
                var payload = await Paginate<ExceptionLog>.ToPaginatedCollection(result, requestModel.Page, requestModel.PerPage);

                response.Payload = payload;
            }

            return response;
        }

        public async Task<Response<Paginate<ExceptionLog>>> SortByAsync(ExceptionSortRequestModel requestModel, IQueryable<ExceptionLog> query = null)
        {
            IQueryable<ExceptionLog> result;

            if (query != null)
            {
                result = query;
            }
            else
            {
                result = this.db.ExceptionLogs.AsQueryable();
            }

            result = ExceptionQueries.SortBy(requestModel, result);

            var payload = await Paginate<ExceptionLog>.ToPaginatedCollection(result, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<ExceptionLog>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Exceptions));

            return response;
        }
    }
}