namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class IssuesController : ControllerBase
    {
        private readonly IIssueService issueService;

        public IssuesController(IIssueService issueService)
        {
            this.issueService = issueService;
        }

        /// <summary>
        /// Returns all issues
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.issueService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns issue selected by id if it exists
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.User)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<IssueResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<IssueResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.issueService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new issue if passed argumenst are valid
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromForm]IssueCreateRequestModel requestModel)
        {
            var response = await this.issueService.CreateAsync(requestModel);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Changes issue status if given arguments are valid
        /// </summary>
        [HttpPatch("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> ChangeStatusAsync([FromForm]long statusId,[FromQuery] long id)
        {
            var response = await this.issueService.ChangeStatusAsync(id, statusId);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Updates issue if given arguments are valid.
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm]IssueUpdateRequestModel requestModel)
        {
            var response = await this.issueService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes issue if given id exists
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.issueService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts all issues by selected criterias
        /// </summary>
        [HttpGet("filter")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueResponseModel>>))]
        public async Task<IActionResult> FilterAsync([FromQuery]IssueFilterAndSortRequestModel requestModel)
        {
            var response = await this.issueService.FilterByAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts all issues by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]IssueSortRequestModel requestModel)
        {
            var response = await this.issueService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}