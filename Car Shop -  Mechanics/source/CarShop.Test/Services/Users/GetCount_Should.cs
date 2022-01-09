namespace CarShop.Test.Services.Users
{
    using CarShop.Data;
    using CarShop.Service.Data.User;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetCount_Should : BaseTest
    {
        [TestMethod]
        public async Task GetCount_Should_ReturnCorrectResponse()
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.GetCountAsync();

                Assert.AreEqual(5, actual);
            }
        }
    }
}