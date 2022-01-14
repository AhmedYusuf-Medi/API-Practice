namespace CarShop.Models.Request.Issue
{
    public class IssueFilterAndSortRequestModel : IssueSortRequestModel
    {
        public string Status { get; set; }
        public string Priority { get; set; }
        public long? VehicleId { get; set; }
        public string OwnerName { get; set; }
        public long? OwnerId { get; set; }
    }
}