namespace CarShop.Test.Services.Report
{
    using CarShop.Data;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Report;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SortBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 2, true, false)]
        [DataRow(1, 2, false, true)]
        public async Task SortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, bool recently, bool oldest)
        {
            var requestModel = new ReportSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Reports));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ReportResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}