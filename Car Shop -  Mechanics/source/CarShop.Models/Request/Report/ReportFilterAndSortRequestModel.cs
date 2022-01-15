namespace CarShop.Models.Request.Report
{
    public class ReportFilterAndSortRequestModel : ReportSortRequestModel
    {
        public long? SenderId { get; set; }
        public long? ReceiverId { get; set; }
        public long? ReportTypeId { get; set; }
        public string SenderName { get; set; }
        public string ReceiverName { get; set; }
    }
}