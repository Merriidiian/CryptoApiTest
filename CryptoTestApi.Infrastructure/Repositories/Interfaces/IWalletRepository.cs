using CryptoTestApi.Domain.Models;

namespace CryptoTestApi.Infrastructure.Repositories.Interfaces;

public interface IWalletRepository : ICrudRepository<Wallet>
{
    Task<List<string>> InsertAllCurrencyNewUserAsync(Guid idUser, CancellationToken cancellationToken);
    Task<List<string>> InsertAllUserNewCurrencyAsync(Guid idCurrency, CancellationToken cancellationToken);
}