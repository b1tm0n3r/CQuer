using Common;
using Common.Exceptions;
using Common.MappingProfiles;
using CommonServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Persistence;

namespace CQuerAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            ValidateConfigurationSettings(configuration);
            Configuration = configuration;
        }

        private static void ValidateConfigurationSettings(IConfiguration configuration)
        {
            AppSettingsValidator validatorHelper = new AppSettingsValidator(configuration, new ValidatorHelper());
            if(!validatorHelper.IsConnectionStringValid() || !validatorHelper.IsFileStorePathValid() || !validatorHelper.IsLocalApiUrlValid())
                throw new ConfigurationValidationException(validatorHelper.ErrorMessageContainer);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "CQuerAPI", Version = "v1"}); });
            services.AddPersistence(Configuration);
            services.AddCommonServices();
            services.AddAutoMapper(x=>x.AddProfile<AccountMapperProfile>(), typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CQuerAPI v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}