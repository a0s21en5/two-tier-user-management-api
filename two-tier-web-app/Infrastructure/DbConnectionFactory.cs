using System.Data;

namespace two_tier_web_app.Infrastructure;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        return new Microsoft.Data.SqlClient.SqlConnection(_connectionString);
    }
}
