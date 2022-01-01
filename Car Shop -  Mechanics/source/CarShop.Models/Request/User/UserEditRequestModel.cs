namespace CarShop.Models.Request.User
{
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class UserEditRequestModel
    {
        [StringLength(20, MinimumLength = 2, ErrorMessage = " must be between {2} and {1}.")]
        public string Username { get; set; }

        [EmailAddress(ErrorMessage = " should be valid.")]
        public string Email { get; set; }

        [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[@$!%*#?&])[A-Za-z\d@$!%*#?&]{8,}$", ErrorMessage = " must be at least 8 symbols should contain capital letter, digit and special symbol (+, -, *, &, ^, …)!")]
        public string Password { get; set; }

        public IFormFile ProfilePicture { get; set; }
    }
}