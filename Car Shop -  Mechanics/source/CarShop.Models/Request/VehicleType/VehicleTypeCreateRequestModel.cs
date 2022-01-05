namespace CarShop.Models.Request.VehicleType
{
    using CarShop.Models.Base.Common;
    using System.ComponentModel.DataAnnotations;

    public class VehicleTypeCreateRequestModel
    {
        [Required]
        [StringLength(ValidationConstants.Max_Vehicle_Type_Length, MinimumLength = ValidationConstants.Min_Vehicle_Type_Length)]
        public string TypeName { get; set; }
    }
}