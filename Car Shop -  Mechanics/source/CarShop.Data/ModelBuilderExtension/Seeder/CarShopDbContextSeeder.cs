namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class CarShopDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(CarShopDbContextSeeder)));
            }

            var seeders = new List<ISeeder>
                          {
                            new RoleSeeder(),
                            new UserSeeder(),
                            new UserRoleSeeder(),
                            new VehicleTypeSeeder(),
                            new VehicleBrandSeeder(),
                            new VehicleSeeder(),
                            new IssueStatusSeeder(),
                            new IssuePrioritySeeder(),
                            new IssueSeeder(),
                            new ReportTypeSeeder(),
                            new ReportSeeder()
                          };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
            }
        }
    }
}