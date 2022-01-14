namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "Random description", 3, 1, 1)]
        [DataRow(2, "Random description", 2, 2, 2)]
        public async Task Create_Should_ReturnSucceedResponse(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "Random description", 1, 1, 1)]
        [DataRow(long.MaxValue, "Random description", 2, 2, 2)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenIssueDoesntExist(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, "Random description", 0, 1, 1)]
        [DataRow(2, "Random description", long.MaxValue, 2, 2)]
        public async Task Create_Should_ReturnSucceedResponse_WhenVehicleDoesntExist(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, "Random description", 3, 0, 1)]
        [DataRow(2, "Random description", 1, long.MaxValue, 2)]
        public async Task Create_Should_ReturnSucceedResponse_WhenIssueStatusDoesntExist(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, "Random description", 3, 2, 0)]
        [DataRow(2, "Random description", 1, 1, long.MaxValue)]
        public async Task Create_Should_ReturnSucceedResponse_WhenIssuePriorityDoesntExist(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, "Random description", 0, long.MaxValue, 0)]
        [DataRow(2, "Random description", long.MaxValue, 0, long.MaxValue)]
        public async Task Create_Should_ReturnSucceedResponse_WhenForeignKeysDoesntExist(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.UpdateAsync(issueId, requestModel);

                string expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Vehicle)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.Issue)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, "", 0, long.MaxValue, 0)]
        [DataRow(2, "", long.MaxValue, 0, long.MaxValue)]
        public async Task Create_Should_ThrowExceptionWhenGivenArgumentsAreInvalid(long issueId, string description, long vehicleId, long statusId, long priorityId)
        {
            var requestModel = new IssueUpdateRequestModel
            {
                Description = description,
                VehicleId = vehicleId,
                StatusId = statusId,
                PriorityId = priorityId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.UpdateAsync(issueId, requestModel));
            
                Assert.IsNotNull(exception);
                Assert.AreEqual(exception.Message, ExceptionMessages.Arguments_Are_Invalid);
                Assert.IsInstanceOfType(exception, typeof(BadRequestException));
            }
        }
    }
}