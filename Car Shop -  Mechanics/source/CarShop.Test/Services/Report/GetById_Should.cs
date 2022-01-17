namespace CarShop.Test.Services.Report
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Report;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class GetById_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1)]
        [DataRow(2)]
        public async Task GetById_ShouldReturn_ReportSelectedById(long reportId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.GetByIdAsync(reportId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Get_Succeed, Constants.Report));
                Assert.IsInstanceOfType(actual, typeof(Response<ReportResponseModel>));
                Assert.AreEqual(actual.Payload.Id, reportId);
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task GetById_ShouldNotSucceedResponse_WhenReportDoesntExist(long reportId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.GetByIdAsync(reportId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Report));
                Assert.IsInstanceOfType(actual, typeof(Response<ReportResponseModel>));
            }
        }
    }
}