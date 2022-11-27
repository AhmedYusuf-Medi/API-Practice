namespace Api.Models.Response.Student
{
    public class StudentResponseModel
    {
        public Guid Id { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime RegisteredOn { get; set; }

        public long SubjectsCount { get; set; }
    }
}
