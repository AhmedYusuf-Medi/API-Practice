using Data.Base.Contracts;

#nullable enable

namespace Data.Models
{
    public class StudentsSubjects : CompositeEntity<Guid, Guid>
    {
        public Student? Student { get; set; }

        public Subject? Subject { get; set; }
    }
}
