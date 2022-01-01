namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.Issue_Priority)]
    public class IssuePriority : DeletableEntity<long>
    {
        public IssuePriority()
        {
            this.Issues = new HashSet<Issue>();
        }
        
        [Required]
        [StringLength(ValidationConstants.Max_Priority_Length, MinimumLength = ValidationConstants.Min_Priority_Length)]
        public string Priority { get; set; }

        [Required]
        [Range(ValidationConstants.Min_Severity_Range, ValidationConstants.Max_Severity_Range)]
        public byte Severity { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}