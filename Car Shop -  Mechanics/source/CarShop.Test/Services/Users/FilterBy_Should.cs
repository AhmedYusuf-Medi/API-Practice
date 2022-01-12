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
    public class FilterBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 3, "", "gmail", "", true, false, false, false)]
        [DataRow(1, 3, "", "gmail", "", false, true, false, false)]
        [DataRow(1, 2, "a", "", "", false, true, false, false)]
        [DataRow(1, 1, "", "", "admin", false, false, true, false)]
        public async Task FilterAndSortBy_ShouldReturn_CorrectReponseModels(int page, int perPage, string username, string email, 
            string role, bool recently, bool oldest, bool mostActive, bool mostVehicles)
        {
            var requestModel = new UserFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Email = email,
                Username = username,
                Role = role,
                Recently = recently,
                Oldest = oldest,
                MostActive = mostActive,
                MostVehicles = mostVehicles
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(email))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Email.Contains(email));
                    }
                }

                if (!string.IsNullOrEmpty(username))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Username.Contains(username));
                    }
                }

                if (!string.IsNullOrEmpty(role))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Roles.Any(role => role.Contains(role)));
                    }
                }

                var expectedMessage = $"{string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Users)}\n{string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Users)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<UserResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }

        [TestMethod]
        [DataRow(1, 3, "", "gmail", "")]
        [DataRow(1, 3, "", "gmail", "")]
        [DataRow(1, 2, "a", "", "")]
        [DataRow(1, 1, "", "", "admin")]
        public async Task FilterBy_ShouldReturn_CorrectReponseModels(int page, int perPage, string username, string email, string role)
        {
            var requestModel = new UserFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Email = email,
                Username = username,
                Role = role
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(email))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Email.Contains(email));
                    }
                }

                if (!string.IsNullOrEmpty(username))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Username.Contains(username));
                    }
                }

                if (!string.IsNullOrEmpty(role))
                {
                    foreach (var user in actual.Payload.Entities)
                    {
                        Assert.IsTrue(user.Roles.Any(role => role.Contains(role)));
                    }
                }

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Users));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<UserResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}