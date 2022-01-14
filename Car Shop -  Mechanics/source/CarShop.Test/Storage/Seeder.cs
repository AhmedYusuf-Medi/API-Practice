namespace CarShop.Test.Storage
{
    using CarShop.Data;
    using CarShop.Models.Base;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public static class Seeder
    {
        private static long id = 0;

        public static async Task SeedAsync(CarShopDbContext dbContext)
        {
            await dbContext.Roles.AddRangeAsync(SeedRoles());
            await dbContext.Users.AddRangeAsync(SeedUsers());
            await dbContext.UserRoles.AddRangeAsync(SeedUserRoles());
            await dbContext.VehicleBrands.AddRangeAsync(SeedVehicleBrands());
            await dbContext.VehicleTypes.AddRangeAsync(SeedVehicleTypes());
            await dbContext.Vehicles.AddRangeAsync(SeedVehicles());
            await dbContext.IssueStatuses.AddRangeAsync(SeedIssueStatuses());
            await dbContext.IssuePriorities.AddRangeAsync(SeedIssuePriorties());
            await dbContext.Issues.AddRangeAsync(SeedIssues());
            await dbContext.ExceptionLogs.AddRangeAsync(SeedExceptionLogs());
        }

        private static IEnumerable<Role> SeedRoles()
        {
            var roleParameters = new List<string>() { "Admin", "User", "Mechanic", "Blocked", "Pending" };

            var roles = new HashSet<Role>();

            id = 0;

            foreach (var role in roleParameters)
            {
                roles.Add(new Role() { Id = ++id, Type = role });
            }

            return roles;
        }

        private static IEnumerable<User> SeedUsers()
        {
            var demoUser = new User();
            var code = new Guid();
            var defaultAvatar = "https://res.cloudinary.com/diihcd5cx/image/upload/v1640880258/Default_Avatar_e2kmn5.png";
            var hasher = new PasswordHasher<User>();

            var usersParameters = new List<(string email, string password,
                string username, string picturePath)>
            {
                ("ahhasmed.usuf@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "amedy", defaultAvatar),
                ("muthasdkabarona@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "medysun", defaultAvatar),
                ("norolesuser@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "norolesname", defaultAvatar),
                ("blockedUser@gmail.com", hasher.HashPassword(demoUser, "passwordQ1!"), "blockedUser", defaultAvatar)
           };

            var users = new HashSet<User>();

            id = 0;

            foreach (var user in usersParameters)
            {
                users.Add(new User
                {
                    Id = ++id,
                    Email = user.email,
                    Password = user.password,
                    Username = user.username,
                    Code = code = Guid.NewGuid(),
                    PicturePath = user.picturePath
                });
            }

            //register unverified user
            users.Add(new User
            {
                Id = ++id,
                Email = "verificationTest@gmail.com",
                Password = hasher.HashPassword(demoUser, "passwordQ1!"),
                Username = "steven_verification",
                Code = new Guid("5C60F693-BEF5-E011-A485-80EE7300C695"),
                PicturePath = defaultAvatar
            });

            return users;
        }

        private static IEnumerable<UserRole> SeedUserRoles()
        {
            var userRolesParameters = new List<(long userId, long roleId)>
            {
                (1, 2),
                (2, 1),
                (2, 3),
                (4, 4),
                (5, 5)
            };

            var roles = new HashSet<UserRole>();

            foreach (var role in userRolesParameters)
            {
                roles.Add(new UserRole()
                {
                    RoleId = role.roleId,
                    UserId = role.userId,
                });
            }

            return roles;
        }

        private static IEnumerable<VehicleBrand> SeedVehicleBrands()
        {
            var brandParameters = new List<string>
            {
               "BMW",
               "Mercedes",
               "Audi",
               "Opel",
               "Nisan"
            };

            id = 0;

            var brands = new HashSet<VehicleBrand>();

            foreach (var brand in brandParameters)
            {
                brands.Add(new VehicleBrand
                {
                    Id = ++id,
                    Brand = brand
                });
            }

            return brands;
        }

        private static IEnumerable<VehicleType> SeedVehicleTypes()
        {
            var vehicleTypesParamters = new List<string>
            {
               "Car",
               "Truck",
               "Motorcycle",
            };

            id = 0;

            var types = new HashSet<VehicleType>();

            foreach (var type in vehicleTypesParamters)
            {
                types.Add(new VehicleType
                {
                    Id = ++id,
                    Type = type
                });
            }

            return types;
        }

        private static IEnumerable<Vehicle> SeedVehicles()
        {
            id = 0;

            var vehicleParamters = new List<Vehicle>
            {
               new Vehicle{Id = ++id, Year = 2005, Model = "A4", PlateNumber = "AB1234AB", BrandId = 3, VehicleTypeId = 1, OwnerId = 1, PicturePath = "" },
               new Vehicle{Id = ++id, Year = 2005, Model = "Astra", PlateNumber = "AC1234AC", BrandId = 4, VehicleTypeId = 1, OwnerId = 1, PicturePath = "" },
               new Vehicle{Id = ++id, Year = 2002, Model = "Astra", PlateNumber = "AD1234AC", BrandId = 4, VehicleTypeId = 1, OwnerId = 2, PicturePath = "" },
               new Vehicle{Id = ++id, Year = 2001, Model = "A4", PlateNumber = "AD1234AB", BrandId = 3, VehicleTypeId = 1, OwnerId = 2, PicturePath = "" }
            };

            var vehicles = new HashSet<Vehicle>();

            foreach (var vehicle in vehicleParamters)
            {
                vehicles.Add(vehicle);
            }

            return vehicles;
        }

        private static IEnumerable<IssueStatus> SeedIssueStatuses()
        {
            id = 0;

            var statusParameters = new List<string>
            {
               "Done",
               "Repairing",
               "Awaiting",
               "No way to repair"
            };

            var statuses = new HashSet<IssueStatus>();

            foreach (var status in statusParameters)
            {
                statuses.Add(new IssueStatus
                {
                    Id = ++id,
                    Status = status
                });
            }

            return statuses;
        }

        private static IEnumerable<IssuePriority> SeedIssuePriorties()
        {
            id = 0;

            var prioritiesParameters = new List<(string priority, byte severity)>
            {
               ("Small", 1),
               ("Medium", 2),
               ("Large", 3)
            };

            var priorities = new HashSet<IssuePriority>();

            foreach (var priority in prioritiesParameters)
            {
                priorities.Add(new IssuePriority
                {
                    Id = ++id,
                    Priority = priority.priority,
                    Severity = priority.severity
                });
            }

            return priorities;
        }

        private static IEnumerable<Issue> SeedIssues()
        {
            id = 0;

            var issueParameters = new List<(long vehicleId, long statusId, string description, long priorityId)>
            {
               (1, 3, "Sounds like a trash metal!", 2),
               (2, 2, "The wheels are not symetric!", 3),
               (1, 2, "The wheels are not symetric!", 2)
            };

            var issues = new HashSet<Issue>();

            foreach (var issue in issueParameters)
            {
                issues.Add(new Issue
                {
                    Id = ++id,
                    VehicleId = issue.vehicleId,
                    StatusId = issue.statusId,
                    PriorityId = issue.priorityId,
                    Description = issue.description
                });
            }

            return issues;
        }

        private static IEnumerable<ExceptionLog> SeedExceptionLogs()
        {
            var exceptionParameters = new List<(Guid id, string message, string innerEx, string stackTrc, bool IsChecked)>
            {
               (new Guid("b526ce37-8b35-475d-9c18-f2740a935b34"), "Exception msg", "Inner ex random generated text", "Server explode", false),
               (new Guid("b526ce37-8b35-475d-9c18-f2740a935b35"), "Exception msg", "Inner ex random generated text", "Server explode", true),
               (new Guid("b526ce37-8b35-475d-9c18-f2740a935b36"), "Exception msg", "Inner ex random generated text", "Server explode", false),
               (new Guid("b526ce37-8b35-475d-9c18-f2740a935b37"), "Exception msg", "Inner ex random generated text", "Server explode", false),
            };

            var exceptionLogs = new HashSet<ExceptionLog>();

            foreach (var exceptionLog in exceptionParameters)
            {
                exceptionLogs.Add(new ExceptionLog
                {
                    Id = exceptionLog.id,
                    ExceptionMessage = exceptionLog.message,
                    InnerException = exceptionLog.innerEx,
                    StackTrace = exceptionLog.stackTrc,
                    IsChecked = exceptionLog.IsChecked,
                    CreatedOn = DateTime.Parse("11-01-2022")
                });
            }

            return exceptionLogs;
        }
    }
}