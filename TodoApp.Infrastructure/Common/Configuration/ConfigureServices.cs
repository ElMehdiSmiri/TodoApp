using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TodoApp.Infrastructure.Context;

namespace TodoApp.Infrastructure.Common.Configuration
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMongoDB(
                configuration["MongoDbSettings:ConnectionString"]!,
                configuration["MongoDbSettings:DatabaseName"]!
                ));

            return services;
        }
    }
}