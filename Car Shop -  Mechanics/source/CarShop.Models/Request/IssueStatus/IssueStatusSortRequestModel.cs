namespace CarShop.Models.Request.IssueStatus
{
    using CarShop.Models.Pagination;

    public class IssueStatusSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostUsed { get; set; }
    }
}