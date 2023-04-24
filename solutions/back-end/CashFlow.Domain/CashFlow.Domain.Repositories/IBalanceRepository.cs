using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> GetAmountSumByRange(long beginn, long endd, BalanceType balanceType);

        Task Create(long time, double amountSum, BalanceType balanceType);
    }
}
