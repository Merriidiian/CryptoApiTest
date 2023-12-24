using CryptoTestApi.Infrastructure;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoTestApi.Application;

public static class ApplicationDI
{
    public static IServiceCollection AddApplication(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddDatabase(configuration);
        var assembly = typeof(ApplicationDI).Assembly;

        services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return services;
    }
}