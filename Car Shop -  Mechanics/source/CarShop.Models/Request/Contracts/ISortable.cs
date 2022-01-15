namespace CarShop.Models.Request.Contracts
{
    public interface ISortable
    {
        public bool Recently { get; set; }
        public bool Oldest { get; set; }
    }
}