namespace CashFlow.Infra.Repositories.PgRDS;

using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;

public class TransactionPgRDSRepository : ITransactionRepository
{
     public async Task<Transaction> Credit(Transaction transaction)
     {
        return await Task.FromResult(new Transaction());
     }

    public async Task<Transaction> Debit(Transaction transaction)
    {
        return await Task.FromResult(new Transaction());
    }
}