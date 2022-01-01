namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class User : DeletableEntity<long>
    {
        public User()
        {
            this.Roles = new HashSet<UserRole>();
            this.Issues = new HashSet<Issue>();
            this.Vehicles = new HashSet<Vehicle>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_Username_Length, MinimumLength = ValidationConstants.Min_Username_Length)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.Password_Regex, ErrorMessage = ValidationConstants.Invalid_Password_Message)]
        public string Password { get; set; }

        public string PicturePath { get; set; }

        public string PictureId { get; set; }

        public Guid Code { get; set; }

        public ICollection<UserRole> Roles { get; set; }

        public ICollection<Issue> Issues { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}