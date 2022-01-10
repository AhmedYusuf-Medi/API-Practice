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
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newBrand")]
        [DataRow(2, "anotherNewBrand")]
        public async Task Update_Should_ReturnSucceedResponse(long vehicleBrandId, string brandName)
        {
            var requestModel = new VehicleBrandCreateRequestModel
            {
                BrandName = brandName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.UpdateAsync(vehicleBrandId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "newBrand")]
        [DataRow(long.MaxValue, "anotherNewBrand")]
        public async Task Update_Should_ReturnNotSucceedResponse_WhenVehicleBrandDoesntExist(long vehicleBrandId, string brandName)
        {
            var requestModel = new VehicleBrandCreateRequestModel
            {
                BrandName = brandName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.UpdateAsync(vehicleBrandId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}