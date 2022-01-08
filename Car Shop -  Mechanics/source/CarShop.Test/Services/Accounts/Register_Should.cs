namespace CarShop.Test.Services.Accounts
{
    using CarShop.Data;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Service.Account.Data;
    using CarShop.Service.Common.Messages;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Register_Should : BaseTest
    {
        [TestMethod]
        [DataRow("newEmail@gmail.com", "newUsername", "passwordQ1!")]
        public async Task Register_Should_AddNewUser_WhenParamtersAreValid(string email, string username, string password)
        {
            var requestModel = new UserRegisterRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.RegisterUserAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ResponseMessages.Check_Email_For_Verification);
            }
        }

        [TestMethod]
        [DataRow("newEmail@gmail.com", "newUsername", "passwordQ1!")]
        public async Task Register_ShouldNotAddNewUser_BecauseUserIsOnPending(string email, string username, string password)
        {
            var requestModel = new UserRegisterRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                await sut.RegisterUserAsync(requestModel);
                var actual = await sut.RegisterUserAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ResponseMessages.Check_Email_For_Verification);
            }
        }

        [TestMethod]
        [DataRow("muthasdkabarona@gmail.com", "medysun", "paasdsswordQ1!")]
        public async Task Register_ShouldNotAddNewUser_BecauseUserAlreadyExists(string email, string username, string password)
        {
            var requestModel = new UserRegisterRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                await sut.RegisterUserAsync(requestModel);
                var actual = await sut.RegisterUserAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.Already_Exist, Constants.User));
            }
        }
    }
}