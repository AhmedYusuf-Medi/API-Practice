namespace CarShop.Models.Request.ReportType
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class ReportTypeCreateRequestModel
    {
        [Required]
        [StringLength(ValidationConstants.Max_ReportType_Length, MinimumLength = ValidationConstants.Max_ReportType_Length)]
        public string ReportType { get; set; }
    }
}