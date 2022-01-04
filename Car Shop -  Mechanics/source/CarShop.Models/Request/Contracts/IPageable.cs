namespace CarShop.Models.Request.Contracts
{
    interface IPageable
    {
        public int Page { get; set; }

        public int PerPage { get; set; }
    }
}