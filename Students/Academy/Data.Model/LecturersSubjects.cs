using Data.Base.Contracts;

#nullable enable

namespace Data.Models
{
    public class LecturersSubjects : CompositeEntity<Guid, Guid>
    {
        public Lecturer? Lecturer { get; set; }

        public Subject? Subject { get; set; }
    }
}
