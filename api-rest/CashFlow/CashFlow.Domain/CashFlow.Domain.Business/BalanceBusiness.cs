﻿using System.Threading.Tasks;
using CashFlow.Domain.Models;
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

        public async Task<Balance> Create(Balance balance)
        {
            await _balanceRepository.Create(balance);
            throw new NotImplementedException();
        }

        public async Task<Balance> GetAmountSumByRange(long begin, long end)
        {
            return await Task.FromResult(new Balance());
        }
    }
}
