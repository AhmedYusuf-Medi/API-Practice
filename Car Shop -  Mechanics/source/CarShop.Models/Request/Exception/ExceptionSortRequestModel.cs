using CarShop.Models.Pagination;

namespace CarShop.Models.Request.Exception
{
    public class ExceptionSortRequestModel : PaginationRequestModel
    {
        public bool MostRecently { get; set; }

        public bool Oldest { get; set; }
    }
}