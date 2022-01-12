namespace CarShop.Test.Services.Vehicles
{
    using CarShop.Data;
    using CarShop.Models.Request.Vehicle;
    using CarShop.Models.Response;
    using CarShop.Models.Response.Vehicle;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class FilterBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(1, 3, "", "gmail", "", "", null, null, null, null, null,  true, false, false, false, true, false)]
        [DataRow(1, 3, "d", "", "", "", null, null, null, null, null,  false, true, false, false, false, true)]
        [DataRow(1, 2, "", "", "A4", "", null, null, null, null, null,  false, true, false, false, false, true)]
        [DataRow(1, 1, "", "", "", "AB1234AB", null, null, null, null, null,  false, true, false, true, false, true)]
        [DataRow(1, 2, "", "", "", "", null, null, null, (long)1, null,  false, true, false, true, false, true)]
        [DataRow(1, 2, "", "", "", "", null, null, null, null, 2005,  false, true, false, true, false, true)]
        public async Task FilterBy_ShouldReturn_CorrectReponseModels(int page, int perPage, string username, string email,
            string model, string plateNumber, long? brandId, long? vehicleTypeId, long? ownerId, long? issueCount, int? year,
            bool recently, bool oldest, bool yearAsc, bool yearDes, bool mostIssues, bool lessIssues)
        {
            var requestModel = new VehicleFilterAndSortRequestModel
            {
                Page = page,
                PerPage = perPage,
                OwnerEmail = email,
                OwnerName = username,
                Model = model,
                PlateNumber = plateNumber,
                BrandId = brandId,
                VehicleTypeId = vehicleTypeId,
                OwnerId = ownerId,
                IssueCount = issueCount,
                Year = year,
                RecentlyRegistered = recently,
                OldestRegistered = oldest,
                ByYearAsc = yearAsc,
                ByYearDesc = yearDes,
                MostIssues = mostIssues,
                LessIssues = lessIssues
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new VehicleService(assertContext, this.CloudinaryService);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(email))
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.OwnerMail.Contains(email));
                    }
                }

                if (!string.IsNullOrEmpty(username))
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.Owner.Contains(username));
                    }
                }

                if (!string.IsNullOrEmpty(plateNumber))
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.PlateNumber.Contains(plateNumber));
                    }
                }

                if (!string.IsNullOrEmpty(model))
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.Model.Contains(model));
                    }
                }

                if (year.HasValue)
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.Year == year);
                    }
                }

                if (issueCount.HasValue)
                {
                    foreach (var vehicle in actual.Payload.Entities)
                    {
                        Assert.IsTrue(vehicle.IssueCount == issueCount);
                    }
                }

                var expectedMessage = $"{string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Vehicles)}\n{string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Vehicles)}";

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<VehicleResponseModel>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}