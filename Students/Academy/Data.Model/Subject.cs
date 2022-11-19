using Data.Base.Contracts;

namespace Data.Models
{
    #nullable enable

    public class Subject : SoftDeletableEntity<Guid>
    {
        public string? Name { get; set; }

        public byte ExecutionTime { get; set; }

        public ICollection<Lecturer>? Lecturers { get; set; } = new HashSet<Lecturer>();

        public ICollection<Student>? Students { get; set; } = new HashSet<Student>();
    }
}
