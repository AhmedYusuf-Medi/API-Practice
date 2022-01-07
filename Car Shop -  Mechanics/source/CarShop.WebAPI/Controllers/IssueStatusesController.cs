namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssueStatus;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;


    [Route("api/[controller]")]
    [ApiController]
    public class IssueStatusesController : ControllerBase
    {
        private readonly IIssueStatusService issueStatusService;

        public IssueStatusesController(IIssueStatusService issueStatusService)
        {
            this.issueStatusService = issueStatusService;
        }

        /// <summary>
        /// Returns all issue statuses
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueStatusResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.issueStatusService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns issue status by given Id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<IssueStatusResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<IssueStatusResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.issueStatusService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new issue status if the arguments are valid
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromBody]IssueStatusCreateRequestModel requestModel)
        {
            var response = await this.issueStatusService.CreateAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Updates issue status if the given Id exists
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]IssueStatusCreateRequestModel requestModel)
        {
            var response = await this.issueStatusService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes issue status if the given Id exists
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.issueStatusService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts issue statuses by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueStatusResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]IssueStatusSortRequestModel requestModel)
        {
            var response = await this.issueStatusService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}