using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request.Auth
{
    public class RegisterRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
