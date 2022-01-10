namespace CarShop.Test.Services.VehicleBrands
{
    using CarShop.Data;
    using CarShop.Models.Request.VehicleBrand;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleBrand;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newBrand")]
        [DataRow("anotherNewBrand")]
        public async Task Create_Should_ReturnSucceedResponse(string brandName)
        {
            var requestModel = new VehicleBrandCreateRequestModel
            {
                BrandName = brandName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}