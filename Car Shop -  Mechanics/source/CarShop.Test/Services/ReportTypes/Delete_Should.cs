namespace CarShop.Test.Services.ReportTypes
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.ReportType;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Delete_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task Delete_Should_ReturnSucceedResponse(long reportTypeId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.DeleteAsync(reportTypeId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Delete_Succeed, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task Delete_Should_ReturnNotSucceedResponse_WhenReportTypeDoesntExist(long reportTypeId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.DeleteAsync(reportTypeId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}