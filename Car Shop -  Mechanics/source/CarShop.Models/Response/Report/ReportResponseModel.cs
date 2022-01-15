namespace CarShop.Models.Response.Report
{
    public class ReportResponseModel
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public long SenderId { get; set; }
        public string SenderName { get; set; }
        public long ReceiverId { get; set; }
        public string ReceiverName { get; set; }
        public string ReportType { get; set; }
    }
}