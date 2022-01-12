namespace CarShop.Models.Request.Vehicle
{
    public class VehicleFilterAndSortRequestModel : VehicleSortRequestModel
    {
        public string OwnerName { get; set; }
        public string OwnerEmail { get; set; }
        public string Model { get; set; }
        public string PlateNumber { get; set; }
        public long? BrandId { get; set; }
        public long? VehicleTypeId { get; set; }
        public long? OwnerId { get; set; }
        public long? IssueCount { get; set; }
        public int? Year { get; set; }
    }
}