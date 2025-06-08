namespace Steps.Api;

/// <summary>
/// Represents the number of steps taken on a given day
/// </summary>
public class StepsEntry {
    /// <summary>
    /// Database key
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The date the steps were taken 
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// The number of steps taken on the day
    /// </summary>
    public long Steps { get; set; }
}