using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories
{
    public interface IBalanceRepository
    {
        Task<Balance> GetAmountSumByRange(int begin, int end);
    }
}