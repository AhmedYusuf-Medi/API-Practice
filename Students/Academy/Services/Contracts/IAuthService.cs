using Api.Models.Base;
using Api.Models.Request.Auth;
using Api.Models.Response.Auth;

namespace Services.Contracts
{
    public interface IAuthService
    {
        Task<Result<LoginResponseModel>> LoginAsync(LoginRequestModel model);
    }
}