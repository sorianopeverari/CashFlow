using System.Threading.Tasks;
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;
using System.Data;
using Dapper;
using CashFlow.Infra.Repositories.Providers;

namespace CashFlow.Infra.Repositories.PgDW
{
    public class BalancePgDWRepository : AbstracPgDapperRepository, IBalanceRepository
    {
        public BalancePgDWRepository() : base("Server=localhost;Port=5433;Database=cash_flow_report;User Id=postgres;Password=Postgres@2023;")
        {
        }

        public async Task<Balance> Create(Balance balance)
        {
           return await Task.FromResult(new Balance());
        }

        public Task<Balance> GetAmountSumByRange(long begin, long end)
        {
            throw new NotImplementedException();
        }
    }
}
