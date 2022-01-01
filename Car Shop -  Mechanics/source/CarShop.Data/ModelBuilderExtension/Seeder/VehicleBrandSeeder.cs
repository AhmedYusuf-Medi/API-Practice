namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class VehicleBrandSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(VehicleBrandSeeder)));
            }

            if (await dbContext.VehicleBrands.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var brands = new List<string>
            {
               "BMW",
               "Mercedes",
               "Audi",
               "Opel",
               "Nisan"
            };

            foreach (var brand in brands)
            {
                await dbContext.VehicleBrands.AddAsync(new VehicleBrand
                {
                    Brand = brand
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}