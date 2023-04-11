namespace CashFlow.Domain.Business
{
    public abstract class AbstracBusiness
    {
        protected long DateTimeToTimeStamp(DateTime value)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            TimeSpan elapsedTime = value - epoch;
            return (long) elapsedTime.TotalSeconds;
        }
    }
}
