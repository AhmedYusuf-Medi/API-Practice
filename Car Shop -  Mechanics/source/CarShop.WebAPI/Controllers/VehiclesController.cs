namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly IVehicleService vehicleService;

        public VehiclesController(IVehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
        }

        /// <summary>
        /// Returns all vehicles
        /// </summary>
        [HttpGet]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var response = await this.vehicleService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns vehicle selected by id if it exists
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<VehicleResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<VehicleResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.vehicleService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new vehicle if the arguments are valid
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Constants.User)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromForm]VehicleCreateRequestModel requestModel)
        {
            var response = await this.vehicleService.CreateAsync(requestModel);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Updates vehicle if given arguments are valid
        /// </summary>
        [HttpPatch("{id}")]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromForm]VehicleUpdateRequestModel requestModel)
        {
            var response = await this.vehicleService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes vehicle if the given id exists
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.vehicleService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Filters and sorts all vehicles by selected criterias
        /// </summary>
        [HttpGet("filter")]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleResponseModel>>))]
        public async Task<IActionResult> FilterAsync([FromQuery]VehicleFilterAndSortRequestModel requestModel)
        {
            var response = await this.vehicleService.FilterByAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts all vehicles by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]VehicleSortRequestModel requestModel)
        {
            var response = await this.vehicleService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}