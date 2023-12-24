using CryptoTestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoTestApi.Infrastructure.Data.Contexts.EntityConfigurations;

public class CurrencyConfiguration: IEntityTypeConfiguration<Currency>
{
    public void Configure(EntityTypeBuilder<Currency> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);
    }
}