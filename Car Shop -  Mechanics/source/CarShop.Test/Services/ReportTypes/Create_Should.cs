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
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newReportType")]
        [DataRow("anotherReportType")]
        public async Task Create_Should_ReturnSucceedResponse(string typeName)
        {
            var requestModel = new ReportTypeCreateRequestModel
            {
                ReportType = typeName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}