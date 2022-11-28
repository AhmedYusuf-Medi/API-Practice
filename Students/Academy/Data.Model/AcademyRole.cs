using Data.Base.Contracts;
using Microsoft.AspNetCore.Identity;

namespace Data.Models
{
    public class AcademyRole : IdentityRole, ISoftDeletableEntity
    {
        public AcademyRole(string name)
            : base(name)
        {
        }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedOn { get; set; }
    }
}
