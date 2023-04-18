using System.Threading.Tasks;
using System;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> Create(Transaction transaction);

        Task<Transaction> CreateIfBalancePositive(Transaction transaction, long balanceDate);

        Task<double> GetSumAmout(long balanceDate);
    }
}