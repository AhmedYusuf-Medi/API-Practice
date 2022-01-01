namespace Forum.Models.Pagination
{
    public class Metadata
    {
        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }

        public int PerPage { get; set; }

        public int TotalEntites { get; set; }
    }
}