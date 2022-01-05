namespace CarShop.Models.Response.VehicleBrand
{
    using System;

    public class VehicleBrandResponseModel
    {
        public long Id { get; set; }

        public string BrandName { get; set; }

        public long RegisteredVehiclesAtShop { get; set; }

        public DateTime RegisteredSince { get; set; }
    }
}