namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.Report;
    using CarShop.Models.Response.Report;
    using CarShop.Service.Common.Extensions.Validator;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class ReportQueries
    {
        public static Func<IQueryable<Report>, IQueryable<ReportResponseModel>> GetAllReportResponse
          => (IQueryable<Report> reports) =>
           reports.Select(report => new ReportResponseModel()
           {
               Id = report.Id,
               Description = report.Description,
               SenderId = report.SenderId,
               SenderName = report.Sender.Username,
               ReceiverId = report.ReceiverId,
               ReceiverName = report.Receiver.Username,
               ReportType = report.ReportType.Type
           });

        public static async Task<ReportResponseModel> IssueByIdAsync(long reportId, CarShopDbContext db)
         => await db.Reports
            .Where(report => report.Id == reportId)
            .Select(report => new ReportResponseModel()
            {
                Id = report.Id,
                Description = report.Description,
                SenderId = report.SenderId,
                SenderName = report.Sender.Username,
                ReceiverId = report.ReceiverId,
                ReceiverName = report.Receiver.Username,
                ReportType = report.ReportType.Type
            })
            .FirstOrDefaultAsync();

        public static IQueryable<Report> Filter(ReportFilterAndSortRequestModel requestModel, IQueryable<Report> query)
        {
            if (EntityValidator.IsStringPropertyValid(requestModel.SenderName))
            {
                query = query.Where(report => report.Sender.Username.ToLower().Contains(requestModel.SenderName.ToLower()));
            }

            if (EntityValidator.IsStringPropertyValid(requestModel.ReceiverName))
            {
                query = query.Where(report => report.Receiver.Username.ToLower().Contains(requestModel.ReceiverName.ToLower()));
            }

            if (requestModel.SenderId.HasValue)
            {
                query = query.Where(report => report.SenderId == requestModel.SenderId);
            }

            if (requestModel.ReceiverId.HasValue)
            {
                query = query.Where(report => report.ReceiverId == requestModel.ReceiverId);
            }

            if (requestModel.ReportTypeId.HasValue)
            {
                query = query.Where(report => report.ReportTypeId == requestModel.ReportTypeId);
            }

            return query;
        }

        public static IQueryable<Report> SortBy(ReportSortRequestModel requestModel, IQueryable<Report> query)
        {
            var dummyQuery = query.OrderByDescending(x => 1);

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