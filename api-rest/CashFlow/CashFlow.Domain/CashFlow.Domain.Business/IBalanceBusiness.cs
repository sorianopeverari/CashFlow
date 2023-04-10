using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business
{
   public interface IBalanceBusiness
   {
      Task<Balance> GetAmountSumByRange(long begin, long end);
   }
}
