using Data.Base.Contracts;

namespace Data.Models
{
    #nullable enable

    public class Subject : SoftDeletableEntity<Guid>
    {
        public string? Name { get; set; }

        public byte ExecutionTime { get; set; }

        public ICollection<LecturersSubjects>? Lecturers { get; set; }

        public ICollection<StudentsSubjects>? Students { get; set; }
    }
}
