namespace Api.Models.Response.Auth
{
    public class LoginResponseModel
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
    }
}
