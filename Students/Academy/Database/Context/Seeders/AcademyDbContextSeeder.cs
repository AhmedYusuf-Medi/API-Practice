namespace Database.Context.Seeders
{
    public class AcademyDbContextSeeder : ISeeder
    {
        public async Task SeedAsync(AcademyContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException();
            }

            var seeders = new List<ISeeder>
            {
                new StudentSeeder()
            };

            foreach (var seeder in seeders)
            {
                await seeder.SeedAsync(dbContext);
            }
        }
    }
}
