namespace Database.Context.Seeders
{
    public interface ISeeder
    {
        Task SeedAsync(AcademyContext dbContext);
    }
}
