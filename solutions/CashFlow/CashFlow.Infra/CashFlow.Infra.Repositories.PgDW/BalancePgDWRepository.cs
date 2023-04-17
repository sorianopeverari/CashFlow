using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;
using System.Data;
using Dapper;
using CashFlow.Infra.Repositories.Providers;
using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Models.Enums;

namespace CashFlow.Infra.Repositories.PgDW
{
    public class BalancePgDWRepository : AbstracPgDapperRepository, IBalanceRepository
    {
        public BalancePgDWRepository() : base("Server=localhost;Port=5433;Database=cash_flow_report;User Id=postgres;Password=Postgres@2023;")
        {
        }

        public async Task Create(long day, double amountSum)
        {
            string time_id = await this.CreateTime(day);
            await this.CreateFactBalance(time_id, amountSum);
        }

        private async Task CreateFactBalance(string time_id, double amountSum)
        {   
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("time_id", new Guid(time_id), DbType.Guid);
            parameters.Add("transaction_amount_sum", amountSum, DbType.Int32);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"INSERT INTO public.fact_balance(time_id, transaction_amount_sum)
                                    VALUES (CAST(@time_id AS uuid), @transaction_amount_sum)";
                try
                {
                    conn.Open();
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);

                    if(rowsAffected <= 0)
                    {
                        throw new Exception("Error trying insert fact balance.");
                    }
                }
                finally
                {
                    conn?.Close();
                }
            }
        }

        private async Task<string> CreateTime(long day)
        {
            DateTime epoch = DateUtil.ToDateTime(day);
            
            Guid id = Guid.NewGuid();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("monthh", epoch.Month, DbType.Int32);
            parameters.Add("dayy", epoch.Day, DbType.Int32);
            parameters.Add("yearr", epoch.Year, DbType.Int32);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"INSERT INTO public.d_time(id, monthh, dayy, yearr)
                                    VALUES (CAST(@id AS uuid), @monthh, @dayy, @yearr)";
                try
                {
                    conn.Open();
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);

                    if(rowsAffected <= 0)
                    {
                        throw new Exception("Error trying insert time.");
                    }
                }
                finally
                {
                    conn?.Close();
                }
            }

            return id.ToString();
        }

        public async Task<Balance> GetAmountSumByRange(long begin, long end)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("beginn", DateUtil.ToDateTime(begin).AddDays(-1), DbType.Date);
            parameters.Add("endd", DateUtil.ToDateTime(end).AddDays(1), DbType.Date);
            
            Console.WriteLine("");
            Console.WriteLine(DateUtil.ToDateTime(begin));
            Console.WriteLine(DateUtil.ToDateTime(end));
            Console.WriteLine("");

            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"SELECT
                                    '' AS Id
                                    ,EXTRACT(epoch FROM make_date(dt.yearr, dt.dayy, dt.monthh))::bigint AS EffectiveDate
                                    ,fb.transaction_amount_sum AS Amount
                                    FROM public.d_time dt
                                INNER JOIN public.fact_balance fb
                                ON dt.id = fb.time_id
                                WHERE make_date(dt.yearr, dt.dayy, dt.monthh)::date > @beginn
                                AND make_date(dt.yearr, dt.dayy, dt.monthh)::date < @endd";
                try
                {
                    conn.Open();

                    IEnumerable<Transaction> transactions = await conn.QueryAsync<Transaction>(query, parameters);

                     Console.WriteLine("");

                    return new Balance()
                    {
                        Begin = begin,
                        End = end,
                        Type = BalanceType.SumByDay,
                        Transactions = transactions
                    };
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}
