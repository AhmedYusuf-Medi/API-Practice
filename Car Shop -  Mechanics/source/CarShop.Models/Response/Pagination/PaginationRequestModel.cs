namespace Forum.Models.Pagination
{
    public class PaginationRequestModel
    {
        private const int maxEntityCount = 10;

        private int entitiesPerPage = 10;

        public int Page { get; set; } = 1;

        public int PerPage
        {
            get
            {
                return this.entitiesPerPage;
            }
            set
            {
                if (value > maxEntityCount && value <= 0) this.entitiesPerPage = maxEntityCount;
                else this.entitiesPerPage = value;
            }
        }
    }
}