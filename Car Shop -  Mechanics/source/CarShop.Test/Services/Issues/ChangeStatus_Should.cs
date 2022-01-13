namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class ChangeStatus_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(2, 1)]
        public async Task ChangeStatus_Should_ReturnSucceedResponse(long issueId, long statusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.ChangeStatusAsync(issueId, statusId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Partial_Update_Suceed, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(long.MaxValue, 1)]
        public async Task ChangeStatus_Should_ReturnNotSucceedResponse_WhenIssueDoesntExist(long issueId, long statusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.ChangeStatusAsync(issueId, statusId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(1, 0)]
        [DataRow(2, long.MaxValue)]
        public async Task ChangeStatus_Should_ReturnNotSucceedResponse_WhenIssueStatusDoesntExist(long issueId, long statusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.ChangeStatusAsync(issueId, statusId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(long.MaxValue, 0)]
        [DataRow(0, long.MaxValue)]
        public async Task ChangeStatus_Should_ReturnNotSucceedResponse_WhenArgumentsAreInvalid(long issueId, long statusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.ChangeStatusAsync(issueId, statusId);

                var expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Issue)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}