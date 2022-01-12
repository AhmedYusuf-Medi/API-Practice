namespace CarShop.Test.Services.Vehicles
{
    using CarShop.Data;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 2004, "A3", "AJ1234AJ", 1, 2, 2)]
        public async Task Update_ShouldReturnSucceedResponse(long vechileId, int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleUpdateRequestModel
            {
                Year = year,
                Model = model,
                PlateNumber = plateNumber,
                BrandId = brandId,
                VehicleTypeId = vehicleTypeId,
                OwnerId = ownerId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.UpdateAsync(vechileId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, 2004, "A3", "AJ1234AJ", 1, 2, 2)]
        [DataRow(long.MaxValue, 2004, "A3", "AJ1234AJ", 1, 2, 2)]
        public async Task Update_ShouldReturnNotSucceedResponse_WhenVehicleDoesntExist(long vechileId, int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleUpdateRequestModel
            {
                Year = year,
                Model = model,
                PlateNumber = plateNumber,
                BrandId = brandId,
                VehicleTypeId = vehicleTypeId,
                OwnerId = ownerId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.UpdateAsync(vechileId, requestModel);
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, null, "", "", null, null, null)]
        public async Task Update_ShouldReturnThrowException_WhenArgumentsAreInvalid(long vechileId, int? year, string model, string plateNumber, long? brandId, long? vehicleTypeId, long? ownerId)
        {
            var requestModel = new VehicleUpdateRequestModel
            {
                Year = year,
                Model = model,
                PlateNumber = plateNumber,
                BrandId = brandId,
                VehicleTypeId = vehicleTypeId,
                OwnerId = ownerId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);

                var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.UpdateAsync(vechileId, requestModel));

                Assert.IsNotNull(exception);
                Assert.IsInstanceOfType(exception, typeof(BadRequestException));
                Assert.AreEqual(exception.Message, ExceptionMessages.Arguments_Are_Invalid);
            }
        }

        [TestMethod]
        [DataRow(1, 2022, "A10", "AC1234AC", 2, 2, 2)]
        public async Task Update_Should_ReturnSucceedResponse_WhenPlateNumberAlreadyExistsButOtherArgumentsAreValid(long vehicleId, int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleUpdateRequestModel
            {
                Year = year,
                Model = model,
                PlateNumber = plateNumber,
                BrandId = brandId,
                VehicleTypeId = vehicleTypeId,
                OwnerId = ownerId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.UpdateAsync(vehicleId, requestModel);

                string expectedMessage = $"{string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.PlateNumber), requestModel.PlateNumber)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Vehicle)}";

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}