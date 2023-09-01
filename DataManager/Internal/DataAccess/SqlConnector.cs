using Dapper;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DataManager.Library.Internal.DataAccess
{
    public class SqlConnector : ISqlConnector
    {
        private readonly IConfiguration config;

        public SqlConnector(IConfiguration config)
        {
            this.config = config;
        }

        public async Task<List<T>> LoadData<T, U>(string storedProc, U parameters, string connectionStringKey)
        {
            string connectionString = config.GetConnectionString(connectionStringKey);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                IEnumerable<T> result = await conn.QueryAsync<T>(storedProc, parameters, commandType: CommandType.StoredProcedure);
                return result.ToList();
            }
        }

        public async Task<int> SaveData<T>(string storedProc, T parameters, string connectionStringKey)
        {
            string connectionString = config.GetConnectionString(connectionStringKey);

            using (IDbConnection conn = new SqlConnection(connectionString))
            {
                int result = await conn.ExecuteAsync(storedProc, parameters, commandType: CommandType.StoredProcedure);
                return result;
            }
        }
    }
}
