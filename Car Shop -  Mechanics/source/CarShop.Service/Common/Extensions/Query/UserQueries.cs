namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Request.User;
    using CarShop.Models.Response.User;
    using CarShop.Service.Common.Extensions.Validator;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class UserQueries
    {
        public static Func<IQueryable<User>, IQueryable<UserResponseModel>> GetAllUserResponse
         => (IQueryable<User> users) =>
             users.Select(u => new UserResponseModel()
             {
                 Id = u.Id,
                 Username = u.Username,
                 Email = u.Email,
                 Roles = u.Roles.Select(r => r.Role.Type),
                 Avatar = u.PicturePath,
                 IssuesCount = u.Issues.Count,
                 VehiclesCount = u.Vehicles.Count
             });

        public static async Task<UserResponseModel> UserByIdAsync(long userId, CarShopDbContext db)
        {
            return await db.Users
                 .Where(u => u.Id == userId)
                 .Select(u => new UserResponseModel()
                 {
                     Id = u.Id,
                     Username = u.Username,
                     Email = u.Email,
                     Roles = u.Roles.Select(r => r.Role.Type),
                     Avatar = u.PicturePath,
                     IssuesCount = u.Issues.Count,
                     VehiclesCount = u.Vehicles.Count
                 })
                 .FirstOrDefaultAsync();
        }

        public static IQueryable<User> Filter(UserSearchAndSortRequestModel model, IQueryable<User> query)
        {
            if (EntityValidator.IsStringPropertyValid(model.Username))
            {
                query = query.Where(u => u.Username.Contains(model.Username));
            }

            if (EntityValidator.IsStringPropertyValid(model.Email))
            {
                query = query.Where(u => u.Email.Contains(model.Email));
            }

            if (EntityValidator.IsStringPropertyValid(model.Role))
            {
                query = query.Where(u => u.Roles.Any(r => r.Role.Type.ToLower().Contains(model.Role.ToLower())));
            }

            return query;
        }

        public static IOrderedQueryable<User> Sort(UserSortRequestModel model, IQueryable<User> query)
        {
            var sortedQuery = query.OrderBy(u => 1);

            if (model.MostVehicles)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.Vehicles.Count);
            }

            if (model.MostActive)
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