namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IssuePrioritySeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(IssuePrioritySeeder)));
            }

            if (await dbContext.IssuePriorities.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var priorites = new List<(string priority, byte severity)>
            {
               ("Small", 1),
               ("Medium", 2),
               ("Large", 3)
            };

            foreach (var priority in priorites)
            {
                await dbContext.IssuePriorities.AddAsync(new IssuePriority
                {
                    Priority = priority.priority,
                    Severity = priority.severity
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}