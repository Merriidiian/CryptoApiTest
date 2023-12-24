namespace CryptoTestApi.Domain.Models;

public class Wallet
{
    public Guid Id { get; set; }
    public double Balance { get; set; }
    public DateTimeOffset DateTimeCreated { get; set; }
    public Guid CurrencyId { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; } = null!;
    public virtual Currency Currency { get; set; } = null!;
}