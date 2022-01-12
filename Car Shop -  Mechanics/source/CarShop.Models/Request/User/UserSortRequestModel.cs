namespace CarShop.Models.Request.User
{
    using CarShop.Models.Pagination;

    public class UserSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostActive { get; set; }

        public bool MostVehicles { get; set; }
    }
}