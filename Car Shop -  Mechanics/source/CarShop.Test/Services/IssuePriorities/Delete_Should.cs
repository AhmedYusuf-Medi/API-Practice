namespace CarShop.Test.Services.IssuePriorities
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssuePriority;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Delete_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Delete_Should_ReturnSucceedResponse(long issuePriorityId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.DeleteAsync(issuePriorityId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Delete_Succeed, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task Delete_Should_ReturnNotSucceedResponse_WhenIssuePriorityDoesntExist(long issuePriorityId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.DeleteAsync(issuePriorityId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}