namespace CarShop.Models.Request.VehicleType
{
    using CarShop.Models.Pagination;

    public class VehicleTypeSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostPopular { get; set; }
    }
}