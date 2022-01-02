namespace CarShop.Service.Account.Data
{
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using System;
    using System.Threading.Tasks;

    public interface IAccountService
    {
        public Task<InfoResponse> EditProfileAsync(long id, UserEditRequestModel user);
        public Task<Response<UserResponseModel>> LoginAsync(UserLoginRequestModel userLogin);
        public Task<InfoResponse> RegisterUserAsync(UserRegisterRequestModel user);
        public Task<InfoResponse> VerificationAsync(string email, Guid code);
    }
}