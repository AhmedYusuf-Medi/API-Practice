namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleBrand;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleBrand;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class VehicleBrandsController : ControllerBase
    {
        private readonly IVehicleBrandService vehicleBrandService;

        public VehicleBrandsController(IVehicleBrandService vehicleBrandService)
        {
            this.vehicleBrandService = vehicleBrandService;
        }

        /// <summary>
        /// Returns all vehicle brands
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleBrandResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.vehicleBrandService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns vehicle type by given Id
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<VehicleBrandResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<VehicleBrandResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.vehicleBrandService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new vehicle brand if the arguments are valid
        /// </summary>
        [HttpPost]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromBody]VehicleBrandCreateRequestModel requestModel)
        {
            var response = await this.vehicleBrandService.CreateAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Updates vehicle brand if the given Id exists
        /// </summary>
        [HttpPut("{id}")]
        [Authorize(Constants.Admin)]
        [Authorize(Constants.User)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long  id, [FromBody]VehicleBrandCreateRequestModel requestModel)
        {
            var response = await this.vehicleBrandService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes vehicle brand if the given Id exists
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.vehicleBrandService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts vehicle brands by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Constants.Admin)]
        [Authorize(Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<VehicleBrandResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]VehicleBrandSortRequestModel requestModel)
        {
            var response = await this.vehicleBrandService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}