namespace CarShop.WebAPI.Controllers
{
    //Local
    using CarShop.Models.Pagination;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.User;
    //Public
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService userService;

        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }

        /// <summary>
        /// Returns user by given Id
        /// </summary>
        [HttpGet("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<UserResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<UserResponseModel>))]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var response = await this.userService.GetByIdAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Returns all users
        /// </summary>
        [HttpGet("")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> GetAllAsync([FromQuery]PaginationRequestModel requestModel)
        {
            var response = await this.userService.GetAllAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes user by Id
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var response = await this.userService.DeleteAsync(id);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Block user
        /// </summary>
        [HttpPatch("block/{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> BlockAsync(long id)
        {
            var response = await this.userService.BlockAsync(id);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Unblock user
        /// </summary>
        [HttpPatch("unblock/{id}")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UnBlockAsync(long id)
        {
            var response = await this.userService.UnBlockAsync(id);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Search by given parameters
        /// </summary>
        [HttpGet("searchby")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> SearchByAsync([FromQuery]UserSearchAndSortRequestModel requestModel)
        {
            var response = await this.userService.SearchByAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Filters and sorts by choosen criterias
        /// </summary>
        [HttpGet("sortby")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> SortByAsync([FromQuery]UserSortRequestModel requestModel)
        {
            var response = await this.userService.SortByAsync(requestModel);

            return this.Ok(response);
        }

        /// <summary>
        /// Removes selected role from user
        /// </summary>
        [HttpDelete("role")]
        [Authorize(Roles = Constants.Admin)]
        [Authorize(Roles = Constants.Mechanic)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> RemoveRoleAsync([FromForm] long userId, long roleId)
        {
            var response = await this.userService.RemoveRole(userId, roleId);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Adds selected role to user
        /// </summary>
        [HttpPatch("role")]
        [Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> RegisterRoleAsync([FromForm] long userId, long roleId)
        {
            var response = await this.userService.RegisterRoleAsync(userId, roleId);

            if (!response.IsSuccess)
            {
                return this.NotFound(response);
            }

            return this.Ok(response);
        }
    }
}