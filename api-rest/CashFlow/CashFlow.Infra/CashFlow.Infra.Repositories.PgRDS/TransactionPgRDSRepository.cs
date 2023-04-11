
using CashFlow.Domain.Models;
using CashFlow.Domain.Repositories;
using System.Data;
using Dapper;
using System.Linq;
using CashFlow.Infra.Repositories.Providers;

namespace CashFlow.Infra.Repositories.PgRDS
{
    public class TransactionPgRDSRepository : AbstracPgDapperRepository, ITransactionRepository 
    {
        public TransactionPgRDSRepository() : base("Server=localhost;Port=5432;Database=cash_flow_control;User Id=postgres;Password=Postgres@2023;")
        {
        }

        public async Task<Transaction> Credit(Transaction transaction)
        {
            Transaction transactionCreated = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                EffectiveDate = transaction.EffectiveDate,
                Amount = transaction.Amount
            };

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", new Guid(transactionCreated.Id), DbType.Guid);
            parameters.Add("effective_date", base.TimeStampToDateTime(transactionCreated.EffectiveDate), DbType.DateTime);
            parameters.Add("amount", transactionCreated.Amount, DbType.Double);
            
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
                        throw new Exception("Error trying insert credit transaction");
                    }
                }
                finally
                {
                    conn?.Close();
                }
            }

            return transactionCreated;
        }

        public async Task<Transaction> Debit(Transaction transaction)
        {
            Transaction transactionCreated = new Transaction()
            {
                Id = Guid.NewGuid().ToString(),
                EffectiveDate = transaction.EffectiveDate,
                Amount = transaction.Amount
            };

            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("id", new Guid(transactionCreated.Id), DbType.Guid);
            parameters.Add("effective_date", base.TimeStampToDateTime(transactionCreated.EffectiveDate), DbType.DateTime);
            parameters.Add("amount", transactionCreated.Amount, DbType.Double);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"INSERT INTO public.transaction(id, effective_date, amount)
                                    SELECT CAST(@id AS uuid)
                                    ,@effective_date AS effective_date
                                    ,@amount AS amount 
                                    WHERE EXISTS(SELECT tr.effective_date::date as effective_date
                                        ,SUM(amount) AS amount
                                        FROM public.transaction AS tr
                                        WHERE tr.effective_date::date = CAST(@effective_date AS DATE)
                                        GROUP BY tr.effective_date::date
                                        HAVING SUM(tr.amount)+(@amount) >= 0)";
                try
                {
                    conn.Open();
                    int rowsAffected = await conn.ExecuteAsync(query, parameters);

                    if(rowsAffected <= 0)
                    {
                        throw new Exception("Error trying insert debit transaction");
                    }
                }
                finally
                {
                    conn?.Close();
                }
            }

            return transactionCreated;
        }

        public async Task<Transaction> GetSumAmoutByDay(long day)
        {
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("effective_date", base.TimeStampToDateTime(day), DbType.DateTime);
            
            using(IDbConnection conn = base.GetConnection())
            {
                string query = @"SELECT '' AS Id 
                                    ,tr.effective_date AS EffectiveDate
                                    ,SUM(amount) AS Amount
                                    FROM public.transaction AS tr
                                    WHERE tr.effective_date = @effective_date
                                    GROUP BY tr.effective_date";
                try
                {
                    conn.Open();

                    var transactions = await conn.QueryAsync<Transaction>(query, parameters);
                    return transactions.FirstOrDefault();
                }
                finally
                {
                    conn?.Close();
                }
            }
        }
    }
}