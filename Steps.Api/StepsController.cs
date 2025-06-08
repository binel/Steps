using Microsoft.AspNetCore.Mvc;

namespace Steps.Api;

/// <summary>
/// Controller to add, view, and delete steps entries 
/// </summary>
[ApiController]
[Route("[controller]")]
public class StepsController : ControllerBase {

    private readonly StepsRepository _stepsRepository;

    /// <summary>
    /// Creates the controller
    /// SIDE EFFECT - will create the steps table if it does not exist
    /// </summary>
    /// <param name="stepsRepository"></param>
    public StepsController(StepsRepository stepsRepository) {
        _stepsRepository = stepsRepository;
        Startup();
    }

    private void Startup() {
        _stepsRepository.CreateTableIfNotExists();
    }

    /// <summary>
    /// Returns all step entries in the database 
    /// </summary>
    /// <returns>A list of all step entries</returns>
    [HttpGet("getStepEntries")]
    public List<StepsEntry> GetStepEntries() {
        return _stepsRepository.GetAll();
    }

    /// <summary>
    /// Adds a new step entry into the database
    /// </summary>
    /// <param name="entry">The entry to add</param>
    [HttpPost("addEntry")]
    public void AddStepEntry(StepsEntry entry) {
        _stepsRepository.Add(entry);
    }

    /// <summary>
    /// Deletes a step entry from the database
    /// </summary>
    /// <param name="id">database key of the steps entry to delete</param>
    [HttpDelete("deleteEntry")]
    public void DeleteEntry(int id) {
        _stepsRepository.Delete(id);
    }

}