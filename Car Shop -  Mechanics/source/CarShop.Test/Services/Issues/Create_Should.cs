namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("Random description", 1, 1, 1)]
        [DataRow("Random description", 2, 2, 2)]
        public async Task Create_Should_ReturnSucceedResponse(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 0, 1, 1)]
        [DataRow("Random description", long.MaxValue, 2, 2)]
        public async Task Create_ShouldReturnNotSucceedResponse_WhenVehicleDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 1, 0, 1)]
        [DataRow("Random description", 2, long.MaxValue, 2)]
        public async Task Create_ShouldReturnNotSucceedResponse_WhenIssueStatusDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 1, 1, 0)]
        [DataRow("Random description", 2, 2, long.MaxValue)]
        public async Task Create_ShouldReturnNotSucceedResponse_WhenIssuePriorityDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 0, long.MaxValue, 1)]
        [DataRow("Random description", long.MaxValue, 0, 1)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenVehicleAndIssueStatusdDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 1, long.MaxValue, 0)]
        [DataRow("Random description", 2, 0, long.MaxValue)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenIssueStatusAndIssuePrioritydDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Random description", 0, long.MaxValue, 0)]
        [DataRow("Random description", long.MaxValue, 0, long.MaxValue)]
        public async Task Create_Should_NotReturnSucceedResponse_WhenForeignKeysDoesntExist(string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueCreateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}