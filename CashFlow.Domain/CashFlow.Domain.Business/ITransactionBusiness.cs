using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business
{
    public interface ITransactionBusiness
    {
        Task<Transaction> Credit(Transaction transaction);


        Task<Transaction> Debit(Transaction transaction);
    }
}
