using Data.Base.Contracts;

namespace Data.Models
{
    #nullable enable

    public class Student : SoftDeletableEntity<Guid>
    {
        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public ICollection<StudentsSubjects>? Subjects { get; set; }
    }
}
