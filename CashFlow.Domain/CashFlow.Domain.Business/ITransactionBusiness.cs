namespace CashFlow.Domain.Business;

using System.Threading.Tasks;
using CashFlow.Domain.Models;

public interface ITransactionBusiness
{
    Task<Transaction> Execute(Transaction transaction);
}
