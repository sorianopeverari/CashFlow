using CashFlow.Domain.Models.Enums;
using System;

namespace CashFlow.Domain.Models
{
    public class Balance
    {
        public long Begin { get; set; }

        public long End { get; set; }

        public IEnumerable<Transaction>? Transactions { get; set; }

        public BalanceType Type { get; set; }
    }
}
