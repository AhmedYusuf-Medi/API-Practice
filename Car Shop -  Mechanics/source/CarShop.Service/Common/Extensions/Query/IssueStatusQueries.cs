namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.IssueStatus;
    using CarShop.Models.Response.IssueStatus;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class IssueStatusQueries
    {
        public static Func<IQueryable<IssueStatus>, IQueryable<IssueStatusResponseModel>> GetAllIssueStatusResponse
        => (IQueryable<IssueStatus> issueStatuses) =>
            issueStatuses.Select(vb => new IssueStatusResponseModel()
            {
                Id = vb.Id,
                StatusName = vb.Status,
                UsedSince = vb.CreatedOn.Date,
                IssuesCount = vb.Issues.Count()
            });

        public static async Task<IssueStatusResponseModel> IssueStatusByIdAsync(long issueStatusId, CarShopDbContext db)
        {
            return await db.IssueStatuses
                 .Where(vb => vb.Id == issueStatusId)
                 .Select(vb => new IssueStatusResponseModel()
                 {
                     Id = vb.Id,
                     StatusName = vb.Status,
                     UsedSince = vb.CreatedOn.Date,
                     IssuesCount = vb.Issues.Count()
                 })
                 .FirstOrDefaultAsync();
        }

        public static IOrderedQueryable<IssueStatus> Sort(IssueStatusSortRequestModel model, IQueryable<IssueStatus> query)
        {
            var sortedQuery = query.OrderBy(u => 1);

            if (model.MostUsed)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.Issues.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(u => u.CreatedOn);
            }

            return sortedQuery;
        }
    }
}