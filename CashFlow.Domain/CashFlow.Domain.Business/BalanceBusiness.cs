using System.Threading.Tasks;
using CashFlow.Domain.Models;

namespace CashFlow.Domain.Business;

public class BalanceBusiness : IBalanceBusiness
{
    public async Task<Balance> Execute(Balance balance)
    {
        return await Task.FromResult(balance);
    }
}
