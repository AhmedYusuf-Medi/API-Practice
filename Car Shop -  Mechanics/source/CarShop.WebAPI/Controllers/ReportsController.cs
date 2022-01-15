namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Report;
    using CarShop.Service.Data.Report;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService reportService;

        public ReportsController(IReportService reportService)
        {
            this.reportService = reportService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var result = await this.reportService.GetByIdAsync(id);

            if (!result.IsSuccess)
            {
                return this.NotFound(result);
            }

            return this.Ok(result);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var result = await this.reportService.GetAllAsync(requestModel);

            return this.Ok(result);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateAsync([FromBody]ReportCreateRequestModel requestModel)
        {
            var result = await this.reportService.CreateAsync(requestModel);

            if (!result.IsSuccess)
            {
                return this.BadRequest(result);
            }

            return this.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var result = await this.reportService.DeleteAsync(id);

            if (!result.IsSuccess)
            {
                return this.NotFound(result);
            }

            return this.Ok(result);
        }

        [HttpGet("filter")]
        public async Task<IActionResult> FilterAsync([FromQuery] ReportFilterAndSortRequestModel requestModel)
        {
            var result = await this.reportService.FilterByAsync(requestModel);

            return this.Ok(result);
        }

        [HttpGet("sortby")]
        public async Task<IActionResult> SortByAsync([FromQuery]ReportSortRequestModel requestModel)
        {
            var result = await this.reportService.SortByAsync(requestModel);

            return this.Ok(result);
        }
    }
}