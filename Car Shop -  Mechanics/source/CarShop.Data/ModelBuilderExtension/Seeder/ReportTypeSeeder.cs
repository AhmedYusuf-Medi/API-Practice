namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ReportTypeSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (await dbContext.ReportTypes.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var reportTypes = new HashSet<string>
            {
                "Negative Attitude",
                "Verbal Abuse",
                "Hate Speech",
                "Offensive"
            };

            foreach (var report in reportTypes)
            {
                await dbContext.ReportTypes.AddAsync(new ReportType()
                {
                    Type = report
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}