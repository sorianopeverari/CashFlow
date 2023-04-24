using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;
using System.Data;
using Dapper;
using CashFlow.Infra.Repositories.Providers;
using CashFlow.Domain.Models.Utils;
using CashFlow.Domain.Models.Enums;
using System.Transactions;
using Transaction = CashFlow.Domain.Models.Transaction;
using CashFlow.Domain.Repositories.Exceptions;
using Npgsql;

namespace CashFlow.Infra.Repositories.PgDW
{
    public class BalancePgDWRepository : AbstracPgDapperRepository, IBalanceRepository
    {
        public BalancePgDWRepository() : base("Server=localhost;Port=5433;Database=cash_flow_report;User Id=postgres;Password=Postgres@2023;")
        {
        }

        public async Task Create(long time, double transactionAmountSum, BalanceType balanceType)
        {
            using(TransactionScope ts = new TransactionScope())
            {
                string time_id = await this.CreateTime(time);
                await this.CreateFactBalance(time_id, transactionAmountSum);
                ts.Complete();
            }
        }

        private async Task CreateFactBalance(string time_id, double transactionAmountSum)
        {   
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("time_id", new Guid(time_id), DbType.Guid);
            parameters.Add("transaction_amount_sum", transactionAmountSum, DbType.Int32);
            
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
                        throw new UnexpectedRepositoryException("Register don't persisted");
                    }
                }
                catch(TimeoutException ex)
                {
                    throw new UnavailableRepositoryException(ex.Message, ex);
                }
                catch(NpgsqlException ex)
                {
                    throw new UnexpectedRepositoryException(ex.Message, ex);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }

        private async Task<string> CreateTime(long time)
        {
            DateTime dateTime = DateUtil.ToDateTime(time);
            Guid id = Guid.NewGuid();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("monthh", dateTime.Month, DbType.Int32);
            parameters.Add("dayy", dateTime.Day, DbType.Int32);
            parameters.Add("yearr", dateTime.Year, DbType.Int32);
            
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
                        throw new UnexpectedRepositoryException("Register don't persisted");
                    }
                }
                catch(PostgresException ex)
                {
                    throw new UnexpectedRepositoryException(ex.Message, ex);
                }
                finally
                {
                    conn?.Close();
                }
            }
            return id.ToString();
        }

        public async Task<Balance> GetAmountSumByRange(long begin, long end, BalanceType balanceType)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("beginn", DateUtil.ToDateTime(begin).AddDays(-1), DbType.Date);
            parameters.Add("endd", DateUtil.ToDateTime(end).AddDays(1), DbType.Date);
            
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
                    
                    return new Balance()
                    {
                        Begin = begin,
                        End = end,
                        Type = BalanceType.SumByDay,
                        Transactions = transactions
                    };
                }
                catch(PostgresException ex)
                {
                    throw new UnexpectedRepositoryException(ex.Message, ex);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}
