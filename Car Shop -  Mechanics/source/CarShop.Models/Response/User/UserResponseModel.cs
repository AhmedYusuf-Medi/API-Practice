namespace CarShop.Models.Response.User
{
    using System;

    public class UserResponseModel
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PicturePath { get; set; }
        public string Role { get; set; }
        public Guid Code { get; set; }
        public long PostsCount { get; set; }
    }
}