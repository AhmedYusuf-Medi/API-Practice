﻿namespace CarShop.Test.Services.Users
{
    using CarShop.Data;
    using CarShop.Models.Pagination;
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
    public class GetAll_Should : BaseTest
    {
        [TestMethod]
        [DataRow(3, 1)]
        [DataRow(2, 1)]
        [DataRow(1, 1)]
        public async Task GetAllShould_ReturnUserResponseModels(int perPage, int page)
        {
            var requestModel = new PaginationRequestModel { Page = page, PerPage = perPage};

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.GetAllAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_GetAll_Succeed, Constants.Users));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<UserResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}