namespace CarShop.Test.Services.Accounts
{
    using CarShop.Data;
    using CarShop.Models.Response;
    using CarShop.Service.Account.Data;
    using CarShop.Service.Common.Messages;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Threading.Tasks;

    [TestClass]
    public class Verificate_Should : BaseTest
    {
        [TestMethod]
        public async Task Verificate_ShouldSucceedResponse_WhenParametersAreValid()
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);
                var code = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695");
                var email = "verificationTest@gmail.com";

                var actual = await sut.VerificateAsync(email, code);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Email_Verification_Succeed, email));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        public async Task Verificate_ShouldNotSucceedResponse_WhenParametersAreInvalid()
        {
            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);
                var code = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695");
                var email = "verificationTest@gmail.com";

                await sut.VerificateAsync(email, code);
                var actual = await sut.VerificateAsync(email, code);

                Assert.AreEqual(false, actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.Already_Verified);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}