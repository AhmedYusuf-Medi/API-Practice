namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetById_ShouldReturn_IssueSelectedById(long issueId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.GetByIdAsync(issueId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(Response<IssueResponseModel>));
                Assert.AreEqual(actual.Payload.Id, issueId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenIssueDoesntExist(long issueId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {

                var sut = new IssueService(assertContext);
                var actual = await sut.GetByIdAsync(issueId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Issue));
                Assert.IsInstanceOfType(actual, typeof(Response<IssueResponseModel>));
            }
        }
    }
}