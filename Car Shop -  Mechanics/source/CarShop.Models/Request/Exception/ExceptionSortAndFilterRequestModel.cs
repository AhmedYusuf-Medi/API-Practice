namespace CarShop.Models.Request.Exception
{
    using System;

    public class ExceptionSortAndFilterRequestModel : ExceptionSortRequestModel
    {
        public DateTime? Date { get; set; }

        public string Month { get; set; }

        public int? Year { get; set; }

        public bool Checked { get; set; }
    }
}