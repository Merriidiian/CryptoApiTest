namespace CryptoTestApi.Domain.Models;

public class Currency
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }

    public virtual ICollection<Wallet> Wallets { get; }
}