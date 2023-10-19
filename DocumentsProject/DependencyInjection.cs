using DocumentsProject.Services;
using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddFluentValidationClientsideAdapters();
        services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection"))
                .AddCheck<HealthCheck>("HealthCheck");
        return services;
    }
}
