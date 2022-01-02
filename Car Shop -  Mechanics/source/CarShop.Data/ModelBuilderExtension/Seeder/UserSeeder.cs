namespace CarShop.Data.ModelBuilderExtension.Seeder
{
    using CarShop.Models.Base;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Identity;

    public class UserSeeder : ISeeder
    {
        public async Task SeedAsync(CarShopDbContext dbContext)
        {
            if (dbContext == null)
            {
                throw new ArgumentNullException(string.Format(ModelBuilderExtension.Invalid_Seeder_Injection, nameof(UserSeeder)));
            }

            if (await dbContext.Users.IgnoreQueryFilters().AnyAsync())
            {
                return;
            }

            var demoUser = new User();
            var code = new Guid();
            var defaultAvatar = "https://res.cloudinary.com/diihcd5cx/image/upload/v1640880258/Default_Avatar_e2kmn5.png";
            var hasher = new PasswordHasher<User>();

            var users = new List<(string email, string password,
                string username, string picturePath)>
            {
                ("ahhasmed.usuf@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "amedy", defaultAvatar),
                ("muthasdkabarona@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "medysun", defaultAvatar)
            };

            foreach (var user in users)
            {
               await dbContext.Users.AddAsync(new User
               {
                    Email = user.email,
                    Password = user.password,
                    Username = user.username,
                    Code = code = Guid.NewGuid(),
                    PicturePath = user.picturePath
                });

                await dbContext.SaveChangesAsync();
            }
        }
    }
}