using Microsoft.Data.Sqlite;

namespace Steps.Api;

/// <summary>
/// Repository for storing goal information in the database 
/// </summary>
public class GoalRepository {
    private readonly SqliteConnection _connection;

    /// <summary>
    /// Primary constructor 
    /// </summary>
    /// <param name="database"></param>
    public GoalRepository(Database database) {
        _connection = database.GetConnection();
    }

    /// <summary>
    /// Creates the sqlite table for goals if it doesn't already exist 
    /// </summary>
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

    /// <summary>
    /// Add a goal entry to the database
    /// </summary>
    /// <param name="entry"></param>
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
        _ = insertCmd.Parameters.AddWithValue("$goalType", entry.Type);

        _ = insertCmd.ExecuteNonQuery();
    }
}