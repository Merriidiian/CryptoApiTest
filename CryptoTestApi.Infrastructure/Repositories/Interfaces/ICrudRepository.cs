namespace CryptoTestApi.Infrastructure.Repositories.Interfaces;

public interface ICrudRepository<T>
{
    Task<T> SelectByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<ICollection<T>> SelectAllAsync(CancellationToken cancellationToken);
    Task<Guid> InsertAsync(T obj, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(T obj, CancellationToken cancellationToken);
}
