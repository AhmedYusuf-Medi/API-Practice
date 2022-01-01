namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class IssueStatusSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(IssueStatusSeeder)));
            }

            if (await dbContext.IssueStatuses.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var statuses = new List<string>
            {
               "Done",
               "Repairing",
               "Awaiting",
               "No way to repair"
            };

            foreach (var status in statuses)
            {
                await dbContext.IssueStatuses.AddAsync(new IssueStatus
                {
                    Status = status
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}