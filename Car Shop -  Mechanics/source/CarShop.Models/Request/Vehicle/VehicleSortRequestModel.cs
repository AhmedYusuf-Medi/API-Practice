namespace CarShop.Models.Request.Vehicle
{
    using CarShop.Models.Request.Contracts;

    public class VehicleSortRequestModel : SortRequestModel
    {
        public bool ByYearDesc { get; set; }
        public bool ByYearAsc { get; set; }
        public bool MostIssues { get; set; }
        public bool LessIssues { get; set; }
    }
}