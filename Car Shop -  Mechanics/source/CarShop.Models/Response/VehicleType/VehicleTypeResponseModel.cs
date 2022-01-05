using System;

namespace CarShop.Models.Response.VehicleType
{
    public class VehicleTypeResponseModel
    {
        public long Id { get; set; }

        public string TypeName { get; set; }

        public long RegisteredVehiclesAtShop { get; set; }

        public DateTime RegisteredSince { get; set; }
    }
}