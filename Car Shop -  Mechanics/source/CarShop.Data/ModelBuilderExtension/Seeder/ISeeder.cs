namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using System.Threading.Tasks;

    public interface ISeeder
    {
        Task SeedAsync(CarShopDbContext dbContext);
    }
}