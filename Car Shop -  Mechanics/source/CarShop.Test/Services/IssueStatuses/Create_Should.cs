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
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newStatus")]
        [DataRow("anotherNewStatus")]
        public async Task Create_Should_ReturnSucceedResponse(string statusName)
        {
            var requestModel = new IssueStatusCreateRequestModel
            {
                StatusName = statusName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.IssueStatus));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}