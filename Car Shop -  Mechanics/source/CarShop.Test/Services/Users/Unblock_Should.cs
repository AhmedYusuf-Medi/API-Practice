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
    public class Unblock_Should : BaseTest
    {
        [TestMethod]
        [DataRow(4)]
        public async Task Unblock_Should_ReturnSucceeedReponse(long userId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.UnblockAsync(userId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.User_UnBlock_Succeed, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0)]
        [DataRow(long.MaxValue)]
        public async Task Unblock_Should_ReturnNotSucceeedReponse_WhenUserDoesntExist(long userId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.UnblockAsync(userId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(3)]
        public async Task Block_Should_ReturnNotSucceeedReponse_WhenUserHaveNoRoles(long userId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.UnblockAsync(userId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.No_Roles);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2)]
        [DataRow(1)]
        public async Task Unblock_Should_ReturnNotSucceeedReponse_WhenUserIsNotBlocked(long userId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.UnblockAsync(userId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.User_Is_Not_Blocked);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

    }
}