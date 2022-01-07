namespace CarShop.Service.Data.IssuePriority
{
    //Local
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssuePriority;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Exceptions;
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

    public class IssuePriorityService : BaseService, IIssuePriorityService
    {
        public IssuePriorityService(CarShopDbContext db)
            : base(db)
        {
        }

        public async Task<Response<IssuePriorityResponseModel>> GetByIdAsync(long id)
        {
            var vehicleBrand = await IssuePriorityQueries.IssuePriorityByIdAsync(id, this.db);

            var response = new Response<IssuePriorityResponseModel>();
            response.Payload = vehicleBrand;

            EntityValidator.ValidateForNull(vehicleBrand, response, ResponseMessages.Entity_Get_Succeed, Constants.IssuePriority);

            return response;
        }

        public async Task<Response<Paginate<IssuePriorityResponseModel>>> GetAllAsync(PaginationRequestModel requestModel)
        {
            var issuePriorities = IssuePriorityQueries.GetAllIssuePriorityResponse(this.db.IssuePriorities);

            var payload = await Paginate<IssuePriorityResponseModel>.ToPaginatedCollection(issuePriorities, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<IssuePriorityResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.IssuePriorities), payload);

            return response;
        }

        public async Task<InfoResponse> CreateAsync(IssuePriorityCreateRequestModel requestModel)
        {
            var issuePriority = Mapper.ToIssuePriority(requestModel);
            await this.db.IssuePriorities.AddAsync(issuePriority);
            await this.db.SaveChangesAsync();

            var response = new InfoResponse();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.IssuePriority));

            return response;
        }

        public async Task<InfoResponse> UpdateAsync(long id, IssuePriorityUpdateRequestModel requestModel)
        {
            var response = new InfoResponse();

            var issuePriority = await this.db.IssuePriorities
                .Where(issuePriority => issuePriority.Id == id)
                .FirstOrDefaultAsync();

            bool isChangesMade = false;

            if (EntityValidator.IsStringPropertyValid(requestModel.PriorityName))
            {
                issuePriority.Priority = requestModel.PriorityName;
                isChangesMade = true;
            }

            if (requestModel.Severity != null)
            {
                issuePriority.Severity = (byte)requestModel.Severity;
                isChangesMade = true;
            }

            if (isChangesMade)
            {
                await this.db.SaveChangesAsync();
                ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.IssuePriority));
            }
            else
            {
                throw new BadRequestException(ExceptionMessages.Arguments_Are_Invalid);
            }

            return response;
        }

        public async Task<InfoResponse> DeleteAsync(long id)
        {
            var response = new InfoResponse();

            var issuePriority = await this.db.IssuePriorities.Where(issuePriority => issuePriority.Id == id)
                .FirstOrDefaultAsync();

            EntityValidator.ValidateForNull(issuePriority, response, ResponseMessages.Entity_Delete_Succeed, Constants.IssuePriority);

            if (response.IsSuccess)
            {
                this.db.IssuePriorities.Remove(issuePriority);
                await this.db.SaveChangesAsync();
            }

            return response;
        }

        public async Task<Response<Paginate<IssuePriorityResponseModel>>> SortByAsync(IssuePrioritySortRequestModel requestModel)
        {
            var issuPriorities = IssuePriorityQueries.Sort(requestModel, this.db.IssuePriorities).AsQueryable();

            var responses = IssuePriorityQueries.GetAllIssuePriorityResponse(issuPriorities);

            var payload = await Paginate<IssuePriorityResponseModel>.ToPaginatedCollection(responses, requestModel.Page, requestModel.PerPage);

            var response = new Response<Paginate<IssuePriorityResponseModel>>();
            ResponseSetter.SetResponse(response, true, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.VehicleBrands), payload);

            return response;
        }
    }
}