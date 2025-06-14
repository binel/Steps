using Microsoft.Data.Sqlite;

namespace Steps.Api;


public class GoalRepository {
    private readonly SqliteConnection _connection;

    public GoalRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void CreateTableIfNotExists() {
        SqliteCommand tableCmd = _connection.CreateCommand();

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Goals (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            GoalSteps INTEGER NOT NULL,
            GoalType INTEGER NOT NULL
        )
        ";

        _ = tableCmd.ExecuteNonQuery();
    }

    public void Add(GoalEntry entry) {
        SqliteCommand insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Goals (
            GoalSteps
            GoalType)
        VALUES (
            $goalSteps
            $goalType
        )
        ";

        _ = insertCmd.Parameters.AddWithValue("$goalSteps", entry.GoalSteps);
        _ = insertCmd.Parameters.AddWithValue("$goalType", entry.GoalType);

        _ = insertCmd.ExecuteNonQuery();
    }
}