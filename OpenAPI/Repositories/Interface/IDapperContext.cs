using OpenAPI.Modesl.Responses;
using System.Data;

namespace OpenAPI.Repositories.Interface
{
    public interface IDapperContext
    {
        DapperResponse<List<T>> Query<T>(string sqlQuery, object? param = null);
        DapperResponse<int> Execute(string sqlQuery);

    }
}
