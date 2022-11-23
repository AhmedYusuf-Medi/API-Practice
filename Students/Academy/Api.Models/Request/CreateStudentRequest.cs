using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request
{
    public class CreateStudentRequest
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
