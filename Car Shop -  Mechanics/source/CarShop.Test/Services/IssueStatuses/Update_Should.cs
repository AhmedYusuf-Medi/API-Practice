namespace CarShop.Test.Services.IssueStatuses
{
    using CarShop.Data;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssueStatus;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newStatus")]
        [DataRow(2, "anotherNewStatus")]
        public async Task Update_Should_ReturnSucceedResponse(long issueStatusId, string statusName)
        {
            var requestModel = new IssueStatusCreateRequestModel
            {
                StatusName = statusName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.UpdateAsync(issueStatusId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "newStatus")]
        [DataRow(long.MaxValue, "anotherNewStatus")]
        public async Task Update_Should_ReturnNotSucceedResponse_WhenIssueStatusDoesntExist(long issueStatusId, string statusName)
        {
            var requestModel = new IssueStatusCreateRequestModel
            {
                StatusName = statusName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.UpdateAsync(issueStatusId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}
