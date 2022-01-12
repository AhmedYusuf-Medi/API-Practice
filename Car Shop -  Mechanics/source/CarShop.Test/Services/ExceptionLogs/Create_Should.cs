namespace CarShop.Test.Services.ExceptionLogs
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Exception;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("exMessage", "innerEx", "fromNasaToPlovdiv")]
        public async Task Create_Should_ReturnSucceedResponse(string message, string innerEx, string stackTrace)
        {
            var requestModel = new ExceptionLog
            {
                ExceptionMessage = message,
                InnerException = innerEx,
                StackTrace = stackTrace
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Exception));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}