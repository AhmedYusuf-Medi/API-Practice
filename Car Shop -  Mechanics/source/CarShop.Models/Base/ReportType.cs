namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.Report_Type)]
    public class ReportType : DeletableEntity<int>
    {
        public ReportType()
        {
            this.Reports = new HashSet<Report>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_ReportType_Length, MinimumLength = ValidationConstants.Max_ReportType_Length)]
        public string Type { get; set; }

        public ICollection<Report> Reports { get; set; }
    }
}