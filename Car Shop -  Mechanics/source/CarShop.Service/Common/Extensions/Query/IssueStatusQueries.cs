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
            issueStatuses.Select(issueStatus => new IssueStatusResponseModel()
            {
                Id = issueStatus.Id,
                StatusName = issueStatus.Status,
                UsedSince = issueStatus.CreatedOn.Date,
                IssuesCount = issueStatus.Issues.Count()
            });

        public static async Task<IssueStatusResponseModel> IssueStatusByIdAsync(long issueStatusId, CarShopDbContext db)
        => await db.IssueStatuses
            .Where(issueStatus => issueStatus.Id == issueStatusId)
            .Select(issueStatus => new IssueStatusResponseModel()
            {
                Id = issueStatus.Id,
                StatusName = issueStatus.Status,
                UsedSince = issueStatus.CreatedOn.Date,
                IssuesCount = issueStatus.Issues.Count()
            })
            .FirstOrDefaultAsync();
        

        public static IOrderedQueryable<IssueStatus> Sort(IssueStatusSortRequestModel model, IQueryable<IssueStatus> query)
        {
            var sortedQuery = query.OrderBy(issueStatus => 1);

            if (model.MostUsed)
            {
                sortedQuery = sortedQuery.ThenByDescending(issueStatus => issueStatus.Issues.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(issueStatus => issueStatus.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(issueStatus => issueStatus.CreatedOn);
            }

            return sortedQuery;
        }
    }
}