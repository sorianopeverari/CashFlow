using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business;

public class TransactionBusiness : ITransactionBusiness
{
    public async Task<Transaction> Execute(Transaction transaction)
    {
        return await Task.FromResult(transaction);
    }
}
