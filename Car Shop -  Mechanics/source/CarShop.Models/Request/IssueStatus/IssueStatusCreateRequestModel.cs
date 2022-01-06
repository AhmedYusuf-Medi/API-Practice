namespace CarShop.Models.Request.IssueStatus
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class IssueStatusCreateRequestModel
    {

        [Required]
        [StringLength(ValidationConstants.Max_Status_Length, MinimumLength = ValidationConstants.Min_Status_Length)]
        public string StatusName { get; set; }
    }
}