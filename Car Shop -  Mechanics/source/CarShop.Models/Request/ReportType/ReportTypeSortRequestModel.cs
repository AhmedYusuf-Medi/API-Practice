namespace CarShop.Models.Request.ReportType
{
    using CarShop.Models.Request.Contracts;

    public class ReportTypeSortRequestModel : SortRequestModel
    {
        public bool MostUsed { get; set; }
    }
}