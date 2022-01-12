namespace CarShop.Test.Services.IssuePriorities
{
    using CarShop.Data;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssuePriority;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newPriorty", 4)]
        [DataRow(2, "anotherNewPriorty", 5)]
        public async Task Update_Should_ReturnSucceedResponse(long issuePriorityId,  string priority, int severity)
        {
            var requestModel = new IssuePriorityUpdateRequestModel
            {
                PriorityName = priority,
                Severity = (byte)severity
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.UpdateAsync(issuePriorityId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "newPriorty", 4)]
        [DataRow(long.MaxValue, "anotherNewPriorty", 5)]
        public async Task Update_Should_ReturnNotSucceedResponse_WhenIssuePriorityDoesntExist(long issuePriorityId, string priority, int severity)
        {
            var requestModel = new IssuePriorityUpdateRequestModel
            {
                PriorityName = priority,
                Severity = (byte)severity
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.UpdateAsync(issuePriorityId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Update_Should_ThrowExceptionWhen_GivenArgumentsAreInvalid(long issuePriorityId)
        {
            var requestModel = new IssuePriorityUpdateRequestModel
            {
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);

                var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.UpdateAsync(issuePriorityId, requestModel));

                Assert.IsNotNull(exception);
                Assert.AreEqual(exception.Message, ExceptionMessages.Arguments_Are_Invalid);
                Assert.IsInstanceOfType(exception, typeof(BadRequestException));
            }
        }
    }
}