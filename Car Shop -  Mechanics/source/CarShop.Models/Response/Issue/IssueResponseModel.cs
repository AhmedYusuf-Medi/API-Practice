namespace CarShop.Models.Response.Issue
{
    public class IssueResponseModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long VehicleId { get; set; }
        public string VehicleOwner { get; set; }
        public long VehicleOwnerId { get; set; }
        public string VehiclePlateNumber { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public byte Severity { get; set; }
    }
}