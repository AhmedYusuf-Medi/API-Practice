namespace CarShop.Models.Request.User
{
    using CarShop.Models.Request.Contracts;

    public class UserSortRequestModel : SortRequestModel
    { 

        public bool MostActive { get; set; }

        public bool MostVehicles { get; set; }
    }
}