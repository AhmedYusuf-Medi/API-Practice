namespace CarShop.WebAPI.Configurations
{
    //Local
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Providers.Mail.MailKit;
    using CarShop.Service.Common.Providers.Mail.SendGrid;
    using CarShop.Models.Base;
    using CarShop.Service.Data.Exception;
    using CarShop.Service.Data.VehicleBrand;
    using CarShop.Service.Account.Data;
    using CarShop.Service.Data.User;
    using CarShop.Service.Common.Providers.Cloudinary;
    using CarShop.Data;
    using CarShop.Service.Data.VehicleType;
    using CarShop.Service.Data.IssueStatus;
    using CarShop.Service.Data.IssuePriority;
    using CarShop.Service.Data.Vehicle;
    using CarShop.Service.Data.Issue;
    using CarShop.Service.Data.Report;
    using CarShop.Service.Data.ReportType;
    //Nuget packets
    using CloudinaryDotNet;
    using SendGrid;
    using Microsoft.EntityFrameworkCore;
    //Public
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.OpenApi.Models;
    using System.Reflection;
    using System.IO;
    using System;
    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.Extensions.Configuration;

    public static class StartUpConfigurations
    {
        public static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CarShopDbContext>(options =>
             options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        }

        public static void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(o =>
                    {
                        o.Cookie.Name = "auth_cookie";
                        o.SlidingExpiration = true;
                        o.ExpireTimeSpan = TimeSpan.FromDays(1);
                    });
        }

        public static void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Car Shop" });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        public static void ConfigureServicesLifeCycle(IServiceCollection services)
        {
            services.AddSingleton<PasswordHasher<User>>();
            services.AddScoped<IExceptionLogService, ExceptionLogService>();
            services.AddScoped<IVehicleBrandService, VehicleBrandService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IIssueStatusService, IssueStatusService>();
            services.AddScoped<IIssuePriorityService, IssuePriorityService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReportTypeService, ReportTypeService>();
            services.AddScoped<ICloudinaryService, CloudinaryService>();
        }

        public static void ConfigureSendGrid(IServiceCollection services)
        {
            services.AddScoped<IMailSender<Response>>
                (serviceProvider => new MailSenderGrid(ExternalProviders.SendGrid_ApiKey));
        }

        public static void ConfigureMailKit(IServiceCollection services)
        {
            services.AddScoped<IMailSender<InfoResponse>>
                (serviceProvider => new MailSenderKit());
        }

        public static void ConfigureCloudinary(IServiceCollection services)
        {
            Cloudinary cloudinary = new Cloudinary(new Account
             (
                ExternalProviders.Cloudinary_Name,
                ExternalProviders.Cloudinary_Key,
                ExternalProviders.Cloudinary_Secret
             ));

            services.AddSingleton(cloudinary);
        }
    }
}