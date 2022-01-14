namespace CarShop.Service.Data.Issue
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Exceptions;
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

    public class IssueService : BaseService, IIssueService
    {
        public IssueService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<Paginate<IssueResponseModel>>> GetAllAsync(PaginationRequestModel request)
        {
            var vehicles = IssueQueries.GetAllIssueResponse(this.db.Issues);

            var payload = await Paginate<IssueResponseModel>.ToPaginatedCollection(vehicles, request.Page, request.PerPage);

            var response = new Response<Paginate<IssueResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Issues));

            return response;
        }

        public async Task<Response<IssueResponseModel>> GetByIdAsync(long id)
        {
            var response = new Response<IssueResponseModel>();

            var vehicle = await IssueQueries.IssueByIdAsync(id, this.db);

            response.Payload = vehicle;

            EntityValidator.ValidateForNull(response, ResponseMessages.Entity_Get_Succeed, Constants.Issue);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(IssueCreateRequestModel requestModel)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            EntityValidator.CheckVehicle(response, requestModel.VehicleId, this.db, Constants.Vehicle);
            EntityValidator.CheckIssueStatus(response, requestModel.StatusId, this.db, Constants.IssueStatus);
            EntityValidator.CheckIssuePriority(response, requestModel.PriorityId, this.db, Constants.IssuePriority);

            if (response.IsSuccess)
            {
                var issue = Mapper.ToIssue(requestModel);

                await this.db.Issues.AddAsync(issue);
                await this.db.SaveChangesAsync();

                ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Issue));
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<InfoResponse> ChangeStatusAsync(long issueId, long statusId)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            var issue = await this.db.Issues.FirstOrDefaultAsync(issue => issue.Id == issueId);

            EntityValidator.ValidateForNull(issue, response, Constants.Issue);
            EntityValidator.CheckIssueStatus(response, statusId, this.db, Constants.IssueStatus);

            if (response.IsSuccess)
            {
                issue.StatusId = statusId;
                await this.db.SaveChangesAsync();

                ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Partial_Update_Suceed, Constants.Issue));
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, IssueUpdateRequestModel requestModel)
        {
            var response = new InfoResponse();
            response.IsSuccess = true;

            var issue = await this.db.Issues.FirstOrDefaultAsync(issue => issue.Id == id);

            EntityValidator.ValidateForNull(issue, response, Constants.Issue);

            if (response.IsSuccess)
            {
                var isChangesDone = false;

                if (requestModel.VehicleId.HasValue && requestModel.VehicleId != issue.VehicleId)
                {
                    EntityValidator.CheckVehicle(response, (long)requestModel.VehicleId, this.db, Constants.Vehicle);

                    if (response.IsSuccess)
                    {
                        issue.VehicleId = (long)requestModel.VehicleId;
                        isChangesDone = true;
                    }
                }

                if (requestModel.StatusId.HasValue && requestModel.StatusId != issue.StatusId)
                {
                    EntityValidator.CheckIssueStatus(response, (long)requestModel.StatusId, this.db, Constants.IssueStatus);

                    if (response.IsSuccess)
                    {
                        issue.StatusId = (long)requestModel.StatusId;
                        isChangesDone = true;
                    }
                }

                if (requestModel.PriorityId.HasValue && requestModel.PriorityId != issue.PriorityId)
                {
                    EntityValidator.CheckIssuePriority(response, (long)requestModel.PriorityId, this.db, Constants.IssuePriority);

                    if (response.IsSuccess)
                    {
                        issue.PriorityId = (long)requestModel.PriorityId;
                        isChangesDone = true;
                    }
                }

                if (EntityValidator.IsStringPropertyValid(requestModel.Description, issue.Description))
                {
                    issue.Description = requestModel.Description;
                    isChangesDone = true;
                }

                if (isChangesDone)
                {
                    response.IsSuccess = true;
                    var sb = new StringBuilder(response.Message);
                    sb.AppendLine(string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue));
                    response.Message = sb.ToString();

                    await this.db.SaveChangesAsync();
                }
                else
                {
                    throw new BadRequestException(ExceptionMessages.Arguments_Are_Invalid);
                }
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var issue = await this.db.Issues
                .Where(issue => issue.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(issue, response, ResponseMessages.Entity_Delete_Succeed, Constants.Issue);

            if (response.IsSuccess)
            {
                this.db.Issues.Remove(issue);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<IssueResponseModel>>> FilterByAsync(IssueFilterAndSortRequestModel requestModel)
        {
            var IsSortingNeeded = ClassScanner.IsThereAnyTrueProperty(requestModel);

            IQueryable<Models.Base.Issue> query = this.db.Issues.AsQueryable();

            query = IssueQueries.Filter(requestModel, query);

            var response = new Response<Paginate<IssueResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Issues));

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
                var filtered = IssueQueries.GetAllIssueResponse(query);

                var payload = await Paginate<IssueResponseModel>.ToPaginatedCollection(filtered, requestModel.Page, requestModel.PerPage);

                response.Payload = payload;
            }

            ResponseSetter.ReworkMessageResult(response);
            return response;
        }

        public async Task<Response<Paginate<IssueResponseModel>>> SortByAsync(IssueSortRequestModel requestModel, IQueryable<Models.Base.Issue> issues = null)
        {
            IQueryable<Models.Base.Issue> query;

            if (issues != null)
            {
                query = issues;
            }
            else
            {
                query = this.db.Issues.AsQueryable();
            }

            query = IssueQueries.SortBy(requestModel, query);

            var responses = IssueQueries.GetAllIssueResponse(query);

            var payload = await Paginate<IssueResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<IssueResponseModel>>();
            response.Payload = payload;
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Issues));

            return response;
        }
    }
}