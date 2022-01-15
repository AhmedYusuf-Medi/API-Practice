namespace CarShop.Models.Request.VehicleType
{
    using CarShop.Models.Request.Contracts;

    public class VehicleTypeSortRequestModel : SortRequestModel
    {
        public bool MostPopular { get; set; }
    }
}