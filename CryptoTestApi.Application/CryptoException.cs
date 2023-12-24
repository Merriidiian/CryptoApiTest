namespace CryptoTestApi.Application;

public class CryptoException: Exception
{
    public CryptoException(string message) : base(message)
    {
    }
}