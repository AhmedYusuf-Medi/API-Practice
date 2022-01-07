using CarShop.Models.Pagination;

namespace CarShop.Models.Request.Vehicle
{
    public class VehicleSortRequestModel : PaginationRequestModel
    {
        public bool MostRecentlyRegistered { get; set; }
        public bool OldestRegistered { get; set; }
        public bool ByYearDesc { get; set; }
        public bool ByYearAsc { get; set; }
        public bool MostIssues { get; set; }
        public bool LessIssues { get; set; }
    }
}