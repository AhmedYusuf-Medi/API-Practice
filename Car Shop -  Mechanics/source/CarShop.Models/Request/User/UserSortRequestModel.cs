using CarShop.Models.Pagination;

namespace CarShop.Models.Request.User
{
    public class UserSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostActive { get; set; }

        public bool MostVehicles { get; set; }
    }
}