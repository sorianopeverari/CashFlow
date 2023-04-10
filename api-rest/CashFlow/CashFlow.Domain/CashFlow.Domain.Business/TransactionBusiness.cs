using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business
{
    public class TransactionBusiness : ITransactionBusiness
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
}