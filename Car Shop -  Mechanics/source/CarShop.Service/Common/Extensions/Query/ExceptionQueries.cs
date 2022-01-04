namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Models.Base;
    using CarShop.Models.Request.Exception;
    using CarShop.Service.Common.Extensions.Validator;
    using System;
    using System.Linq;

    public static class ExceptionQueries
    {
        public static IQueryable<ExceptionLog> SortBy(SortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> result)
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

        public static IQueryable<ExceptionLog> Filter(SortAndFilterRequestModel requestModel, IQueryable<ExceptionLog> result)
        {
            if (requestModel.Checked != null)
            {
                result = result.Where(e => e.IsChecked == requestModel.Checked);
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Date))
            {
                var date = Convert.ToDateTime(requestModel.Date).Date;

                result = result.Where(e => e.CreatedOn.Date == date);
            }

            return result;
        }
    }
}