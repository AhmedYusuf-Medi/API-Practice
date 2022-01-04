namespace CarShop.Service.Common.Mapper
{
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
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

        public static UserSortRequestModel ToRequest(UserSearchAndSortRequestModel requestModel)
        {
            return new UserSortRequestModel
            {
                Recently = requestModel.Recently,
                Oldest = requestModel.Oldest,
                MostActive = requestModel.MostActive,
                MostVehicles = requestModel.MostVehicles,
                PerPage = requestModel.PerPage,
                Page = requestModel.Page
            };
        }

        public static ExceptionSortRequestModel ToRequest(ExceptionSortAndFilterRequestModel requestModel)
        {
            return new ExceptionSortRequestModel
            {
                MostRecently = requestModel.MostRecently,
                Oldest = requestModel.Oldest,
                PerPage = requestModel.PerPage,
                Page = requestModel.Page
            };
        }
    }
}