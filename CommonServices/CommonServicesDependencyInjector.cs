﻿using CommonServices.DatabaseOperator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using CommonServices.AccountServices;
using CommonServices.FileManager;
using CommonServices.HttpWebProxy;
using RestSharp;
using CommonServices.ClientService;
using CommonServices.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace CommonServices
{
    public static class CommonServicesDependencyInjector
    {
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            AddDatabaseConnector(services);
            AddFileManager(services);
            AddAccountService(services);
            AddHttpWebClientProxy(services);
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
        public static void AddClientService(this IServiceCollection services)
        {
            services.AddScoped<IAccountClientService, AccountClientService>();
            services.AddTransient<IRestClient, RestClient>();
        }
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => 
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenSecretKey"])),
                        ValidateAudience = false,
                        ValidateIssuer = false
                    };
                });

            return services;
        }
        public static IServiceCollection AddTokenService(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService.TokenService>();
            return services;
        }
    }
}
