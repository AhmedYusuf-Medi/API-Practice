namespace CarShop.Models.Response.Vehicle
{
    using System;

    public class VehicleResponseModel
    {
        public long Id { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string Owner { get; set; }

        public string OwnerMail { get; set; }

        public string PicturePath { get; set; }

        public string VehicleType { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string PlateNumber { get; set; }

        public long IssueCount { get; set; }
    }
}