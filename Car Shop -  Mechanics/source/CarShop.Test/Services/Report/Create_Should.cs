namespace CarShop.Test.Services.Report
{
    using CarShop.Data;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Report;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Threading.Tasks;

    [TestClass]
    public class Create_Should : BaseTest
    {
        [TestMethod]
        [DataRow("Description asddsa!", 1, 3, 1)]
        [DataRow("Description asddsa!", 3, 1, 3)]
        public async Task Create_Should_ReturnSucceedResponse(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
               Description = description,
               SenderId =senderId,
               ReceiverId = receiverId,
               ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Create_Succeed, Constants.Report));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 1, 2, 1)]
        [DataRow("Description asddsa!", 2, 1, 3)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenReportAlreadyExist(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.Cannot_Report_Twice);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 1, 1, 1)]
        [DataRow("Description asddsa!", 2, 2, 3)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenTryingToReportYourself(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, ExceptionMessages.Cannot_Report_Yourself);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 0, 1, 1)]
        [DataRow("Description asddsa!", long.MaxValue, 2, 3)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenSenderDoesntExist(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 1, 0, 1)]
        [DataRow("Description asddsa!", 1, long.MaxValue, 3)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenReceiverDoesntExist(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 1, 3, 0)]
        [DataRow("Description asddsa!", 1, 3, int.MaxValue)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenReportTypeDoesntExist(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Report_Type));
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }

        [TestMethod]
        [DataRow("Description asddsa!", 0, long.MaxValue, 0)]
        [DataRow("Description asddsa!", long.MaxValue, 0, int.MaxValue)]
        public async Task Create_Should_ReturnNotSucceedResponse_WhenForeignKeysDoesntExist(string description, long senderId, long receiverId, int reportTypeId)
        {
            var requestModel = new ReportCreateRequestModel
            {
                Description = description,
                SenderId = senderId,
                ReceiverId = receiverId,
                ReportTypeId = reportTypeId
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.CreateAsync(requestModel);

                var expectedMessage = $"{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.User)}\n{string.Format(ExceptionMessages.DOESNT_EXIST, Constants.Report_Type)}";
                Assert.IsNotNull(actual);
                Assert.IsFalse(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(InfoResponse));
            }
        }
    }
}