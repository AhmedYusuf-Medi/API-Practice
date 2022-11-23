namespace Api.Models.Request
{
    public class AddSubjectToStudentRequest
    {
        public Guid SubjectId { get; set; }

        public Guid StudentId { get; set; }
    }
}
