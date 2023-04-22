using System.Threading.Tasks;
using System;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task Create(Transaction transaction);

        Task CreateIfBalancePositive(Transaction transaction, long balanceDate);

        Task<double> GetSumAmout(long balanceDate);
    }
}