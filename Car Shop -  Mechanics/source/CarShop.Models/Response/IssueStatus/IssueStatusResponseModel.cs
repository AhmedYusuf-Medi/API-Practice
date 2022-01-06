using System;

namespace CarShop.Models.Response.IssueStatus
{
    public class IssueStatusResponseModel
    {
        public long Id { get; set; }

        public string StatusName { get; set; }

        public DateTime UsedSince { get; set; }

        public long IssuesCount { get; set; }
    }
}