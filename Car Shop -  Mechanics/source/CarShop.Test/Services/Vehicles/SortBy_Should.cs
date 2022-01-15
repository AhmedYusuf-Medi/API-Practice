namespace CarShop.Test.Services.Vehicles
{
    using CarShop.Data;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SortBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 4, true, false, false, false, false, false)]
        [DataRow(1, 4, false, true, false, false, false, false)]
        [DataRow(1, 4, false, false, true, false, false, false)]
        [DataRow(1, 4, false, false, false, true, false, false)]
        [DataRow(1, 4, false, false, false, false, true, false)]
        [DataRow(1, 4, false, false, false, false, false, true)]
        [DataRow(1, 4, true, false, true, false, false, true)]
        [DataRow(1, 4, false, true, false, true, true, false)]
        public async Task SortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, bool recently, bool oldest, bool yearAsc, bool yearDes, bool mostIssues, bool lessIssues)
        {
            var requestModel = new VehicleSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
                ByYearAsc = yearAsc,
                ByYearDesc = yearDes,
                MostIssues = mostIssues,
                LessIssues = lessIssues
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Vehicles));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<VehicleResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}