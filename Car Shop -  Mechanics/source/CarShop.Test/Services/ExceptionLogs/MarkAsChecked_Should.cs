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
    public class MarkAsChecked_Should : BaseTest
    {
        [TestMethod]
        [DataRow("b526ce37-8b35-475d-9c18-f2740a935b34")]
        [DataRow("b526ce37-8b35-475d-9c18-f2740a935b35")]
        public async Task MarkAsChecked_Should_ReturnSucceedResponseAndMarkExceptionAsChecked(string id)
        {
            var exceptionId = new Guid(id);
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.MarkAsCheckedAsync(exceptionId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Exception_Checked, Constants.Exception));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        public async Task MarkAsChecked_Should_NotReturnSucceedResponseWhenExceptionDoesntExist()
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.MarkAsCheckedAsync(Guid.NewGuid());

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Exception));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}