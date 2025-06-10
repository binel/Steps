namespace Steps.Api;

/// <summary>
/// A GoalEntry is a user-defined goal for the average number of steps they 
/// want to accomplish over a given time period
/// </summary>
public class GoalEntry {
    /// <summary>
    /// Database key of the goal entry 
    /// </summary>
    public long Id { get; set; }

    /// <summary>
    /// The average number of steps goal for the time period 
    /// </summary>
    public long GoalSteps { get; set; }

    /// <summary>
    /// The type of goal - how long this goal covers 
    /// </summary>
    public GoalType Type { get; set; }
}

/// <summary>
/// Goal type models the length of time a goal covers
/// </summary>
public enum GoalType {
    /// <summary>
    /// Average number of steps over a 7 day period 
    /// </summary>
    Weekly,

    /// <summary>
    /// Average number of steps over a 30 day period
    /// </summary>
    Monthly
}