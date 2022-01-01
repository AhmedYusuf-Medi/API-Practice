namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VehicleTypeSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(VehicleTypeSeeder)));
            }

            if (await dbContext.VehicleTypes.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var vehicleTypes = new List<string>
            {
               "Car",
               "Truck",
               "Motorcycle",
            };

            foreach (var vehicleType in vehicleTypes)
            {
                await dbContext.VehicleTypes.AddAsync(new VehicleType
                {
                    Type = vehicleType
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}