using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request.Auth
{
    public class LoginRequestModel
    {
        [Required]
        [Email]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
