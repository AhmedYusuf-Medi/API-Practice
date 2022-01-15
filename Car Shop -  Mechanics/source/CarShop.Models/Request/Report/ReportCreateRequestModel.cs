namespace CarShop.Models.Request.Report
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class ReportCreateRequestModel
    {
        [Required]
        [StringLength(ValidationConstants.Max_Report_Description_Length, MinimumLength = ValidationConstants.Min_Report_Description_Length)]
        public string Description { get; set; }

        [Range(1, long.MaxValue)]
        public long SenderId { get; set; }

        [Range(1, long.MaxValue)]
        public long ReceiverId { get; set; }

        [Range(1, int.MaxValue)]
        public int ReportTypeId { get; set; }
    }
}