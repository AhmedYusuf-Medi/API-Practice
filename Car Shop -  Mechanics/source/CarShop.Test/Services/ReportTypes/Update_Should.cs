namespace CarShop.Test.Services.ReportTypes
{
    using CarShop.Data;
    using CarShop.Models.Request.ReportType;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.ReportType;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Update_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newReportType")]
        [DataRow(2, "anotherReportType")]
        public async Task Update_Should_ReturnSucceedResponse(long reportTypeId, string reportType)
        {
            var requestModel = new ReportTypeUpdateRequestModel
            {
                ReportType = reportType
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.UpdateAsync(reportTypeId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Edit_Succeed, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, "newReportType")]
        [DataRow(long.MaxValue, "anotherReportType")]
        public async Task Update_Should_ReturnNotSucceedResponse_WhenReportTypeDoesntExist(long reportTypeId, string reportType)
        {
            var requestModel = new ReportTypeUpdateRequestModel
            {
                ReportType = reportType
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.UpdateAsync(reportTypeId, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}