namespace CarShop.Models.Request.User
{
    using CarShop.Models.Pagination;

    public class UserSearchRequestModel : PaginationRequestModel
    {
        public string Username { get; set; }

        public string Email { get; set; }

        public string Role { get; set; }
    }
}