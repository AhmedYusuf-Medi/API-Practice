namespace CarShop.Test.Services.IssueStatuses
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssueStatus;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetById_ShouldReturn_IssueStatusSelectedById(long issueStatusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.GetByIdAsync(issueStatusId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(Response<IssueStatusResponseModel>));
                Assert.AreEqual(actual.Payload.Id, issueStatusId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenIssueStatusDoesntExist(long issueStatusId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.GetByIdAsync(issueStatusId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(Response<IssueStatusResponseModel>));
            }
        }
    }
}