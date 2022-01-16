namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.ReportType;
    using CarShop.Models.Response.ReportType;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ReportTypeQueries
    {
        public static Func<IQueryable<ReportType>, IQueryable<ReportTypeResponseModel>> GetAllReportTypeResponse
        => (IQueryable<ReportType> reportTypes) =>
        reportTypes.Select(reportType => new ReportTypeResponseModel()
        {
            Id = reportType.Id,
            Type = reportType.Type,
            UsedSince = reportType.CreatedOn.Date,
            ReportsCount = reportType.Reports.Count()
        });

        public static async Task<ReportTypeResponseModel> ReportTypeByIdAsync(long reporTypeId, CarShopDbContext db)
        => await db.ReportTypes
            .Where(reportType => reportType.Id == reporTypeId)
            .Select(reportType => new ReportTypeResponseModel()
            {
                Id = reportType.Id,
                Type = reportType.Type,
                UsedSince = reportType.CreatedOn.Date,
                ReportsCount = reportType.Reports.Count()
            })
            .FirstOrDefaultAsync();


        public static IOrderedQueryable<ReportType> Sort(ReportTypeSortRequestModel model, IQueryable<ReportType> query)
        {
            var sortedQuery = query.OrderBy(reportType => 1);

            if (model.MostUsed)
            {
                sortedQuery = sortedQuery.ThenByDescending(reportType => reportType.Reports.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(reportType => reportType.CreatedOn);
            }
            else if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(reportType => reportType.CreatedOn);
            }

            return sortedQuery;
        }
    }
}