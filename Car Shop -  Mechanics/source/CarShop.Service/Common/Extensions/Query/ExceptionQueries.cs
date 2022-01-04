namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
    using CarShop.Service.Common.Extensions.Validator;
    using System;
    using System.Globalization;
    using System.Linq;

    public static class ExceptionQueries
    {
        public static IQueryable<ExceptionLog> SortBy(ExceptionSortRequestModel requestModel, IQueryable<ExceptionLog> result)
        {
            if (requestModel.MostRecently)
            {
                result = result.OrderByDescending(e => e.CreatedOn);
            }
            else if (requestModel.Oldest)
            {
                result = result.OrderBy(e => e.CreatedOn);
            }

            return result;
        }

        public static IQueryable<ExceptionLog> Filter(ExceptionSortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> result)
        {
            if (requestModel.Checked != null)
            {
                result = result.Where(e => e.IsChecked == requestModel.Checked);
            }

            if (requestModel.Date != null)
            {
                result = result.Where(e => e.CreatedOn.Date == requestModel.Date);
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Month))
            {
                int month = DateTime.ParseExact(requestModel.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                result = result.Where(e => e.CreatedOn.Date.Month == month);
            }

            if (requestModel.Year != null)
            {
                int year = (int)requestModel.Year;
                result = result.Where(e => e.CreatedOn.Date.Year == year);
            }

            return result;
        }
    }
}