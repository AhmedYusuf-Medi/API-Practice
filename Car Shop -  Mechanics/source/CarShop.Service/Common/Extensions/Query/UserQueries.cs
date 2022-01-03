namespace CarShop.Service.Common.Extensions.Query
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using CarShop.Models.Response.User;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public static class UserQueries
    {
        public static Func<IQueryable<User>, IQueryable<UserResponseModel>> GetAllUserResponse
         => (IQueryable<User> posts) =>
             posts.Select(u => new UserResponseModel()
             {
                 Id = u.Id,
                 Username = u.Username,
                 Roles = u.Roles.Select(r => r.Role.Type),
                 Avatar = u.PicturePath,
                 IssueCount = u.Issues.Count
             });

        public static async Task<UserResponseModel> UserByIdAsync(long userId, CarShopDbContext db)
        {
            return await db.Users
                 .Where(u => u.Id == userId)
                 .Select(u => new UserResponseModel()
                 {
                     Id = u.Id,
                     Username = u.Username,
                     Roles = u.Roles.Select(r => r.Role.Type),
                     Avatar = u.PicturePath,
                     IssueCount = u.Issues.Count
                 })
                 .FirstOrDefaultAsync();
        }
    }
}