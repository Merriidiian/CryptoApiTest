using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Data.Contexts;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CryptoTestApi.Infrastructure.Repositories;

public class WalletRepository : IWalletRepository
{
    private readonly CryptoContext _context;

    public WalletRepository(CryptoContext context)
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

    public async Task<Guid> InsertAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        var newWallet = await _context.Wallets.AddAsync(wallet, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newWallet.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Wallet wallet, CancellationToken cancellationToken)
    {
        try
        {
            _context.Wallets.Update(wallet);
            await _context.SaveChangesAsync(cancellationToken);
            return true;
        }
        catch (Exception e)
        {
            return false;
        }
    }

    public async Task<Wallet?> FindWalletAsync(Guid idUser, Guid idCurrency, CancellationToken cancellationToken)
    {
        var wallet = await _context.Wallets.FirstOrDefaultAsync(w => w.UserId == idUser && w.CurrencyId == idCurrency, cancellationToken: cancellationToken);
        return wallet ?? null;
    }
}