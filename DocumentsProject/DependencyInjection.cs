using FluentValidation.AspNetCore;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddMVCServices(this IServiceCollection services)
    {
        services.AddFluentValidationClientsideAdapters();
        return services;
    }
}
