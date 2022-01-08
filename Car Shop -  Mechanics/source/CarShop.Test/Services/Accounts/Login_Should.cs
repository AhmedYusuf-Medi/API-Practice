namespace CarShop.Test.Services.Accounts
{
    using CarShop.Data;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Models.Response.User;
    using CarShop.Service.Account.Data;
    using CarShop.Service.Common.Messages;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Login_Should : BaseTest
    {
        [TestMethod]
        [DataRow("ahhasmed.usuf@gmail.com", "passwordQ1!")]
        [DataRow("muthasdkabarona@gmail.com", "passwordQ1!")]
        public async Task Login_Should_Return_SucceedResponse_WhenParameters_AreCorrect(string email, string password)
        {
            var requestModel = new UserLoginRequestModel() { Email = email, Password = password};

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.LoginAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(Response<UserResponseModel>));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ResponseMessages.Login_Succeed);
                Assert.IsNotNull(actual.Payload);
            }
        }

        [TestMethod]
        [DataRow("aahhasmed.usuf@gmail.com", "passwordQ1!")]
        [DataRow("mmuthasdkabarona@gmail.com", "passwordQ1!")]
        public async Task Login_Should_Return_NotSucceedResponse_WhenEmail_DoesntExist(string email, string password)
        {
            var requestModel = new UserLoginRequestModel() { Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.LoginAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(Response<UserResponseModel>));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsNull(actual.Payload);
            }
        }

        [TestMethod]
        [DataRow("ahhasmed.usuf@gmail.com", "passwordQ11!")]
        [DataRow("muthasdkabarona@gmail.com", "passwordQ11!")]
        public async Task Login_Should_Return_NotSucceedResponse_WhenPassword_IsInvalid(string email, string password)
        {
            var requestModel = new UserLoginRequestModel() { Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.LoginAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(Response<UserResponseModel>));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.Invalid_Password);
                Assert.IsNull(actual.Payload);
            }
        }
    }
}