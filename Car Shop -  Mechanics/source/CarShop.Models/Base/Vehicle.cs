namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Vehicle : DeletableEntity<long>
    {
        public Vehicle()
        {
            this.Issues = new HashSet<Issue>();
        }

        [Required]
        [Range(ValidationConstants.Year_Of_First_Care, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        [StringLength(ValidationConstants.Max_Model_Length, MinimumLength = ValidationConstants.Min_Model_Length)]
        public string Model { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.Plate_Number_Regex, ErrorMessage = ValidationConstants.Invalid_Plate_Number)]
        public string PlateNumber { get; set; }

        //[Required]
        public string PicturePath { get; set; }

        public string PictureId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long BrandId { get; set; }

        [ForeignKey(nameof(BrandId))]
        public VehicleBrand Brand { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long VehicleTypeId { get; set; }

        [ForeignKey(nameof(VehicleTypeId))]
        public VehicleType VehicleType { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long OwnerId { get; set; }

        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; }

        public ICollection<Issue> Issues { get; set; }
    }
}