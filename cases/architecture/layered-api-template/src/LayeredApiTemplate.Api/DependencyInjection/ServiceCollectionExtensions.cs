using LayeredApiTemplate.Application.Interfaces;
using LayeredApiTemplate.Application.Services;
using LayeredApiTemplate.Domain.Interfaces;
using LayeredApiTemplate.Infrastructure.Repositories;

namespace LayeredApiTemplate.Api.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IExampleService, ExampleService>();
        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IExampleRepository, InMemoryExampleRepository>();
        return services;
    }
}