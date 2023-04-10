using System.Threading.Tasks;
using System;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<Transaction> Credit(Transaction transaction);

        Task<Transaction> Debit(Transaction transaction);
    }
}