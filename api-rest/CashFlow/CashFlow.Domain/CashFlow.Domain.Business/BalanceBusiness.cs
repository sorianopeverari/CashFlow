using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business
{
    public class BalanceBusiness : IBalanceBusiness
    {
        public async Task<Balance> GetAmountSumByRange(long begin, long end)
        {
            return await Task.FromResult(new Balance());
        }
    }
}
