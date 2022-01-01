﻿namespace CarShop.WebAPI.Configurations
{
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Providers.SendGrid;
    using CloudinaryDotNet;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartUpConfigurations
    {
        public static void ConfigureEmailSender(IServiceCollection services)
        {
            services.AddScoped<IMailSender>
                (serviceProvider => new MailSender(ExternalProviders.SendGrid_Key));
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