namespace CarShop.Test.Services.IssuePriorities
{
    using CarShop.Data;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssuePriority;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newPriorty", 4)]
        [DataRow("anotherNewPriorty", 5)]
        public async Task Create_Should_ReturnSucceedResponse(string priority, int severity)
        {
            var requestModel = new IssuePriorityCreateRequestModel
            {
                PriorityName = priority,
                Severity = (byte)severity
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.IssuePriority));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}