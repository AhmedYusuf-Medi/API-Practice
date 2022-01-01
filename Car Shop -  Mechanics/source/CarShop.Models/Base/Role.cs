namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Role : DeletableEntity<long>
    {
        public Role()
        {
            this.Users = new HashSet<UserRole>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_Role_Length,MinimumLength = ValidationConstants.Max_Role_Length)]
        public string Type { get; set; }

        public ICollection<UserRole> Users { get; set; }
    }
}