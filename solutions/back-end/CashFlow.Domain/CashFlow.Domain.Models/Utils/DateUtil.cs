namespace CashFlow.Domain.Models.Utils
{
    public static class DateUtil
    {
        public static DateTime ToDateTime(long timestamp)
        {
            return DateTime.UnixEpoch.AddSeconds((double)timestamp).ToUniversalTime();
        }

        public static long ToTimestamp(DateTime dateTime)
        {;
            return (long)(dateTime - DateTime.UnixEpoch).TotalSeconds;
        }

        public static long ToTimestampOnlyDate(DateTime dateTime)
        {
            return (long)(dateTime.Date - DateTime.UnixEpoch).TotalSeconds;
        }

        public static long ToTimestampOnlyDate(long timestamp)
        {
            return (long)(ToDateTime(timestamp).Date - DateTime.UnixEpoch).TotalSeconds;
        }
    }
}