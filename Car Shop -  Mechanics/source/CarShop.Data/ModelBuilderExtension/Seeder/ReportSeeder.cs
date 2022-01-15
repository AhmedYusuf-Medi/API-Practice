namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReportSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (await dbContext.Reports.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var reports = new HashSet<(int reportTypeId, long senderId, long receiverId, string description)>
            {
                (1, 1, 2, "He did not paid at time once again!"),
                (2, 2, 1, "He is blaming me for something that did not happend!"),
            };

            foreach (var report in reports)
            {
                await dbContext.Reports.AddAsync(new Report()
                {
                    ReportTypeId = report.reportTypeId,
                    ReceiverId = report.receiverId,
                    SenderId = report.senderId,
                    Description = report.description
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}