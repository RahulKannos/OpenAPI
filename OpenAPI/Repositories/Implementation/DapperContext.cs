using OpenAPI.Modesl.Responses;
using OpenAPI.Repositories.Interface;
using System.Data.SqlClient;

namespace OpenAPI.Repositories.Implementation
{
    public class DapperContext : IDapperContext
    {
        private readonly SqlConnection _connection;
        private readonly string connectionString = $"Data Source={Directory.GetCurrentDirectory()}\\AppData\\database.db;version=3;Foregin keys=true";


        public DapperContext()
        {
            _connection = new SqlConnection(connectionString);
        }
        public DapperResponse<int> Execute(string sqlQuery)
        {
            throw new NotImplementedException();
        }

        public DapperResponse<List<T>> Query<T>(string sqlQuery, object? param = null)
        {
            throw new NotImplementedException();
        }
    }
}
