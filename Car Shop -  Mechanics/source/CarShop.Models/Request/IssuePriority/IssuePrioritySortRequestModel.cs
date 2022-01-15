namespace CarShop.Models.Request.IssuePriority
{
    using CarShop.Models.Request.Contracts;

    public class IssuePrioritySortRequestModel : SortRequestModel
    {
        public bool MostUsed { get; set; }

        public bool BySeverity { get; set; }
    }
}