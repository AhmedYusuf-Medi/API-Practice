namespace CarShop.WebAPI.Configurations
{
    //Local
    using CarShop.Models.Response;
    using CarShop.Service.Common.Base;
    using CarShop.Service.Common.Providers.Mail.MailKit;
    using CarShop.Service.Common.Providers.Mail.SendGrid;
    //Nuget packets
    using CloudinaryDotNet;
    using SendGrid;
    //Public
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class StartUpConfigurations
    {
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