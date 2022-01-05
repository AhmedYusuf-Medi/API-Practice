namespace CarShop.Models.Request.VehicleBrand
{
    using CarShop.Models.Pagination;

    public class VehicleBrandSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostPopular { get; set; }
    }
}