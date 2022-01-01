using System.Collections.Generic;

namespace CarShop.Models.Response.User
{
    public class UserLoginResponseModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Avatar { get; set; }
    }
}