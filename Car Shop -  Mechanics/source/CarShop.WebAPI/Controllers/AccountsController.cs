namespace CarShop.WebAPI.Controllers
{
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Service.Account.Data;
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

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserLoginRequestModel requestModel)
        {
            var result = await accountService.LoginAsync(requestModel);

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
            else
            {
                return BadRequest(result);
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserRegisterRequestModel requestModel)
        {
            var result = await accountService.RegisterUserAsync(requestModel);

            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        /// <summary>
        /// Takes the verification url that we send to the user so he can verificates his self
        /// </summary>
        [HttpGet("verification")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(InfoResponse))]
        public async Task<IActionResult> Verification([FromQuery] string email, Guid code)
        {
            var result = await this.accountService.VerificationAsync(email, code);
            return Ok(result);
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return Ok();
        }
    }
}