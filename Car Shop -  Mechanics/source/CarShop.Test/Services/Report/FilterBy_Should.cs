namespace CarShop.Test.Services.Report
{
    using CarShop.Data;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Report;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class FilterBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 1, (long)1, null, null, null, null, true, false)]
        [DataRow(1, 1, null, (long)1, null, null, null, false, true)]
        [DataRow(1, 1, null, null, "amedy", null, null, true, false)]
        [DataRow(1, 1, null, null, null, "amedy", null, false, true)]
        [DataRow(1, 1, null, null, null, "amedy", (long)2, false, true)]
        public async Task FilterBy_Should_ReturnCorrectFilteredAndSortedResponseModels(int page, int perPage,
          long? senderId, long? receiverId, string senderName, string receiverName, long? reportTypeId,
          bool recently, bool oldest)
        {
            var requestModel = new ReportFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                SenderId = senderId,
                SenderName = senderName,
                ReceiverId = receiverId,
                ReceiverName = receiverName,
                ReportTypeId = reportTypeId,
                Recently = recently,
                Oldest = oldest,
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ReportService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (senderId.HasValue)
                {
                    foreach (var report in actual.Payload.Entities)
                    {
                        Assert.IsTrue(report.SenderId == senderId);
                    }
                }

                if (receiverId.HasValue)
                {
                    foreach (var report in actual.Payload.Entities)
                    {
                        Assert.IsTrue(report.ReceiverId == receiverId);
                    }
                }

                if (!string.IsNullOrEmpty(senderName))
                {
                    foreach (var report in actual.Payload.Entities)
                    {
                        Assert.IsTrue(report.SenderName.ToLower().Contains(senderName.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(receiverName))
                {
                    foreach (var report in actual.Payload.Entities)
                    {
                        Assert.IsTrue(report.ReceiverName.ToLower().Contains(receiverName.ToLower()));
                    }
                }

                var expectedMessage = $"{string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Reports)}\n{string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Reports)}";

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ReportResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}