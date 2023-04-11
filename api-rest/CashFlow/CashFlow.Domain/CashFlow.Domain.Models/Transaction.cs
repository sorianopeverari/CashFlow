namespace CashFlow.Domain.Models
{
    public class Transaction
    {
        public string Id { get;set; }
        
        public long EffectiveDate { get; set; }

        public double Amount { get; set; }
    }
}
