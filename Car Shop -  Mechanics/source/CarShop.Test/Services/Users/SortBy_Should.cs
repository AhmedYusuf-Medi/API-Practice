namespace CarShop.Test.Services.Users
{
    using CarShop.Data;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.User;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class SortBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 3, true, false, false, false)]
        [DataRow(1, 3, false, true, false, false)]
        [DataRow(1, 3, false, false, true, false)]
        [DataRow(1, 3, false, false, true, true)]
        [DataRow(1, 3, false, true, true, true)]
        public async Task SortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, bool recently, bool oldest, bool mostActive, bool mostVehicles)
        {
            var requestModel = new UserSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Recently = recently,
                Oldest = oldest,
                MostActive = mostActive,
                MostVehicles = mostVehicles
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.SortByAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Users));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<UserResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}