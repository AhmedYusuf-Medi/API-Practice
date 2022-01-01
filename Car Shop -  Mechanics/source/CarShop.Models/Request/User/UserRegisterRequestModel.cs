namespace CarShop.Models.Request.User
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class UserRegisterRequestModel
    {
        [Required]
        [StringLength(ValidationConstants.Max_Username_Length, MinimumLength = ValidationConstants.Min_Username_Length)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.Password_Regex, ErrorMessage = ValidationConstants.Invalid_Password_Message)]
        public string Password { get; set; }
    }
}