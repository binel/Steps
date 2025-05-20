using Microsoft.AspNetCore.Mvc;

namespace Steps.Api;

[ApiController]
[Route("[controller]")]
public class StepsController: ControllerBase {

    private readonly StepsRepository _stepsRepository; 

    public StepsController(StepsRepository stepsRepository) {
        _stepsRepository = stepsRepository;
        Startup();
    }

    private void Startup() {
        _stepsRepository.CreateTableIfNotExists();
    }

    [HttpGet("getStepEntries")]
    public List<StepsEntry> GetStepEntries() {
        return _stepsRepository.GetAll();
    }

    [HttpPost("addEntry")]
    public void AddStepEntry(StepsEntry entry) {
        _stepsRepository.Add(entry);
    }

    [HttpDelete("deleteEntry")]
    public void DeleteEntry(int id) {
        _stepsRepository.Delete(id);
    }

}