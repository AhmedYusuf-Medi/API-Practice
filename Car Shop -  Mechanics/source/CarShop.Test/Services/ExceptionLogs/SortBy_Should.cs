namespace CarShop.Test.Services.ExceptionLogs
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Exception;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SortBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 3, true, false)]
        [DataRow(1, 3, false, true)]
        public async Task SortBy_ShouldReturn_CorrectModels(int page, int perPage, bool recently, bool oldest)
        {
            var requestModel = new ExceptionSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Exceptions));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ExceptionLog>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}