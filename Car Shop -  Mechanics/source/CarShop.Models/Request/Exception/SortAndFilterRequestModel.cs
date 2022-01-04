namespace CarShop.Models.Request.Exception
{
    using CarShop.Models.Pagination;

    public class SortAndFilterRequestModel : PaginationRequestModel
    {
        public bool MostRecently { get; set; }

        public bool Oldest { get; set; }

        public string Date { get; set; }

        public bool? Checked { get; set; }
    }
}