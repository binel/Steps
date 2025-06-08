using System.Globalization;
using Microsoft.Data.Sqlite;

namespace Steps.Api;

public class StepsRepository {

    private readonly SqliteConnection _connection;

    public StepsRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void CreateTableIfNotExists() {
        SqliteCommand tableCmd = _connection.CreateCommand();

        tableCmd.CommandText =
        @"
        CREATE TABLE IF NOT EXISTS Steps (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Steps INTEGER NOT NULL,
            Date TEXT NOT NULL
        )
        ";

        _ = tableCmd.ExecuteNonQuery();
    }

    public void Add(StepsEntry steps) {
        SqliteCommand insertCmd = _connection.CreateCommand();
        insertCmd.CommandText =
        @"INSERT INTO Steps (
            Steps,
            Date)
          VALUES (
           $steps,
           $date)";

        _ = insertCmd.Parameters.AddWithValue("$steps", steps.Steps);
        _ = insertCmd.Parameters.AddWithValue("$date", steps.Date.ToShortDateString());

        _ = insertCmd.ExecuteNonQuery();
    }

    public void Delete(long id) {
        SqliteCommand deleteCmd = _connection.CreateCommand();
        deleteCmd.CommandText =
        @"DELETE FROM Steps 
          WHERE Id=$stepsId
          ";

        _ = deleteCmd.Parameters.AddWithValue("$stepsId", id);

        _ = deleteCmd.ExecuteNonQuery();
    }

    public List<StepsEntry> GetAll() {
        SqliteCommand selectCmd = _connection.CreateCommand();
        selectCmd.CommandText =
        @"SELECT Id, 
            Steps,
            Date
          FROM Steps
          ORDER BY Date DESC";

        using SqliteDataReader reader = selectCmd.ExecuteReader();

        List<StepsEntry> entries = ReadStepsFromReader(reader);

        return entries;
    }

    private static List<StepsEntry> ReadStepsFromReader(SqliteDataReader reader) {
        List<StepsEntry> histories = [];
        while (reader.Read()) {
            histories.Add(ReadWithoutAdvance(reader));
        }

        return histories;
    }

    private static StepsEntry ReadWithoutAdvance(SqliteDataReader reader) {
        StepsEntry entry = new() {
            Id = (long)reader["Id"],
            Steps = (long)reader["Steps"],
            Date = DateOnly.Parse((string)reader["Date"], CultureInfo.InvariantCulture)
        };
        return entry;
    }
}