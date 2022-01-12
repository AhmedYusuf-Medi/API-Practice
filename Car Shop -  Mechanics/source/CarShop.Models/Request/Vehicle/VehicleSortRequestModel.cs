namespace CarShop.Models.Request.Vehicle
{
    using CarShop.Models.Pagination;

    public class VehicleSortRequestModel : PaginationRequestModel
    {
        public bool RecentlyRegistered { get; set; }
        public bool OldestRegistered { get; set; }
        public bool ByYearDesc { get; set; }
        public bool ByYearAsc { get; set; }
        public bool MostIssues { get; set; }
        public bool LessIssues { get; set; }
    }
}