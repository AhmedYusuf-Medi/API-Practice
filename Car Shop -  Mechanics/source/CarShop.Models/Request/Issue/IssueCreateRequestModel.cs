namespace CarShop.Models.Request.Issue
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class IssueCreateRequestModel
    {
        [Required]
        [StringLength(int.MaxValue, MinimumLength = ValidationConstants.Min_Issue_Description_Length)]
        public string Description { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long VehicleId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long StatusId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long PriorityId { get; set; }
    }
}