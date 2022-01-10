namespace CarShop.Test.Services.VehicleTypes
{
    using CarShop.Data;
    using CarShop.Models.Request.VehicleType;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleType;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newType")]
        [DataRow(2, "anotherNewType")]
        public async Task Update_Should_ReturnSucceedResponse(long vehicleTypeId, string typeName)
        {
            var requestModel = new VehicleTypeCreateRequestModel
            {
                TypeName = typeName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleTypeService(assertContext);
                var actual = await sut.UpdateAsync(vehicleTypeId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "newType")]
        [DataRow(long.MaxValue, "anotherNewType")]
        public async Task Update_Should_ReturnNotSucceedResponse_WhenVehicleBrandDoesntExist(long vehicleTypeId, string typeName)
        {
            var requestModel = new VehicleTypeCreateRequestModel
            {
                TypeName = typeName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleTypeService(assertContext);
                var actual = await sut.UpdateAsync(vehicleTypeId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}