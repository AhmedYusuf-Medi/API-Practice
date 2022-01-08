namespace CarShop.Test.Services.Accounts
{
    using CarShop.Data;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response;
    using CarShop.Service.Account.Data;
    using CarShop.Service.Common.Exceptions;
    using CarShop.Service.Common.Messages;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class EditProfile_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, "newemail@gmail.com", "newusername", "ppaswordQ1!")]
        [DataRow(2, "newemail@gmail.com", "newusername", "ppaswordQ1!")]
        public async Task EditProfile_ShouldReturn_SucceedResponse_WhenParameters_AreCorrect(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username,  Email = email, Password = password};

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.User));
            }
        }

        [TestMethod]
        [DataRow(0, "newemail@gmail.com", "newusername", "ppaswordQ1!")]
        [DataRow(long.MaxValue, "newemail@gmail.com", "newusername", "ppaswordQ1!")]
        public async Task EditProfile_ShouldReturn_NotSucceedResponse_WhenUser_DoesntExist(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
            }
        }

        [TestMethod]
        [DataRow(1, "", "", "")]
        public async Task EditProfile_ShouldThrow_Exception_WhenPassedArguments_AreInvalid(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var exception = await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.EditProfileAsync(id, requestModel));

                Assert.AreEqual(exception.Message, ExceptionMessages.Arguments_Are_Invalid);
                await Assert.ThrowsExceptionAsync<BadRequestException>(() => sut.EditProfileAsync(id, requestModel));
            }
        }

        [TestMethod]
        [DataRow(1, "", "medysun", "")]
        public async Task EditProfile_ShouldNotUpdateUsername_WhenItIsAlreadyUsed(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Username), requestModel.Username));
            }
        }

        [TestMethod]
        [DataRow(1, "muthasdkabarona@gmail.com", "", "")]
        public async Task EditProfile_ShouldNotUpdateEmail_WhenItIsAlreadyUsed(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Email), requestModel.Email));
            }
        }

        [TestMethod]
        [DataRow(1, "muthasdkabarona@gmail.com", "newname", "")]
        public async Task EditProfile_ShouldUpdateUsername_ButNotEmail_BecauseItIsAlreadyUsed(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                string expectedMessage = $"{string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Email), requestModel.Email)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.User)}";
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
            }
        }

        [TestMethod]
        [DataRow(1, "newmail@gmail.com", "medysun", "")]
        public async Task EditProfile_ShouldUpdateEmail_ButNotUsername_BecauseItIsAlreadyUsed(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                string expectedMessage = $"{string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Username), requestModel.Username)}\n{string.Format(ResponseMessages.Entity_Partial_Edit_Succeed, Constants.User)}";
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
            }
        }

        [TestMethod]
        [DataRow(1, "muthasdkabarona@gmail.com", "medysun", "")]
        public async Task EditProfile_ShouldNotUpdate_BecauseUsernameAndEmail_AreAlreadyUsed(long id, string email, string username, string password)
        {
            var requestModel = new UserEditRequestModel() { Username = username, Email = email, Password = password };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new AccountService(assertContext, this.MailService, this.CloudinaryService, this.PasswordHasher);

                var actual = await sut.EditProfileAsync(id, requestModel);

                string expectedMessage = $"{string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Username), requestModel.Username)}\n{string.Format(ResponseMessages.Entity_Property_Is_Taken, nameof(requestModel.Email), requestModel.Email)}";
                Assert.IsNotNull(actual);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
            }
        }
    }
}