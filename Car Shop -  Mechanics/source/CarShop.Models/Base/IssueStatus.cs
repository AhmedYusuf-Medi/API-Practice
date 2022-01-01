namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.Issue_Status)]
    public class IssueStatus : DeletableEntity<long>
    {
        public IssueStatus()
        {
            this.Issues = new HashSet<Issue>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_Status_Length, MinimumLength = ValidationConstants.Min_Status_Length)]
        public string Status { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}