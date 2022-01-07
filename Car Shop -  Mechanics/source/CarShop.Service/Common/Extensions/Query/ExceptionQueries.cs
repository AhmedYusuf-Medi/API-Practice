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
        public static IQueryable<ExceptionLog> SortBy(ExceptionSortRequestModel requestModel, IQueryable<ExceptionLog> query)
        {
            if (requestModel.MostRecently)
            {
                query = query.OrderByDescending(exception => exception.CreatedOn);
            }
            else if (requestModel.Oldest)
            {
                query = query.OrderBy(exception => exception.CreatedOn);
            }

            return query;
        }

        public static IQueryable<ExceptionLog> Filter(ExceptionSortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> query)
        {
            if (requestModel.Checked)
            {
                query = query.Where(exception => exception.IsChecked == requestModel.Checked);
            }

            if (requestModel.Date != null)
            {
                query = query.Where(exception => exception.CreatedOn.Date == requestModel.Date);
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Month))
            {
                int month = DateTime.ParseExact(requestModel.Month, "MMMM", CultureInfo.InvariantCulture).Month;
                query = query.Where(exception => exception.CreatedOn.Date.Month == month);
            }

            if (requestModel.Year != null)
            {
                int year = (int)requestModel.Year;
                query = query.Where(exception => exception.CreatedOn.Date.Year == year);
            }

            return query;
        }
    }
}