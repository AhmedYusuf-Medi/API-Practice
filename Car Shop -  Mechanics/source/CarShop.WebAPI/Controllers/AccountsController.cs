namespace CarShop.WebAPI.Controllers
{
    //Local
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Service.Account.Data;
    //Public
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Security.Claims;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountService accountService;

        public AccountsController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        /// <summary>
        /// Logins/Authenticates user by taking information from body and settings identity
        /// </summary>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]UserLoginRequestModel requestModel)
        {
            var result = await this.accountService.LoginAsync(requestModel);

            if (result.IsSuccess)
            {
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, result.Payload.Id.ToString()));
                identity.AddClaim(new Claim(ClaimTypes.Name, requestModel.Email));

                foreach (var role in result.Payload.Roles)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, role));
                }

                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

                return Ok(result);
            }

            return BadRequest(result);
        }

        /// <summary>
        /// Registers user by taking information from body
        /// </summary>
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(InfoResponse))]
        public async Task<IActionResult> RegisterAsync([FromBody]UserRegisterRequestModel requestModel)
        {
            var response = await this.accountService.RegisterUserAsync(requestModel);

            if (!response.IsSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

        /// <summary>
        /// Edit user profile information by selected arguments
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(InfoResponse))]
        //[Authorize("User")]
        public async Task<IActionResult> EditAsync(long id, [FromForm] UserEditRequestModel user)
        {
            var result = await this.accountService.EditProfileAsync(id, user);

            if (!result.IsSuccess)
            {
                return this.NotFound(result);
            }

            return this.Ok(result);
        }

        /// <summary>
        /// Takes the verification url that we send to the user so he can verificates his self
        /// </summary>
        [HttpGet("verification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> VerificateAsync([FromQuery]string email, Guid code)
        {
            var response = await this.accountService.VerificationAsync(email, code);

            if (!response.IsSuccess)
            {
                return this.BadRequest(response);
            }

            return this.Ok(response);
        }

        /// <summary>
        /// Logouts user
        /// </summary>
        /// <returns></returns>
        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> LogoutAsync()
        {
            await this.HttpContext.SignOutAsync();
            return this.Ok();
        }
    }
}