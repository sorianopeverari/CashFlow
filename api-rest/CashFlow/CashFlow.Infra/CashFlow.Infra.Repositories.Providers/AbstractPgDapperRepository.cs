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

        protected DateTime TimeStampToDateTime(long timestamp)
        {
            DateTime epoch = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            epoch = epoch.AddSeconds((double)timestamp).ToUniversalTime();
            return epoch;
        }
    }
}
