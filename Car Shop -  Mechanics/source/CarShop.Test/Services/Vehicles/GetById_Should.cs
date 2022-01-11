namespace CarShop.Test.Services.Vehicles
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(4)]
        public async Task GetById_ShouldReturn_VehicleSelectedById(long vehicleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.GetByIdAsync(vehicleId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleResponseModel>));
                Assert.AreEqual(actual.Payload.Id, vehicleId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenVehicleDoesntExist(long vehicleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.GetByIdAsync(vehicleId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleResponseModel>));
            }
        }
    }
}