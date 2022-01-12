namespace CarShop.Test.Services.ExceptionLogs
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Exception;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class Delete_Should : BaseTest
    {
        [TestMethod]
        [DataRow("b526ce37-8b35-475d-9c18-f2740a935b34")]
        [DataRow("b526ce37-8b35-475d-9c18-f2740a935b35")]
        public async Task Delete_Should_ReturnSucceedResponse(string id)
        {
            var exceptionId = new Guid(id);
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.DeleteAsync(exceptionId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Delete_Succeed, Constants.Exception));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        public async Task Delete_Should_ReturnNotSucceedResponse_WhenExceptionDoesntExist()
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.DeleteAsync(Guid.NewGuid());

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Exception));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}
