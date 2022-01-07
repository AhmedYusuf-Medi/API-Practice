namespace CarShop.Models.Request.Issue
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class IssueUpdateRequestModel
    {
        [StringLength(int.MaxValue, MinimumLength = ValidationConstants.Min_Issue_Description_Length)]
        public string Description { get; set; }

        [Range(1, long.MaxValue)]
        public long? VehicleId { get; set; }

        [Range(1, long.MaxValue)]
        public long? StatusId { get; set; }

        [Range(1, long.MaxValue)]
        public long? PriorityId { get; set; }
    }
}