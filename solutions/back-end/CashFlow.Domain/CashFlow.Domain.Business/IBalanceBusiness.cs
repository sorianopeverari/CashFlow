using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Domain.Business
{
   public interface IBalanceBusiness
   {
      Task<Balance> GetAmountSumByRange(long begin, long end, BalanceType balanceType);

      Task Create(long effectiveDate, double amountSum, BalanceType balanceType);
   }
}
