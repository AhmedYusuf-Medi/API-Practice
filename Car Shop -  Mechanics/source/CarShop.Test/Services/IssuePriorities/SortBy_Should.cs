namespace CarShop.Test.Services.IssuePriorities
{
    using CarShop.Data;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssuePriority;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssuePriority;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SortBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 3, true, false, false)]
        [DataRow(1, 3, false, true, false)]
        [DataRow(1, 3, false, false, true)]
        [DataRow(1, 3, false, true, true)]
        [DataRow(1, 3, true, false, true)]
        public async Task SortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, bool recently, bool oldest, bool mostUsed)
        {
            var requestModel = new IssuePrioritySortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
                MostUsed = mostUsed
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssuePriorityService(assertContext);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.IssuePriorities));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<IssuePriorityResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}