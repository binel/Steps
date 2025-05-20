using Microsoft.Data.Sqlite;

namespace Steps.Api;

public class StepsRepository {

    private SqliteConnection _connection;

    public StepsRepository(Database database) {
        _connection = database.GetConnection();
    }

    public void CreateTableIfNotExists() {
        var tableCmd = _connection.CreateCommand(); 

        tableCmd.CommandText = 
        @"
        CREATE TABLE IF NOT EXISTS Steps (
            Id INTEGER PRIMARY KEY AUTOINCREMENT,
            Steps INTEGER NOT NULL,
            Date TEXT NOT NULL
        )
        ";

        tableCmd.ExecuteNonQuery();        
    }

    public void Add(StepsEntry steps) {
        var insertCmd = _connection.CreateCommand();
        insertCmd.CommandText = 
        @"INSERT INTO Steps (
            Steps,
            Date)
          VALUES (
           $steps,
           $date)";

        insertCmd.Parameters.AddWithValue("$steps", steps.Steps);
        insertCmd.Parameters.AddWithValue("$date", steps.Date.ToShortDateString());

        insertCmd.ExecuteNonQuery();
    }

    public void Delete(long id) {
        var deleteCmd = _connection.CreateCommand();
        deleteCmd.CommandText = 
        @"DELETE FROM Steps 
          WHERE Id=$stepsId
          ";

        deleteCmd.Parameters.AddWithValue("$stepsId", id);

        deleteCmd.ExecuteNonQuery(); 
    }

    public List<StepsEntry> GetAll() {
        var selectCmd = _connection.CreateCommand();
        selectCmd.CommandText = 
        @"SELECT Id, 
            Steps,
            Date
          FROM Steps
          ORDER BY Date DESC";

        using var reader = selectCmd.ExecuteReader();
        
        List<StepsEntry> entries = ReadStepsFromReader(reader);

        return entries; 
    }

    private List<StepsEntry> ReadStepsFromReader(SqliteDataReader reader) {
        List<StepsEntry> histories = new List<StepsEntry>();
        while (reader.Read()) {
            histories.Add(ReadWithoutAdvance(reader));
        }

        return histories;
    }

    private StepsEntry ReadWithoutAdvance(SqliteDataReader reader) {
        var entry = new StepsEntry {
            Id = (long)reader["Id"],
            Steps = (long)reader["Steps"],
            Date = DateOnly.Parse((string)reader["Date"])
        };
        return entry;
    }  
}