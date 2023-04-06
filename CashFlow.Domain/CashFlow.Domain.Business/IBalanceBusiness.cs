namespace CashFlow.Domain.Business;

using System.Threading.Tasks;
using CashFlow.Domain.Models;

public interface IBalanceBusiness
{
   Task<IEnumerable<Balance>> GetByTime(int month, int day, int year);
}
