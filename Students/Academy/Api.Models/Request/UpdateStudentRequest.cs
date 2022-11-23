namespace Api.Models.Request
{
    public class UpdateStudentRequest
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
