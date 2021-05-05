using CommonServices.DatabaseOperator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CommonServices.AccountServices;
using CommonServices.FileManager;
using CommonServices.HttpWebProxy;
using RestSharp;
using CommonServices.TicketServices;
using CommonServices.ClientService.TicketClient;
using CommonServices.ClientService.AccountClient;

namespace CommonServices
{
    public static class CommonServicesDependencyInjector
    {
        private static readonly string API_CONFIGURATION_OPTION = "CQuerLocalAPIURL";
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            AddDatabaseConnector(services);
            AddFileManager(services);
            AddAccountService(services);
            AddHttpWebClientProxy(services);
            AddTicketService(services);
            return services;
        }

        private static void AddDatabaseConnector(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnector, DatabaseConnector>();
        }
        private static void AddFileManager(this IServiceCollection services)
        {
            services.AddScoped<IFileManagerService, FileManagerService>();
        }
        private static void AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
        }
        private static void AddHttpWebClientProxy(this IServiceCollection services)
        {
            services.AddTransient<IHttpWebClientProxy, HttpWebClientProxy>();
        }
        public static void AddApiClientServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IRestClient, RestClient>();
            services.AddScoped<IAccountClientService>(provider => 
                new AccountClientService(provider.GetRequiredService<IRestClient>(), 
                    configuration.GetValue<string>(API_CONFIGURATION_OPTION))
            );
            services.AddScoped<ITicketClientService>(provider =>
                new TicketClientService(provider.GetRequiredService<IRestClient>(),
                    configuration.GetValue<string>(API_CONFIGURATION_OPTION))
            );
        }
        public static void AddTicketService(this IServiceCollection services)
        {
            services.AddScoped<ITicketService, TicketService>();
        }

    }
}
