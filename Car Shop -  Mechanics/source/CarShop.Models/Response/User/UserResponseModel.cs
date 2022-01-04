namespace CarShop.Models.Response.User
{
    using System.Collections.Generic;

    public class UserResponseModel
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public string Avatar { get; set; }
        public long IssueCount { get; set; }
        public long VehiclesCount { get; set; }
    }
}