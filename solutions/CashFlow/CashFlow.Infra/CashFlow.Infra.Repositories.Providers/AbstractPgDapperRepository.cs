using System.Data;
using Npgsql;

namespace CashFlow.Infra.Repositories.Providers
{
    public abstract class AbstracPgDapperRepository
    {
        private readonly IDbConnection _conn;

        public AbstracPgDapperRepository(string strConn)
        {
            _conn = new NpgsqlConnection(strConn);
        }

        protected IDbConnection GetConnection()
        {
            return _conn;
        }
    }
}
