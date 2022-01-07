namespace CarShop.Models.Request.Issue
{
    using CarShop.Models.Pagination;

    public class IssueSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }
        public bool Oldest { get; set; }
        public bool BySeverity { get; set; }
    }
}