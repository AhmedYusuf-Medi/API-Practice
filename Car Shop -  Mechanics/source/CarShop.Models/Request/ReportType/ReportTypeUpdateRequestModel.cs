namespace CarShop.Models.Request.ReportType
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class ReportTypeUpdateRequestModel
    {
        [StringLength(ValidationConstants.Max_ReportType_Length, MinimumLength = ValidationConstants.Max_ReportType_Length)]
        public string ReportType { get; set; }
    }
}