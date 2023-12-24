using CryptoTestApi.Domain.Models;

namespace CryptoTestApi.Request.WalletDTO;

public class BalanceActionDTO
{
    public record Request(Guid IdUser, Guid IdCurrency, Domain.Models.BalanceAction WalletBalanceAction, double sum);
    public record Response(Wallet Wallet);
}