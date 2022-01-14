namespace CarShop.Test.Services.Issues
{
    using CarShop.Data;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Issue;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class FilterBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 2, "repairing", "", null, "", null, true, false, false)]
        [DataRow(1, 2, "", "medi", null, "", null, false, true, false)]
        [DataRow(1, 2, "", "", (long)1, "", null, false, false, true)]
        [DataRow(1, 2, "", "", null, "amedy", null, false, false, true)]
        [DataRow(1, 1, "", "", null, "", (long)1, false, false, true)]
        public async Task FilterBy_Should_ReturnCorrectFilteredAndSortedResponseModels(int page, int perPage,
            string status, string priority, long? vehicleId, string ownerName, long? ownerId,
            bool recently, bool oldest, bool bySeveriy)
        {
            var requestModel = new IssueFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Status = status,
                Priority = priority,
                VehicleId = vehicleId,
                OwnerId = ownerId,
                OwnerName = ownerName,
                Recently = recently,
                Oldest = oldest,
                BySeverity = bySeveriy
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(status))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.Status.ToLower().Contains(requestModel.Status.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(priority))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.Priority.ToLower().Contains(requestModel.Priority.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(ownerName))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleOwner.Contains(ownerName));
                    }
                }

                if (vehicleId.HasValue)
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleId == vehicleId);
                    }
                }

                if (ownerId.HasValue)
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleOwnerId == ownerId);
                    }
                }

                var expectedMessage = $"{string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Issues)}\n{string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Issues)}";

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<IssueResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());

            }
        }

        [TestMethod]
        [DataRow(1, 2, "repairing", "", null, "", null)]
        [DataRow(1, 2, "", "medi", null, "", null)]
        [DataRow(1, 2, "", "", (long)1, "", null)]
        [DataRow(1, 2, "", "", null, "amedy", null)]
        [DataRow(1, 1, "", "", null, "", (long)1)]
        public async Task FilterBy_Should_ReturnCorrectFilteredResponseModels(int page, int perPage,
          string status, string priority, long? vehicleId, string ownerName, long? ownerId)
        {
            var requestModel = new IssueFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                Status = status,
                Priority = priority,
                VehicleId = vehicleId,
                OwnerId = ownerId,
                OwnerName = ownerName
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new IssueService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(status))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.Status.ToLower().Contains(requestModel.Status.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(priority))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.Priority.ToLower().Contains(requestModel.Priority.ToLower()));
                    }
                }

                if (!string.IsNullOrEmpty(ownerName))
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleOwner.Contains(ownerName));
                    }
                }

                if (vehicleId.HasValue)
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleId == vehicleId);
                    }
                }

                if (ownerId.HasValue)
                {
                    foreach (var issue in actual.Payload.Entities)
                    {
                        Assert.IsTrue(issue.VehicleOwnerId == ownerId);
                    }
                }

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Issues));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<IssueResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());

            }
        }
    }
}