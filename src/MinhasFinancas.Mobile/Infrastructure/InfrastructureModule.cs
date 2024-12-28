using Microsoft.Extensions.Configuration;

namespace MinhasFinancas.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
    {
        services.AddDbServices(configuration);

        //

        return services;
    }
}
