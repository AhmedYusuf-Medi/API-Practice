namespace CarShop.Service.Data.ReportType
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.ReportType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.ReportType;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;

    public class ReportTypeService : BaseService, IReportTypeService
    {
        public ReportTypeService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<ReportTypeResponseModel>> GetByIdAsync(long id)
        {
            var reportType = await ReportTypeQueries.ReportTypeByIdAsync(id, this.db);

            var response = new Response<ReportTypeResponseModel>();
            response.Payload = reportType;

            EntityValidator.ValidateForNull(reportType, response, ResponseMessages.Entity_Get_Succeed, Constants.Report_Type);

            return response;
        }

        public async Task<Response<Paginate<ReportTypeResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var reportTypes = ReportTypeQueries.GetAllReportTypeResponse(this.db.ReportTypes);

            var payload = await Paginate<ReportTypeResponseModel>.ToPaginatedCollection(reportTypes, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<ReportTypeResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Report_Types), payload);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(ReportTypeCreateRequestModel requestModel)
        {
            var reportType = Mapper.ToReportType(requestModel);
            await this.db.ReportTypes.AddAsync(reportType);
            await this.db.SaveChangesAsync();

            var response = new InfoResponse();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Report_Type));

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, ReportTypeUpdateRequestModel requestModel)
        {
            var response = new InfoResponse();

            var reportType = await this.db.ReportTypes.Where(reportType => reportType.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(reportType, response, ResponseMessages.Entity_Edit_Succeed, Constants.Report_Type);

            if (response.IsSuccess)
            {
                reportType.Type = requestModel.ReportType;
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var reportType = await this.db.ReportTypes.Where(reportType => reportType.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(reportType, response, ResponseMessages.Entity_Delete_Succeed, Constants.Report_Type);

            if (response.IsSuccess)
            {
                this.db.ReportTypes.Remove(reportType);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<ReportTypeResponseModel>>> SortByAsync(ReportTypeSortRequestModel requestModel)
        {
            var reportTypes = ReportTypeQueries.Sort(requestModel, this.db.ReportTypes).AsQueryable();

            var responses = ReportTypeQueries.GetAllReportTypeResponse(reportTypes);

            var payload = await Paginate<ReportTypeResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<ReportTypeResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Report_Types), payload);

            return response;
        }
    }
}