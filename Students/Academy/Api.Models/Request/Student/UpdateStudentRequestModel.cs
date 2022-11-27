namespace Api.Models.Request.Student
{
    public class UpdateStudentRequestModel
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
