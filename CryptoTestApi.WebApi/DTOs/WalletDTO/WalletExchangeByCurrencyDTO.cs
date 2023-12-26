namespace CryptoTestApi.DTOs.WalletDTO;

public class WalletExchangeByCurrencyDTO
{
    public record Request(Guid IdUser, Guid IdCurrencyWithdraw, Guid IdCurrencyReplenishment, double ExchangeRate,
        double Sum, double CommissionPercentage = 0.05);
    public record Response(string Result = "Успешно!");
}