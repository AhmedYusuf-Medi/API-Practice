namespace Api.Models.Request.Student
{
    public class AddSubjectToStudentRequestModel
    {
        public Guid SubjectId { get; set; }

        public Guid StudentId { get; set; }
    }
}
