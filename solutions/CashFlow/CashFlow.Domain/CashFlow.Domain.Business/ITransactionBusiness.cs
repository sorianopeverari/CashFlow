using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Domain.Business
{
    public interface ITransactionBusiness
    {
        Task<Transaction> Credit(Transaction transaction);
        
        Task<Transaction> Debit(Transaction transaction);

        Task<double> GetSumAmout(long balanceDate);
    }
}
