using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> GetAmountSumByRange(long begin, long end);

         Task<Balance> Create(Balance balance);
    }
}