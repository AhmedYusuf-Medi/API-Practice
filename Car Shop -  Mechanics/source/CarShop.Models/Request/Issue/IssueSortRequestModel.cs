namespace CarShop.Models.Request.Issue
{
    using CarShop.Models.Request.Contracts;

    public class IssueSortRequestModel : SortRequestModel
    {
        public bool BySeverity { get; set; }
    }
}