using Common;
using Common.Exceptions;
using CommonServices;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CQuerMVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ValidateApiUrl(configuration);
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        private static void ValidateApiUrl(IConfiguration configuration)
        {
            AppSettingsValidator validatorHelper = new AppSettingsValidator(configuration, new ValidatorHelper());
            if (!validatorHelper.IsLocalApiUrlValid())
                throw new ConfigurationValidationException(validatorHelper.ErrorMessageContainer);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                })
                .AddCookie(options =>
                {
                    options.Cookie.Name = ".MVC";
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/Logout";
                    options.Cookie.HttpOnly = true;
                });
            services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddFluentValidation(x =>
                    x.RegisterValidatorsFromAssemblyContaining<Common.Validators.RegisterDtoValidator>());
            services.AddApiClientServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
      
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}