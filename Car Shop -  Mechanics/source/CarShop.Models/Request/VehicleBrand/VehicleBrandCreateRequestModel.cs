namespace CarShop.Models.Request.VehicleBrand
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class VehicleBrandCreateRequestModel
    {
        [Required]
        [StringLength(ValidationConstants.Max_Vehicle_Brand_Length, MinimumLength = ValidationConstants.Min_Vehicle_Brand_Length)]
        public string BrandName { get; set; }
    }
}