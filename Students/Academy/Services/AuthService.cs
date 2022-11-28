using Api.Models.Base;
using Api.Models.Request.Auth;
using Api.Models.Response.Auth;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Services.Contracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AcademyUser> _userManager;
        private readonly RoleManager<AcademyRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AuthService(UserManager<AcademyUser> userManager, RoleManager<AcademyRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager ?? throw new ArgumentNullException();
            _roleManager = roleManager ?? throw new ArgumentNullException();
            _configuration = configuration ?? throw new ArgumentNullException();
        }

        public async Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model)
        {
            Result<LoginResponseModel>? response;
            var user = await _userManager.FindByNameAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);

                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString())
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = GetToken(authClaims);

                response = new Result<LoginResponseModel>
                {
                    Payload = new LoginResponseModel
                    {
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        Expiration = token.ValidTo
                    },
                    IsSuccess = true,
                    Message = "Successfully loged in!"
                };
            }
            else
            {
                response = new Result<LoginResponseModel>
                {
                    Payload = null,
                    Message = "Failed authentication!"
                };
            }

            return response;
        }

        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTSettings:SecretKey"]));
            var duration = int.Parse(_configuration["JWTSettings:Duration"]);

            var token = new JwtSecurityToken(
                expires: DateTime.Now.AddHours(duration),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }
    }
}
