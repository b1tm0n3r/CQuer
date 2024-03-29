using System;
using System.Threading.Tasks;
using Common.DataModels.IdentityManagement;
using Common.DTOs;
using CommonServices.AccountServices;
using CommonServices.FileManager;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Persistence.Context;

namespace CQuerAPI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();
            await ConfigureDatabase(host);
            await CreateDefaulAdminAccount(host);
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        public static async Task ConfigureDatabase(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var context = services.GetRequiredService<ICQuerDbContext>();
            //Testing purposes (exception thrown if directory in DefaultFileStorePath doesn't exist)
            services.GetRequiredService<IFileManagerService>();
            try
            {
                await context.Database.EnsureCreatedAsync();
            }
            catch(Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError("Error during db configuration: {0}", ex);
            }
        }

        public static async Task CreateDefaulAdminAccount(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            var accountService = services.GetRequiredService<IAccountService>();
            var configurationService = services.GetRequiredService<IConfiguration>();

            if (!await accountService.AccountExists(configurationService.GetSection("DefaultStartUpAdminDetails").GetValue<string>("Login")))
            {
                await accountService.Register(new RegisterDto
                {
                    Username = configurationService.GetSection("DefaultStartUpAdminDetails").GetValue<string>("Login"),
                    Password = configurationService.GetSection("DefaultStartUpAdminDetails").GetValue<string>("Password"),
                    AccountType = AccountType.Administrator
                });
            }
        }
    }
}