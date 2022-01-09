namespace CarShop.Test.Services.Users
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.User;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class RemoveRole_Should : BaseTest
    {
        [TestMethod]
        [DataRow(2, 1)]
        [DataRow(1, 2)]
        public async Task RemoveRole_ShouldReturnSucceedResponse(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.RemoveRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Delete_Succeed, Constants.Role));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2, 0)]
        [DataRow(1, long.MaxValue)]
        [DataRow(0, 1)]
        [DataRow(long.MaxValue, 2)]
        public async Task RemoveRole_ShouldReturnNotSucceedResponse_WhenUserRoleDoesntExist(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.RemoveRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Role));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}