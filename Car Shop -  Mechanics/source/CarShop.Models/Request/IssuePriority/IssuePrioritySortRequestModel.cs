namespace CarShop.Models.Request.IssuePriority
{
    using CarShop.Models.Pagination;

    public class IssuePrioritySortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }

        public bool MostUsed { get; set; }

        public bool BySeverity { get; set; }
    }
}