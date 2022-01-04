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
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<UserResponseModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(Response<UserResponseModel>))]
        public async Task<IActionResult> GetById(long id)
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
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> GetAll([FromQuery]PaginationRequestModel request)
        {
            var response = await this.userService.GetAllAsync(request);

            return this.Ok(response);
        }

        /// <summary>
        /// Deletes user by Id
        /// </summary>
        [HttpDelete("{id}")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> Delete(long id)
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
        public async Task<IActionResult> Block(long id)
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
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> UnBlock(long id)
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
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> SearchBy([FromQuery]UserSearchAndSortRequestModel model)
        {
            var response = await this.userService.SearchByAsync(model);

            return this.Ok(response);
        }

        /// <summary>
        /// Filters and sorts by choosen criterias
        /// </summary>
        [HttpGet("sortby")]
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Response<Paginate<UserResponseModel>>))]
        public async Task<IActionResult> SortBy([FromQuery]UserSortRequestModel model)
        {
            var response = await this.userService.SortByAsync(model);

            return this.Ok(response);
        }

        /// <summary>
        /// Removes selected role from user
        /// </summary>
        [HttpDelete("role")]
        //[Authorize(Roles = Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> RemoveRole([FromForm] long userId, long roleId)
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
        //[Authorize(Constants.Admin)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        public async Task<IActionResult> RegisterRole([FromForm] long userId, long roleId)
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