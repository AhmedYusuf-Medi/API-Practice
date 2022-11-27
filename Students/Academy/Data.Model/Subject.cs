using Data.Base.Contracts;

namespace Data.Models
{
    #nullable enable

    public class Subject : SoftDeletableEntity<Guid>
    {
        public string? Name { get; set; }

        public byte ExecutionTime { get; set; }

        public ICollection<LecturersSubjects>? Lecturers { get; set; } = new HashSet<LecturersSubjects>();

        public ICollection<StudentsSubjects>? Students { get; set; } = new HashSet<StudentsSubjects>();
    }
}
