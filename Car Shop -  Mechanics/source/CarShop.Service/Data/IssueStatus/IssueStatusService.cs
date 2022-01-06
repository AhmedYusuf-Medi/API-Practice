namespace CarShop.Service.Data.IssueStatus
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Extensions.Query;
    using CarShop.Service.Common.Extensions.Validator;
    using CarShop.Service.Common.Mapper;
    using CarShop.Service.Common.Messages;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using System.Linq;
    using System.Threading.Tasks;

    public class IssueStatusService : BaseService, IIssueStatusService
    {
        public IssueStatusService(CarShopDbContext db) : base(db)
        {
        }

        public async Task<Response<IssueStatusResponseModel>> GetByIdAsync(long id)
        {
            var issueStatus = await IssueStatusQueries.IssueStatusByIdAsync(id, this.db);

            var response = new Response<IssueStatusResponseModel>();
            response.Payload = issueStatus;

            EntityValidator.ValidateForNull(issueStatus, response, ResponseMessages.Entity_Get_Succeed, Constants.IssueStatus);

            return response;
        }

        public async Task<Response<Paginate<IssueStatusResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var issueStatuses = IssueStatusQueries.GetAllIssueStatusResponse(this.db.IssueStatuses);

            var payload = await Paginate<IssueStatusResponseModel>.ToPaginatedCollection(issueStatuses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<IssueStatusResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.IssueStatuses), payload);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(IssueStatusCreateRequestModel requestModel)
        {
            var issueStatus = Mapper.ToIssueStatus(requestModel);
            await this.db.IssueStatuses.AddAsync(issueStatus);
            await this.db.SaveChangesAsync();

            var response = new InfoResponse();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.IssueStatus));

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, IssueStatusCreateRequestModel requestModel)
        {
            var response = new InfoResponse();

            var issueStatus = await this.db.IssueStatuses.Where(issueStatus => issueStatus.Id == id)
                .FirstOrDefaultAsync();

            issueStatus.Status = requestModel.StatusName;
            await this.db.SaveChangesAsync();

            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.IssueStatus));

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var issueStatus = await this.db.VehicleBrands.Where(issueStatus => issueStatus.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(issueStatus, response, ResponseMessages.Entity_Delete_Succeed, Constants.VehicleBrand);

            if (response.IsSuccess)
            {
                this.db.VehicleBrands.Remove(issueStatus);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<IssueStatusResponseModel>>> SortByAsync(IssueStatusSortRequestModel requestModel)
        {
            var issueStatuses = IssueStatusQueries.Sort(requestModel, this.db.IssueStatuses).AsQueryable();

            var responses = IssueStatusQueries.GetAllIssueStatusResponse(issueStatuses);

            var payload = await Paginate<IssueStatusResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<IssueStatusResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.VehicleBrands), payload);

            return response;
        }
    }
}