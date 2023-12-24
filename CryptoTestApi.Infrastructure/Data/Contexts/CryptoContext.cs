using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Data.Contexts.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CryptoTestApi.Infrastructure.Data.Contexts;

public class CryptoContext : DbContext
{
    public CryptoContext(DbContextOptions<CryptoContext> options) : base(options)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Wallet> Wallets { get; set; }
    public DbSet<Currency> Currencies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new CurrencyConfiguration());
        modelBuilder.ApplyConfiguration(new WalletConfiguration());
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        if (!optionsBuilder.IsConfigured) 
        {
            throw new InvalidOperationException("Context was not configured");
        }
        base.OnConfiguring(optionsBuilder);
    }
}