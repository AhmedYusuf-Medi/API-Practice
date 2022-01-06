using CarShop.Models.Pagination;
using CarShop.Models.Request.IssuePriority;
using CarShop.Models.Response;
using CarShop.Models.Response.IssuePriority;
using CarShop.Service.Common.Extensions.Pager;
using CarShop.Service.Data.IssuePriority;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarShop.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IssuePrioritiesController : ControllerBase
    {
        private readonly IIssuePriorityService issuePriorityService;

        public IssuePrioritiesController(IIssuePriorityService issuePriorityService)
        {
            this.issuePriorityService = issuePriorityService;
        }

        /// <summary>
        /// Returns all issue priorities
        /// </summary>
        [HttpGet]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssuePriorityResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.issuePriorityService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Returns issue priority by given Id
        /// </summary>
        [HttpGet("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<IssuePriorityResponseModel>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Response<IssuePriorityResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.issuePriorityService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }


        /// <summary>
        /// Creates new issue priority if the arguments are valid
        /// </summary>
        [HttpPost]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> CreateAsync([FromBody]IssuePriorityCreateRequestModel requestModel)
        {
            var response = await this.issuePriorityService.CreateAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Updates issue priority if the given Id exists
        /// </summary>
        [HttpPut("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody]IssuePriorityUpdateRequestModel requestModel)
        {
            var response = await this.issuePriorityService.UpdateAsync(id, requestModel);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes issue priority if the given Id exists
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.issuePriorityService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Sorts issue priorities by selected criterias
        /// </summary>
        [HttpGet("sortby")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<IssuePriorityResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]IssuePrioritySortRequestModel requestModel)
        {
            var response = await this.issuePriorityService.SortByAsync(requestModel);

            return this.Ok(response);
        }
    }
}