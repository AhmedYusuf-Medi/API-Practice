namespace CarShop.Test.Services.VehicleBrands
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.VehicleBrand;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.VehicleBrand;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetById_ShouldReturn_VehicleBrandSelectedById(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.GetByIdAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleBrandResponseModel>));
                Assert.AreEqual(actual.Payload.Id, vehicleBrandId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenVehicleBrandDoesntExist(long vehicleBrandId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleBrandService(assertContext);
                var actual = await sut.GetByIdAsync(vehicleBrandId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(Response<VehicleBrandResponseModel>));
            }
        }
    }
}