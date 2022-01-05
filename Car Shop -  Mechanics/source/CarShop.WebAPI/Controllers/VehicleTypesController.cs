namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleType;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleType;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Data.VehicleType;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleTypesController : ControllerBase
    {
        private readonly IVehicleTypeService vehicleTypeService;

        public VehicleTypesController(IVehicleTypeService vehicleTypeService)
        {
            this.vehicleTypeService = vehicleTypeService;
        }

        /// <summary>
        /// Returns all vehicle types
        /// </summary>
        [HttpGet]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleTypeResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationRequestModel requestModel)
        {
            var response = await this.vehicleTypeService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns vehicle type by given Id
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<VehicleTypeResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<VehicleTypeResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.vehicleTypeService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new vehicle type if the arguments are valid
        /// </summary>
        [HttpPost]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromBody]VehicleTypeCreateRequestModel requestModel)
        {
            var response = await this.vehicleTypeService.CreateAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Updates vehicle type if the given Id exists
        /// </summary>
        [HttpPut("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]VehicleTypeCreateRequestModel requestModel)
        {
            var response = await this.vehicleTypeService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes vehicle type if the given Id exists
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.vehicleTypeService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts vehicle types by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleTypeResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]VehicleTypeSortRequestModel requestModel)
        {
            var response = await this.vehicleTypeService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}