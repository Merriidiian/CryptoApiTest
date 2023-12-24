using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Data.Contexts;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;

namespace CryptoTestApi.Infrastructure.Repositories;

public class CurrencyRepository : ICurrencyRepository
{
    private readonly CryptoContext _context;


    public CurrencyRepository(CryptoContext context)
    {
        _context = context;
    }
    public async Task<Currency> SelectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<Currency>> SelectAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertAsync(Currency currency, CancellationToken cancellationToken)
    {
        var newCurrency = await _context.Currencies.AddAsync(currency, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newCurrency.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(Currency obj, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}