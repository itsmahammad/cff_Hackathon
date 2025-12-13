using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CffHackathon.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default");
        services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
        
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        return services;
    }
}
