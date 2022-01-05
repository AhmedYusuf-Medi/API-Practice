namespace CarShop.WebAPI
{
    //Local
    using CarShop.Data;
    using CarShop.Data.ModelBuilderExtension.Seeder;
    using CarShop.WebAPI.Configurations;
    //Nuget packets
    using Microsoft.EntityFrameworkCore;
    //Public
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using CarShop.WebAPI.Helpers;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            StartUpConfigurations.ConfigureDatabase(services, this.Configuration);
            StartUpConfigurations.ConfigureAuthentication(services);
            StartUpConfigurations.ConfigureSwagger(services);
            StartUpConfigurations.ConfigureServicesLifeCycle(services);
            StartUpConfigurations.ConfigureCloudinary(services);
            StartUpConfigurations.ConfigureMailKit(services);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                using (var serviceScope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = serviceScope.ServiceProvider.GetRequiredService<CarShopDbContext>();

                    if (!env.IsDevelopment())
                    {
                        dbContext.Database.Migrate();
                    }

                    new CarShopDbContextSeeder()
                        .SeedAsync(dbContext)
                        .GetAwaiter()
                        .GetResult();
                }

                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Forum"));
            }

            app.UseExceptionHandlingMiddleware();

            app.UseRouting();

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict
            };

            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors(
                  options => options.WithOrigins("*").AllowAnyMethod()
              );

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}