using CryptoTestApi.Domain.Models;
using CryptoTestApi.Infrastructure.Data.Contexts;
using CryptoTestApi.Infrastructure.Repositories.Interfaces;

namespace CryptoTestApi.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly CryptoContext _context;


    public UserRepository(CryptoContext context)
    {
        _context = context;
    }


    public async Task<User> SelectByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<ICollection<User>> SelectAllAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<Guid> InsertAsync(User user, CancellationToken cancellationToken)
    {
        var newUser = await _context.Users.AddAsync(user, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return newUser.Entity.Id;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> UpdateAsync(User obj, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}