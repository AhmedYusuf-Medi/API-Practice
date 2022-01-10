namespace CarShop.Test.Services.IssueStatuses
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
    using CarShop.Models.Response;
    using CarShop.Models.Response.IssueStatus;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.IssueStatus;
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
        public async Task GetAllShould_ReturnIssueStatusResponseModels(int perPage, int page)
        {
            var requestModel = new PaginationRequestModel { Page = page, PerPage = perPage };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueStatusService(assertContext);
                var actual = await sut.GetAllAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.IssueStatuses));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<IssueStatusResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}