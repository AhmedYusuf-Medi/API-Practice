namespace CarShop.Models.Request.IssueStatus
{
    using CarShop.Models.Request.Contracts;

    public class IssueStatusSortRequestModel : SortRequestModel
    {
        public bool MostUsed { get; set; }
    }
}