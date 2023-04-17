namespace CashFlow.Domain.Models.Utils
{
    public static class DateUtil
    {
        public static DateTime ToDateTime(long timestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds((double)timestamp).ToUniversalTime();
            return epoch;
        }

        public static long ToTimestamp(DateTime dateTime)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = dateTime - epoch;
            return (long)elapsedTime.TotalSeconds;
        }
    }
}