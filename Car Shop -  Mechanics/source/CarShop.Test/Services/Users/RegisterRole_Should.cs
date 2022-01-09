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
    public class RegisterRole_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 1)]
        [DataRow(1, 3)]
        public async Task RegisterRole_ShouldReturnSucceedResponse(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.RegisterRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ResponseMessages.Role_Register_Succeed);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(0, 1)]
        [DataRow(long.MaxValue, 3)]
        public async Task RegisterRole_ShouldReturnNotSucceedResponse_WhenGivenUserIdDoesntExist(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.RegisterRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2, long.MaxValue)]
        [DataRow(1, 0)]
        public async Task RegisterRole_ShouldReturnNotSucceedResponse_WhenGivenRoleIdDoesntExist(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);
                var actual = await sut.RegisterRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Role));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow(2, 1)]
        public async Task RegisterRole_ShouldReturnSucceedResponse_ButInsteadOfCreatingNewRoe_ShouldRevive(long userId, long roleId)
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new UserService(assertContext);

                await sut.RemoveRoleAsync(userId, roleId);
                var actual = await sut.RegisterRoleAsync(userId, roleId);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ResponseMessages.Role_Register_Succeed);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}