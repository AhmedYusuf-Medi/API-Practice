namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.IssuePriority;
    using CarShop.Models.Response.IssuePriority;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class IssuePriorityQueries
    {
        public static Func<IQueryable<IssuePriority>, IQueryable<IssuePriorityResponseModel>> GetAllIssuePriorityResponse
         => (IQueryable<IssuePriority> issuePriorities) =>
          issuePriorities.Select(issuePriority => new IssuePriorityResponseModel()
          {
              Id = issuePriority.Id,
              PriorityName = issuePriority.Priority,
              Severity = issuePriority.Severity,
              UsedSince = issuePriority.CreatedOn.Date,
              IssuesCount = issuePriority.Issues.Count()
          });

        public static async Task<IssuePriorityResponseModel> IssuePriorityByIdAsync(long issuePriorityId, CarShopDbContext db)
           => await db.IssuePriorities
            .Where(issuePriority => issuePriority.Id == issuePriorityId)
            .Select(issuePriority => new IssuePriorityResponseModel()
            {
                Id = issuePriority.Id,
                PriorityName = issuePriority.Priority,
                Severity = issuePriority.Severity,
                UsedSince = issuePriority.CreatedOn.Date,
                IssuesCount = issuePriority.Issues.Count()
            })
            .FirstOrDefaultAsync();

        public static IOrderedQueryable<IssuePriority> Sort(IssuePrioritySortRequestModel model, IQueryable<IssuePriority> query)
        {
            var sortedQuery = query.OrderBy(issuePriority => 1);

            if (model.MostUsed)
            {
                sortedQuery = sortedQuery.ThenByDescending(issuePriority => issuePriority.Issues.Count);
            }

            if (model.BySeverity)
            {
                sortedQuery = sortedQuery.ThenByDescending(issuePriority => issuePriority.Severity);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(issuePriority => issuePriority.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(issuePriority => issuePriority.CreatedOn);
            }

            return sortedQuery;
        }
    }
}