using CashFlow.Domain.Models.Utils;

namespace CashFlow.Domain.Business.Providers
{
    public class DateTimeProvider : IDateTimeProvider
    {
        public long UtcNow()
        {
            return DateUtil.ToTimestamp(DateTime.UtcNow); 
        }
    }
}