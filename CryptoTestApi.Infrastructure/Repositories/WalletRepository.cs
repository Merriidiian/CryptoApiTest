using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Data.Contexts;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;

namespace CryptoTestApi.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly CryptoContext _context;

    public WalletRepository (CryptoContext context)
    {
        _context = context;
    }

    public async Task<Wallet> SelectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Wallet>> SelectAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertAsync(Wallet obj, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Wallet obj, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> InsertAllCurrencyNewUserAsync(Guid idUser, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<List<string>> InsertAllUserNewCurrencyAsync(Guid idCurrency, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}