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
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newType")]
        [DataRow("anotherNewType")]
        public async Task Create_Should_ReturnSucceedResponse(string typeName)
        {
            var requestModel = new VehicleTypeCreateRequestModel
            {
                TypeName = typeName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleTypeService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}