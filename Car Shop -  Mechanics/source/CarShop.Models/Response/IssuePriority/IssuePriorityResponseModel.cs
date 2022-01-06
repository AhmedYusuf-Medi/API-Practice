namespace CarShop.Models.Response.IssuePriority
{
    using System;

    public class IssuePriorityResponseModel
    {
        public long Id { get; set; }

        public string PriorityName { get; set; }

        public byte Severity { get; set; }

        public DateTime UsedSince { get; set; }

        public long IssuesCount { get; set; }
    }
}