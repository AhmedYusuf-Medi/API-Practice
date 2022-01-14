namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.Issue;
    using CarShop.Models.Response.Issue;
    using CarShop.Service.Common.Extensions.Validator;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class IssueQueries
    {
        public static Func<IQueryable<Issue>, IQueryable<IssueResponseModel>> GetAllIssueResponse
         => (IQueryable<Issue> issues) =>
         issues.Select(issue => new IssueResponseModel()
         {
             Id = issue.Id,
             VehicleOwner = issue.Vehicle.Owner.Username,
             VehicleOwnerId = issue.Vehicle.Owner.Id,
             VehicleId = issue.Vehicle.Id,
             VehiclePlateNumber = issue.Vehicle.PlateNumber,
             Description = issue.Description,
             Status = issue.Status.Status,
             Priority = issue.Priority.Priority,
             Severity = issue.Priority.Severity,
         });

        public static async Task<IssueResponseModel> IssueByIdAsync(long issueId, CarShopDbContext db)
         => await db.Issues
            .Where(issue => issue.Id == issueId)
            .Select(issue => new IssueResponseModel()
            {
                Id = issue.Id,
                VehicleOwner = issue.Vehicle.Owner.Username,
                VehicleOwnerId = issue.Vehicle.Owner.Id,
                VehicleId = issue.Vehicle.Id,
                VehiclePlateNumber = issue.Vehicle.PlateNumber,
                Description = issue.Description,
                Status = issue.Status.Status,
                Priority = issue.Priority.Priority,
                Severity = issue.Priority.Severity,
            })
            .FirstOrDefaultAsync();

        public static IQueryable<Issue> Filter(IssueFilterAndSortRequestModel requestModel, IQueryable<Issue> query)
        {
            if (EntityValidator.IsStringPropertyValid(requestModel.OwnerName))
            {
                query = query.Where(issue => issue.Vehicle.Owner.Username.ToLower().Contains(requestModel.OwnerName.ToLower()));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Priority))
            {
                query = query.Where(issue => issue.Priority.Priority.ToLower().Contains(requestModel.Priority.ToLower()));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.Status))
            {
                query = query.Where(issue => issue.Status.Status.ToLower().Contains(requestModel.Status.ToLower()));
            }

            if (requestModel.VehicleId.HasValue)
            {
                query = query.Where(issue => issue.VehicleId == requestModel.VehicleId);
            }

            if (requestModel.OwnerId.HasValue)
            {
                query = query.Where(issue => issue.Vehicle.OwnerId == requestModel.OwnerId);
            }

            return query;
        }

        public static IQueryable<Issue> SortBy(IssueSortRequestModel requestModel, IQueryable<Issue> query)
        {
            var dummyQuery = query.OrderByDescending(x => 1);

            if (requestModel.BySeverity)
            {
                dummyQuery = dummyQuery.ThenByDescending(issue => issue.Priority.Severity);
            }

            if (requestModel.Recently)
            {
                dummyQuery = dummyQuery.ThenByDescending(issue => issue.CreatedOn);
            }
            else if (requestModel.Oldest)
            {
                dummyQuery = dummyQuery.ThenBy(issue => issue.CreatedOn);
            }

            return dummyQuery;
        }
    }
}