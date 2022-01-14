namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
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
        [DataRow(1, 3, false, false, false)]
        [DataRow(1, 3, false, false, false)]
        [DataRow(1, 3, false, false, false)]
        [DataRow(1, 3, true, false, true)]
        [DataRow(1, 3, false, true, false)]
        public async Task SortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, bool recently, bool oldest, bool bySeverity)
        {
            var requestModel = new IssueSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
                BySeverity = bySeverity
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Issues));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<IssueResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}