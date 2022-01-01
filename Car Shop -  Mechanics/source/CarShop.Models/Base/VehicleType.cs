namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.Vehicle_Type)]
    public class VehicleType : DeletableEntity<long>
    {
        public VehicleType()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_Vehicle_Type_Length, MinimumLength = ValidationConstants.Min_Vehicle_Type_Length)]
        public string Type { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}