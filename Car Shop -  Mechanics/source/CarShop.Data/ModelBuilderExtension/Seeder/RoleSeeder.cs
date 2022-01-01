namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class RoleSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(RoleSeeder)));
            }

            if (await dbContext.Roles.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var roles = new List<string>() { "Admin", "User", "Mechanic", "Blocked", "Pending" };

            foreach (var role in roles)
            {
                await dbContext.Roles.AddAsync(new Role
                {
                    Type = role
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}