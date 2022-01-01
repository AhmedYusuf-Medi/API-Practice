namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VehicleSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(VehicleSeeder)));
            }

            if (await dbContext.Vehicles.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var vehicles = new List<Vehicle>
            {
               new Vehicle{Year = 2005, Model = "A4", PlateNumber = "AB1234AB", BrandId = 3, VehicleTypeId = 1, OwnerId = 1, PicturePath = "" },
               new Vehicle{Year = 2005, Model = "Astra", PlateNumber = "AC1234AC", BrandId = 4, VehicleTypeId = 1, OwnerId = 1, PicturePath = "" }
            };

            foreach (var vehicle in vehicles)
            {
                await dbContext.Vehicles.AddAsync(vehicle);

                await dbContext.SaveChangesAsync();
            }
        }
    }
}