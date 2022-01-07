namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Base;
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Exception;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ExceptionLogsController : ControllerBase
    {
        private readonly IExceptionLogService exceptionLogService;

        public ExceptionLogsController(IExceptionLogService exceptionLogService)
        {
            this.exceptionLogService = exceptionLogService;
        }

        /// <summary>
        /// Returns all non deleted/removed exceptions
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<ExceptionLog>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel request)
        {
            var response = await this.exceptionLogService.GetAllAsync(request);

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes choosen by Id exception
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var response = await this.exceptionLogService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Changes exception stataus from non-checked to checked
        /// </summary>
        [HttpPatch("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> MarkAsCheckedAsync(Guid id)
        {
            var response = await this.exceptionLogService.MarkAsCheckedAsync(id);

            if (!response.IsSuccess)
            {
                this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Filters and sorts exceptions by selected criterias
        /// </summary>
        [HttpGet("filter")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<ExceptionLog>>))]
        public async Task<IActionResult> FilterAsync([FromQuery]ExceptionSortAndFilterRequestModel request)
        {
            var respone = await this.exceptionLogService.FilterByAsync(request);

            return this.Ok(respone);
        }

        /// <summary>
        /// Sorts exceptions by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<ExceptionLog>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]ExceptionSortRequestModel request)
        {
            var respone = await this.exceptionLogService.SortByAsync(request);

            return this.Ok(respone);
        }
    }
}