namespace CarShop.Models.Response.ReportType
{
    using System;

    public class ReportTypeResponseModel
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public long ReportsCount { get; set; }
        public DateTime UsedSince { get; set; }
    }
}