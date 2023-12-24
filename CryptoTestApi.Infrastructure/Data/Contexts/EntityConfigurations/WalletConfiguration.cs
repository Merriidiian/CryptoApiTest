using CryptoTestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoTestApi.Infrastructure.Data.Contexts.EntityConfigurations;

public class WalletConfiguration : IEntityTypeConfiguration<Wallet>
{
    public void Configure(EntityTypeBuilder<Wallet> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
        entityTypeBuilder
            .HasOne<Currency>(e => e.Currency)
            .WithMany(e => e.Wallets)
            .HasForeignKey(e => e.CurrencyId);
        entityTypeBuilder
            .HasOne<User>(e => e.User)
            .WithMany(e => e.Wallets)
            .HasForeignKey(e => e.UserId);
    }
}