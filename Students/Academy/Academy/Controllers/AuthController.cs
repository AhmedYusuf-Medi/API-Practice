using Api.Models.Request.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Contracts;
using System.Threading.Tasks;

namespace Academy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService) =>
            _authService = authService;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]LoginRequestModel requestModel)
            => Ok(await _authService.LoginAsync(requestModel));
    }
}
