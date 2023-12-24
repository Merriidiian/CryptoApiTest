using CryptoTestApi.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CryptoTestApi.Infrastructure.Data.Contexts.EntityConfigurations;

public class UserConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> entityTypeBuilder)
    {
        entityTypeBuilder.HasKey(x => x.Id);;
    }
}