namespace CarShop.Models.Request.IssuePriority
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class IssuePriorityUpdateRequestModel
    {
        [StringLength(ValidationConstants.Max_Priority_Length, MinimumLength = ValidationConstants.Min_Priority_Length)]
        public string PriorityName { get; set; }

        [Range(ValidationConstants.Min_Severity_Range, ValidationConstants.Max_Severity_Range)]
        public byte? Severity { get; set; }
    }
}