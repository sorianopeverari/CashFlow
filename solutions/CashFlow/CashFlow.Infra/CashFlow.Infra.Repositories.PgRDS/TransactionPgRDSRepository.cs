
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using CashFlow.Infra.Repositories.Providers;
using CashFlow.Domain.Models.Utils;

namespace CashFlow.Infra.Repositories.PgRDS
{
    public class TransactionPgRDSRepository : AbstracPgDapperRepository, ITransactionRepository 
    {
        public TransactionPgRDSRepository() : base("Server=localhost;Port=5432;Database=cash_flow_control;User Id=postgres;Password=Postgres@2023;")
        {
        }

        public async Task Create(Transaction transaction)
        {
            Guid id = Guid.NewGuid();
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("effective_date", DateUtil.ToDateTime(transaction.EffectiveDate), DbType.DateTime);
            parameters.Add("amount", transaction.Amount, DbType.Double);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"INSERT INTO public.transaction(id, effective_date, amount)
                                    VALUES (CAST(@id AS uuid), @effective_date, @amount)";
                try
                {
                    conn.Open();
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);

                    if(rowsAffected <= 0)
                    {
                        throw new Exception("Error trying insert transaction.");
                    }
                    transaction.Id = id.ToString();
                }
                finally
                {
                    conn?.Close();
                }
            }
        }

        public async Task CreateIfBalancePositive(Transaction transaction, long balanceDate)
        {
            Guid id = Guid.NewGuid();

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", id, DbType.Guid);
            parameters.Add("effective_date", DateUtil.ToDateTime(transaction.EffectiveDate), DbType.DateTime);
            parameters.Add("balance_date", DateUtil.ToDateTime(balanceDate), DbType.Date);
            parameters.Add("amount", transaction.Amount, DbType.Double);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"INSERT INTO public.transaction(id, effective_date, amount)
                                    SELECT CAST(@id AS uuid)
                                    ,@effective_date AS effective_date
                                    ,@amount AS amount 
                                    WHERE EXISTS(SELECT tr.effective_date::date as effective_date
                                        ,SUM(amount) AS amount
                                        FROM public.transaction AS tr
                                        WHERE tr.effective_date::date = @balance_date
                                        GROUP BY tr.effective_date::date
                                        HAVING SUM(tr.amount)+(@amount) >= 0)";
                try
                {
                    conn.Open();
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);

                    if(rowsAffected <= 0)
                    {
                        throw new Exception(@"Error trying insert transaction where balance is positive");
                    }

                    transaction.Id = id.ToString();
                }
                finally
                {
                    conn?.Close();
                }
            }
        }

        public async Task<double> GetSumAmout(long balanceDate)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("balance_date", DateUtil.ToDateTime(balanceDate), DbType.Date);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"SELECT SUM(amount) AS Amount
                                    FROM public.transaction AS tr
                                    WHERE tr.effective_date::date = @balance_date
                                    GROUP BY tr.effective_date::date";
                try
                {
                    conn.Open();
                    return await conn.QuerySingleAsync<double>(query, parameters);
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}