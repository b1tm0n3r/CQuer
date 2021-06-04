using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace Persistence
{
    public static class PersistanceDependencyInjector
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<CQuerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("CQuerDB"));
            });
            services.AddScoped<ICQuerDbContext>(provider => provider.GetService<CQuerDbContext>());

            return services;
        }
    }
}
