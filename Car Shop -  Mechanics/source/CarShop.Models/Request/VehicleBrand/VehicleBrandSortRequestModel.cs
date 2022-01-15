namespace CarShop.Models.Request.VehicleBrand
{
    using CarShop.Models.Request.Contracts;

    public class VehicleBrandSortRequestModel : SortRequestModel
    {
        public bool MostPopular { get; set; }
    }
}