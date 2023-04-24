namespace CashFlow.Domain.Business.Providers
{
    public interface IDateTimeProvider
    {
        long UtcNow();
    }
}