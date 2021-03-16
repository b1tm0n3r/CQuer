using CommonServices.DatabaseOperator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CommonServices.AccountServices;
using CommonServices.ClientService;
using RestSharp;
using CommonServices.FileServices.FileManager;

namespace CommonServices
{
    public static class CommonServicesDependencyInjector
    {
        public static IServiceCollection AddDatabaseConnector(this IServiceCollection services)
        {
            services.AddScoped<IDatabaseConnector, DatabaseConnector>();
            return services;
        }
        public static IServiceCollection AddFileManager(this IServiceCollection services)
        {
            services.AddScoped<IFileManagerService, FileManagerService>();
            return services;
        }
        public static IServiceCollection AddAccountService(this IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            return services;
        }
        public static IServiceCollection AddClientService(this IServiceCollection services)
        {
            services.AddScoped<IAccountClientService, AccountClientService>();
            services.AddTransient<IRestClient, RestClient>();
            return services;
        }
    }
}
