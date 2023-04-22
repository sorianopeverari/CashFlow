using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Enums;
using CashFlow.Domain.Repositories;

namespace CashFlow.Domain.Business
{
    public class BalanceBusiness : IBalanceBusiness
    {
        private readonly IBalanceRepository _balanceRepository;

        public BalanceBusiness(IBalanceRepository balanceRepository)
        {
            _balanceRepository = balanceRepository;
        }

        public async Task Create(long effectiveDate, double amountSum, BalanceType balanceType)
        {
            await _balanceRepository.Create(effectiveDate, amountSum, balanceType);
        }

        public async Task<Balance> GetAmountSumByRange(long begin, long end, BalanceType balanceType)
        {
            return await _balanceRepository.GetAmountSumByRange(begin, end, balanceType);
        }
    }
}
