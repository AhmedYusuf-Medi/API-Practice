using System.ComponentModel.DataAnnotations;

namespace Api.Models.Request.Student
{
    public class CreateStudentRequestModel
    {
        [Required]
        public string? FirstName { get; set; }

        [Required]
        public string? LastName { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }
    }
}
