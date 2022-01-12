namespace CarShop.Models.Request.Exception
{
    using CarShop.Models.Pagination;

    public class ExceptionSortRequestModel : PaginationRequestModel
    {
        public bool Recently { get; set; }

        public bool Oldest { get; set; }
    }
}