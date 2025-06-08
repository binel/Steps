using Microsoft.Data.Sqlite;

namespace Steps.Api;

/// <summary>
/// General Database access 
/// </summary>
public class Database {
    private readonly string _connectionString = "DataSource=:memory:";

    private SqliteConnection? _connection;

    /// <summary>
    /// Use an in-memory database 
    /// </summary>
    public Database() {

    }

    /// <summary>
    /// Use a sqlite database at the provided path
    /// </summary>
    /// <param name="connectionOverride">Path to sqlite database, formatted as "Data Source=path/to/db"</param>
    public Database(string connectionOverride) {
        _connectionString = connectionOverride;
    }

    /// <summary>
    /// Get a connection to the database 
    /// </summary>
    /// <returns>An open SqliteConnection</returns>
    public SqliteConnection GetConnection() {
        _connection ??= new SqliteConnection(_connectionString);

        if (_connection.State == System.Data.ConnectionState.Closed) {
            _connection.Open();
        }

        return _connection;
    }
}