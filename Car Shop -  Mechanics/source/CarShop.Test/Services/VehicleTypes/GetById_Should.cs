namespace CarShop.Test.Services.VehicleTypes
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleType;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleType;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetById_ShouldReturn_VehicleTypeSelectedById(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleTypeService(assertContext);
                var actual = await sut.GetByIdAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleTypeResponseModel>));
                Assert.AreEqual(actual.Payload.Id, vehicleBrandId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenVehicleTypeDoesntExist(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleTypeService(assertContext);
                var actual = await sut.GetByIdAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleTypeResponseModel>));
            }
        }
    }
}