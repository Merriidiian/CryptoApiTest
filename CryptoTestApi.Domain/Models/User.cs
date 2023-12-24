namespace CryptoTestApi.Domain.Models;

public class User
{
    public Guid Id { get; set; }
    public string FullName { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }

    public virtual ICollection<Wallet> Wallets { get; }
}