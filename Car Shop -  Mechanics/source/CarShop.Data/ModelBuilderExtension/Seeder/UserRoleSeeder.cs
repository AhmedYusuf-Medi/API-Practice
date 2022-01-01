namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class UserRoleSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(UserRoleSeeder)));
            }

            if (await dbContext.UserRoles.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var userRoles = new List<(long userId, long roleId)>
            {
                (1, 2),
                (2, 1),
                (2, 3)
            };

            foreach (var userRole in userRoles)
            {
                await dbContext.UserRoles.AddAsync(new UserRole
                {
                    UserId = userRole.userId,
                    RoleId = userRole.roleId
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}