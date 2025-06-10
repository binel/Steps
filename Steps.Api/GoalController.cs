using Microsoft.AspNetCore.Mvc;

namespace Steps.Api;

/// <summary>
/// Controller to add, view, and delete steps entries 
/// </summary>
[ApiController]
[Route("[controller]")]
public class GoalController : ControllerBase {

    /// <summary>
    /// Returns configured weekly goal
    /// </summary>
    /// <returns>The current active weekly goal</returns>
    [HttpGet("getWeeklyGoal")]
    public GoalEntry GetWeeklyGoal() {
        return new GoalEntry {
            Id = 1,
            GoalSteps = 20_000,
            Type = GoalType.Weekly
        };
    }

    /// <summary>
    /// Returns configured weekly goal
    /// </summary>
    /// <returns>The current active weekly goal</returns>
    [HttpGet("getMonthlyGoal")]
    public GoalEntry GetMonthlyGoal() {
        return new GoalEntry {
            Id = 1,
            GoalSteps = 15_000,
            Type = GoalType.Monthly
        };
    }

}