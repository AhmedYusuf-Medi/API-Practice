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
                result = result.OrderByDescending(exception => exception.CreatedOn);
            }
            else if (requestModel.Oldest)
            {
                result = result.OrderBy(exception => exception.CreatedOn);
            }

            return result;
        }

        public static IQueryable<ExceptionLog> Filter(ExceptionSortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> result)
        {
            if (requestModel.Checked != null)
            {
                result = result.Where(exception => exception.IsChecked == requestModel.Checked);
            }

            if (requestModel.Date != null)
            {
                result = result.Where(exception => exception.CreatedOn.Date == requestModel.Date);
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Month))
            {
                int month = DateTime.ParseExact(requestModel.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                result = result.Where(exception => exception.CreatedOn.Date.Month == month);
            }

            if (requestModel.Year != null)
            {
                int year = (int)requestModel.Year;
                result = result.Where(exception => exception.CreatedOn.Date.Year == year);
            }

            return result;
        }
    }
}