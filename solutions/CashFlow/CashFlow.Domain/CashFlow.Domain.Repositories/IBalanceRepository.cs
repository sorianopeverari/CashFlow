using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> GetAmountSumByRange(long begin, long end);

        Task Create(long day, double amountSum);
    }
}
