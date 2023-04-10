using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;

namespace CashFlow.Infra.Repositories.PgDW
{
    public class BalancePgDWRepository : IBalanceRepository
    {
        public async Task<Balance> GetAmountSumByRange(int begin, int end)
        {
            return await Task.FromResult(new Balance());
        }
    }
}
