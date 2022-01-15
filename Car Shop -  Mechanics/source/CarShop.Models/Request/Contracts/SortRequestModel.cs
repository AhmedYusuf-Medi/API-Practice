namespace CarShop.Models.Request.Contracts
{
    using CarShop.Models.Pagination;

    public class SortRequestModel : PaginationRequestModel, ISortable
    {
        public bool Recently { get; set; }
        public bool Oldest { get; set; }
    }
}