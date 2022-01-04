namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Exception;
    using CarShop.Service.Data.Exception;
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

        [HttpGet]
        public async Task<IActionResult> GetAll(PaginationRequestModel request)
        {
            var response = await this.exceptionLogService.GetAllAsync(request);

            return this.Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var response = await this.exceptionLogService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                this.NotFound(response);
            }

            return this.Ok(response);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> MarkAsChecked(Guid id)
        {
            var response = await this.exceptionLogService.MarkAsChecked(id);

            if (!response.IsSuccess)
            {
                this.NotFound(response);
            }

            return this.Ok(response);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> Filter([FromQuery]SortAndFilterRequestModel request)
        {
            var respone = await this.exceptionLogService.FilterByAsync(request);

            return this.Ok(respone);
        }


        [HttpGet("sortby")]
        public async Task<IActionResult> SortBy([FromQuery]SortAndFilterRequestModel request)
        {
            var respone = await this.exceptionLogService.SortByAsync(request);

            return this.Ok(respone);
        }
    }
}