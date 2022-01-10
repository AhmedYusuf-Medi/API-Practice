namespace CarShop.Test.Services.VehicleBrands
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleBrand;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Delete_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Delete_Should_ReturnSucceedResponse(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.DeleteAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Delete_Succeed, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task Delete_Should_ReturnNotSucceedResponse_WhenVehicleBrandDoesntExist(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.DeleteAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}