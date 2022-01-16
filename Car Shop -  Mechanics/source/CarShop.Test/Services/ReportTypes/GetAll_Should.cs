namespace CarShop.Test.Services.ReportTypes
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Response;
    using CarShop.Models.Response.ReportType;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.ReportType;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class GetAll_Should : BaseTest
    {
        [TestMethod]
        [DataRow(4, 1)]
        [DataRow(3, 1)]
        [DataRow(2, 1)]
        public async Task GetAllShould_ReturnReportTypeResponseModels(int perPage, int page)
        {
            var requestModel = new PaginationRequestModel { Page = page, PerPage = perPage };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportTypeService(assertContext);
                var actual = await sut.GetAllAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Report_Types));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ReportTypeResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}