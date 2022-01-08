namespace CarShop.Test.Services.Base
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Providers.Cloudinary;
    using CarShop.Service.Common.Providers.Mail.SendGrid;
    using CarShop.Test.Storage;
    using CloudinaryDotNet;
    using Microsoft.Data.Sqlite;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using Moq;
    using System.Threading.Tasks;

    public abstract class BaseTest
    {
        protected DbContextOptions<CarShopDbContext> Options { get; set; }

        protected ICloudinaryService CloudinaryService { get; set; }

        protected IMailSender<InfoResponse> MailService { get; set; }

        private SqliteConnection connection = new SqliteConnection("DataSource=:memory:");

        [TestInitialize]
        public async Task Initialize()
        {
            var mockedCloudinary = new Mock<Cloudinary>();
            var mockCloudinaryService = new Mock<CloudinaryService>(mockedCloudinary);
            this.CloudinaryService = mockCloudinaryService as ICloudinaryService;

            var mockMailService = new Mock<IMailSender<InfoResponse>>();
            this.MailService = mockMailService.Object;

            this.connection.Open();

            this.Options = new DbContextOptionsBuilder<CarShopDbContext>().UseSqlite(connection).Options;

            using (var dbContext = new CarShopDbContext(Options))
            {
                dbContext.Database.EnsureCreated();

                await Seeder.SeedAsync(dbContext);
                await dbContext.SaveChangesAsync();
            }
        }
    }
}