namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IssueSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(IssueSeeder)));
            }

            if (await dbContext.Issues.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var issues = new List<(long vehicleId, long statusId, string description, long priorityId)>
            {
               (1, 3, "Sounds like a trash metal!",2),
               (2, 2, "The wheels are not symetric!",3)
            };

            foreach (var issue in issues)
            {
                await dbContext.Issues.AddAsync(new Issue
                {
                    VehicleId = issue.vehicleId,
                    StatusId = issue.statusId,
                    PriorityId = issue.priorityId,
                    Description = issue.description
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}