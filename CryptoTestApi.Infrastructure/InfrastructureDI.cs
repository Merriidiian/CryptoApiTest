using CryptoTestApi.Infrastructure.Data.Contexts;
using CryptoTestApi.Infrastructure.Repositories;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CryptoTestApi.Infrastructure;

public static class InfrastructureDI
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CryptoContext>(
            options => options.UseNpgsql(
                configuration.GetConnectionString("CryptoConnection")
            )
        );
        services.AddTransient<ICurrencyRepository, CurrencyRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IWalletRepository, WalletRepository>();
        return services;
        
    }
}