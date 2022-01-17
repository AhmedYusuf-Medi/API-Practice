namespace CarShop.Service.Data.Report
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Reflection;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class ReportService : BaseService, IReportService
    {
        public ReportService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<Paginate<ReportResponseModel>>> GetAllAsync(PaginationRequestModel request)
        {
            var reports = ReportQueries.GetAllReportResponse(this.db.Reports);

            var payload = await Paginate<ReportResponseModel>.ToPaginatedCollection(reports, request.Page, request.PerPage);

            var response = new Response<Paginate<ReportResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Reports));

            return response;
        }

        public async Task<Response<ReportResponseModel>> GetByIdAsync(long reportId)
        {
            var response = new Response<ReportResponseModel>();

            var report = await ReportQueries.IssueByIdAsync(reportId, this.db);

            response.Payload = report;

            EntityValidator.ValidateForNull(response, ResponseMessages.Entity_Get_Succeed, Constants.Report);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(ReportCreateRequestModel requestModel)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            var doesReportExist = await this.db.Reports
                .AnyAsync(report => report.ReceiverId == requestModel.ReceiverId && report.SenderId == requestModel.SenderId);

            var sb = new StringBuilder();

            if (doesReportExist)
            {
                sb.AppendLine(ExceptionMessages.Cannot_Report_Twice);
                response.IsSuccess = false;
                response.Message = sb.ToString();
            }

            if (response.IsSuccess)
            {
                EntityValidator.CheckUser(response, requestModel.SenderId, this.db, Constants.User);
                EntityValidator.CheckUser(response, requestModel.ReceiverId, this.db, Constants.User);
                EntityValidator.CheckReportType(response, requestModel.ReportTypeId, this.db, Constants.Report_Type);
            }

            if (response.IsSuccess)
            {
                if (requestModel.ReceiverId == requestModel.SenderId)
                {
                    sb = new StringBuilder();
                    sb.AppendLine(ExceptionMessages.Cannot_Report_Yourself);
                    response.IsSuccess = false;
                    response.Message = sb.ToString();
                }
                else
                {
                    var report = Mapper.ToReport(requestModel);

                    await this.db.Reports.AddAsync(report);
                    await this.db.SaveChangesAsync();

                    ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Report));
                }
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var report = await this.db.Reports
                .Where(report => report.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(report, response, ResponseMessages.Entity_Delete_Succeed, Constants.Report);

            if (response.IsSuccess)
            {
                this.db.Reports.Remove(report);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<ReportResponseModel>>> FilterByAsync(ReportFilterAndSortRequestModel requestModel)
        {
            var IsSortingNeeded = ClassScanner.IsThereAnyTrueProperty(requestModel);

            IQueryable<Models.Base.Report> query = this.db.Reports.AsQueryable();

            query = ReportQueries.Filter(requestModel, query);

            var response = new Response<Paginate<ReportResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Reports));

            if (IsSortingNeeded)
            {
                var sortByResponse = await this.SortByAsync(Mapper.ToRequest(requestModel), query);

                var sb = new StringBuilder();
                sb.AppendLine(response.Message);
                sb.AppendLine(sortByResponse.Message);

                response.Message = sb.ToString();

                response.Payload = sortByResponse.Payload;
            }
            else
            {
                var filtered = ReportQueries.GetAllReportResponse(query);

                var payload = await Paginate<ReportResponseModel>.ToPaginatedCollection(filtered, requestModel.Page, requestModel.PerPage);

                response.Payload = payload;
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<Response<Paginate<ReportResponseModel>>> SortByAsync(ReportSortRequestModel requestModel, IQueryable<Models.Base.Report> reports = null)
        {
            IQueryable<Models.Base.Report> query;

            if (reports != null)
            {
                query = reports;
            }
            else
            {
                query = this.db.Reports.AsQueryable();
            }

            query = ReportQueries.SortBy(requestModel, query);

            var responses = ReportQueries.GetAllReportResponse(query);

            var payload = await Paginate<ReportResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<ReportResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Reports));

            return response;
        }
    }
}