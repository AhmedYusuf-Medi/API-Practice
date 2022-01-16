namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.ReportType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Models.Response.ReportType;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.ReportType;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ReportTypesController : ControllerBase
    {

        private readonly IReportTypeService reportTypeService;

        public ReportTypesController(IReportTypeService reportTypeService)
        {
            this.reportTypeService = reportTypeService;
        }

        /// <summary>
        /// Returns all report types
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<ReportTypeResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.reportTypeService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns report type by given Id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<ReportTypeResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<ReportTypeResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.reportTypeService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new report type if the arguments are valid
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromBody]ReportTypeCreateRequestModel requestModel)
        {
            var response = await this.reportTypeService.CreateAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Updates report type if the given Id exists
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]ReportTypeUpdateRequestModel requestModel)
        {
            var response = await this.reportTypeService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes report type if the given Id exists
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.reportTypeService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts report types by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssueStatusResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]ReportTypeSortRequestModel requestModel)
        {
            var response = await this.reportTypeService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}