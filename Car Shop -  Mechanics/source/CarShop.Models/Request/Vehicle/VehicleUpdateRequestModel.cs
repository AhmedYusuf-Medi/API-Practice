namespace CarShop.Models.Request.Vehicle
{
    using CarShop.Models.Base.Common;
    using Microsoft.AspNetCore.Http;
    using System.ComponentModel.DataAnnotations;

    public class VehicleUpdateRequestModel
    {
        [Range(ValidationConstants.Year_Of_First_Care, int.MaxValue)]
        public int? Year { get; set; }

        [StringLength(ValidationConstants.Max_Model_Length, MinimumLength = ValidationConstants.Min_Model_Length)]
        public string Model { get; set; }

        [RegularExpression(ValidationConstants.Plate_Number_Regex, ErrorMessage = ValidationConstants.Invalid_Plate_Number)]
        public string PlateNumber { get; set; }

        public IFormFile VehiclePhoto { get; set; }

        public long? BrandId { get; set; }

        public long? VehicleTypeId { get; set; }

        public long? OwnerId { get; set; }
    }
}