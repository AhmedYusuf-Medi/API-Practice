namespace CarShop.Service.Common.Mapper
{
    using CarShop.Models.Base;
    using CarShop.Models.Request.User;

    public static class Mapper
    {
        public static User ToUser(UserRegisterRequestModel user)
        {
            return new User()
            {
                Username = user.Username,
                Email = user.Email,
                Password = user.Password,
            };
        }
    }
}