namespace CarShop.Test.Services.ExceptionLogs
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
    using CarShop.Models.Response;
    using CarShop.Service.Common.Extensions.Pager;
    using CarShop.Service.Common.Messages;
    using CarShop.Service.Data.Exception;
    using CarShop.Test.Services.Base;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading.Tasks;

    [TestClass]
    public class FilterBy_Should : BaseTest
    {
        [TestMethod]
        [DataRow(3, 1, "January", null, false, true, false)]
        [DataRow(3, 1, "", 2022, false, false, true)]
        [DataRow(1, 1, "", null, true, false, true)]
        public async Task FilterAndSortByShould_ReturnExceptionLogModels(int perPage, int page, string month, int? year, bool IsChecked, bool recently, bool oldest)
        {
            var requestModel = new ExceptionSortAndFilterRequestModel
            {
                Page = page,
                PerPage = perPage,
                Month = month,
                Year = year,
                Checked = IsChecked,
                Recently = recently,
                Oldest = oldest
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(month))
                {
                    int monthNum = DateTime.ParseExact(requestModel.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.CreatedOn.Date.Month == monthNum);
                    }
                }

                if (year.HasValue)
                {
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.CreatedOn.Date.Year == year);
                    }
                }

                if (IsChecked)
                {
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.IsChecked);
                    }
                }

                var expectedMessage = $"{string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Exceptions)}\n{string.Format(ResponseMessages.Entity_Sort_Succeed, Constants.Exceptions)}";
                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, expectedMessage);
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ExceptionLog>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }

        [TestMethod]
        [DataRow(3, 1, "January", null, false)]
        [DataRow(3, 1, "", 2022, false)]
        public async Task FilterByShould_ReturnExceptionLogModels(int perPage, int page, string month, int? year, bool IsChecked)
        {
            var requestModel = new ExceptionSortAndFilterRequestModel
            {
                Page = page,
                PerPage = perPage,
                Month = month,
                Year = year,
                Checked = IsChecked
            };

            using (var assertContext = new CarShopDbContext(this.Options))
            {
                var sut = new ExceptionLogService(assertContext);
                var actual = await sut.FilterByAsync(requestModel);

                if (!string.IsNullOrEmpty(month))
                {
                    int monthNum = DateTime.ParseExact(requestModel.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.CreatedOn.Date.Month == monthNum);
                    }
                }

                if (year.HasValue)
                {
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.CreatedOn.Date.Year == year);
                    }
                }

                if (IsChecked)
                {
                    foreach (var exception in actual.Payload.Entities)
                    {
                        Assert.IsTrue(exception.IsChecked);
                    }
                }

                Assert.IsNotNull(actual);
                Assert.IsTrue(actual.IsSuccess);
                Assert.AreEqual(actual.Message, string.Format(ResponseMessages.Entity_Filter_Succeed, Constants.Exceptions));
                Assert.IsInstanceOfType(actual, typeof(Response<Paginate<ExceptionLog>>));
                Assert.AreEqual(perPage, actual.Payload.Entities.Count());
            }
        }
    }
}