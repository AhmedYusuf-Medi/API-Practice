namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Report : DeletableEntity<long>
    {
        [Required]
        [StringLength(ValidationConstants.Max_Report_Description_Length, MinimumLength = ValidationConstants.Min_Report_Description_Length)]
        public string Description { get; set; }

        [Range(1, long.MaxValue)]
        public long SenderId { get; set; }
        [ForeignKey(nameof(SenderId))]
        public User Sender { get; set; }

        [Range(1, long.MaxValue)]
        public long ReceiverId { get; set; }
        [ForeignKey(nameof(ReceiverId))]
        public User Receiver { get; set; }

        [Range(1, int.MaxValue)]
        public int ReportTypeId { get; set; }
        [ForeignKey(nameof(ReportTypeId))]
        public ReportType ReportType { get; set; }
    }
}