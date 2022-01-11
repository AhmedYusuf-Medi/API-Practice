namespace CarShop.Test.Services.Vehicles
{
    using CarShop.Data;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow(2004, "A3", "AJ1234AJ", 1, 2, 2)]
        public async Task Create_Should_ReturnSucceedResponse(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AJ1234AJ", 0, 2, 2)]
        [DataRow(2004, "A3", "AJ1234AJ", long.MaxValue, 2, 2)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenBrandDoesntExist(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleBrand));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AJ1234AJ", 1, 0, 2)]
        [DataRow(2004, "A3", "AJ1234AJ", 2, long.MaxValue, 2)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenTypeDoesntExist(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleType));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AJ1234AJ", 1, 1, 0)]
        [DataRow(2004, "A3", "AJ1234AJ", 2, 2, long.MaxValue)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenUserDoesntExist(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AJ1234AJ", long.MaxValue, 1, 0)]
        [DataRow(2004, "A3", "AJ1234AJ", 0, 2, long.MaxValue)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenUserAndBrandDoesntExist(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                string message = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleBrand)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, message);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AB1234AB", 1, 1, 1)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenPlateNumberAlreadyExists(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.Already_Exist, nameof(requestModel.PlateNumber)));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2004, "A3", "AB1234AB", 1, 0, 1)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenPlateNumberAlreadyExistsAndVehicleTypeDoesntExist(int year, string model, string plateNumber, long brandId, long vehicleTypeId, long ownerId)
        {
            var requestModel = new VehicleCreateRequestModel
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
                var actual = await sut.CreateAsync(requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.VehicleType)}\n{string.Format(ExceptionMessages.Already_Exist, nameof(requestModel.PlateNumber))}";

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}