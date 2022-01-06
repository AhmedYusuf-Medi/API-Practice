namespace CarShop.Models.Request.Vehicle
{
    using CarShop.Models.Base.Common;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class VehicleCreateRequestModel
    {
        [Required]
        [Range(ValidationConstants.Year_Of_First_Care, int.MaxValue)]
        public int Year { get; set; }

        [Required]
        [StringLength(ValidationConstants.Max_Model_Length, MinimumLength = ValidationConstants.Min_Model_Length)]
        public string Model { get; set; }

        [Required]
        [RegularExpression(ValidationConstants.Plate_Number_Regex, ErrorMessage = ValidationConstants.Invalid_Plate_Number)]
        public string PlateNumber { get; set; }

        [Required]
        public IFormFile VehiclePhoto { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long BrandId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long VehicleTypeId { get; set; }

        [Required]
        [Range(1, long.MaxValue)]
        public long OwnerId { get; set; }
    }
}