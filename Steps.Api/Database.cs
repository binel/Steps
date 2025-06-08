using Microsoft.Data.Sqlite;

namespace Steps.Api;

public class Database {
    private readonly string _connectionString = "DataSource=:memory:";

    private SqliteConnection? _connection;

    public Database() {

    }

    public Database(string connectionOverride) {
        _connectionString = connectionOverride;
    }

    public SqliteConnection GetConnection() {
        _connection ??= new SqliteConnection(_connectionString);

        if (_connection.State == System.Data.ConnectionState.Closed) {
            _connection.Open();
        }

        return _connection;
    }

}