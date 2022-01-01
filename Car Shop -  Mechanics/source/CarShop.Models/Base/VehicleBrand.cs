namespace CarShop.Models.Base
{
    using CarShop.Models.Base.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table(ValidationConstants.Vehicle_Brand)]
    public class VehicleBrand : DeletableEntity<long>
    {
        public VehicleBrand()
        {
            this.Vehicles = new HashSet<Vehicle>();
        }

        [Required]
        [StringLength(ValidationConstants.Max_Vehicle_Brand_Length, MinimumLength = ValidationConstants.Min_Vehicle_Brand_Length)]
        public string Brand { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }
    }
}