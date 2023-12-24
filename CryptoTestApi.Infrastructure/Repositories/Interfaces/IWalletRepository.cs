using CryptoTestApi.Domain.Models;

namespace CryptoTestApi.Infrastructure.Repositories.Interfaces;

public interface IWalletRepository : ICrudRepository<Wallet>
{
    Task<Wallet?> FindWalletAsync(Guid idUser, Guid idCurrency, CancellationToken cancellationToken);
}