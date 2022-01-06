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
             users.Select(user => new UserResponseModel()
             {
                 Id = user.Id,
                 Username = user.Username,
                 Email = user.Email,
                 Roles = user.Roles.Select(role => role.Role.Type),
                 Avatar = user.PicturePath,
                 IssuesCount = user.Issues.Count,
                 VehiclesCount = user.Vehicles.Count
             });

        public static async Task<UserResponseModel> UserByIdAsync(long userId, CarShopDbContext db)
        => await db.Users
            .Where(user => user.Id == userId)
            .Select(user => new UserResponseModel()
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Roles = user.Roles.Select(role => role.Role.Type),
                Avatar = user.PicturePath,
                IssuesCount = user.Issues.Count,
                VehiclesCount = user.Vehicles.Count
            })
            .FirstOrDefaultAsync();

        public static IQueryable<User> Filter(UserSearchAndSortRequestModel model, IQueryable<User> query)
        {
            if (EntityValidator.IsStringPropertyValid(model.Username))
            {
                query = query.Where(user => user.Username.Contains(model.Username));
            }

            if (EntityValidator.IsStringPropertyValid(model.Email))
            {
                query = query.Where(user => user.Email.Contains(model.Email));
            }

            if (EntityValidator.IsStringPropertyValid(model.Role))
            {
                query = query.Where(user => user.Roles.Any(r => r.Role.Type.ToLower().Contains(model.Role.ToLower())));
            }

            return query;
        }

        public static IOrderedQueryable<User> Sort(UserSortRequestModel model, IQueryable<User> query)
        {
            var sortedQuery = query.OrderBy(u => 1);

            if (model.MostVehicles)
            {
                sortedQuery = sortedQuery.ThenByDescending(user => user.Vehicles.Count);
            }

            if (model.MostActive)
            {
                sortedQuery = sortedQuery.ThenByDescending(user => user.Issues.Count);
            }

            if (model.Recently)
            {
                sortedQuery = sortedQuery.ThenByDescending(u => u.CreatedOn);
            }

            if (model.Oldest)
            {
                sortedQuery = sortedQuery.ThenBy(user => user.CreatedOn);
            }

            return sortedQuery;
        }
    }
}