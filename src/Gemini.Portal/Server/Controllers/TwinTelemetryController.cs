using Gemini.Portal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gemini.Portal.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TwinTelemetryController : ControllerBase
{
    private readonly ILogger<TwinTelemetryController> _logger;

    private readonly static List<TwinTelemetry> _store = new();

    public TwinTelemetryController(ILogger<TwinTelemetryController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<TwinTelemetry>> Get(string id)
    {
        var telemetry = _store.FirstOrDefault(it => it.Id == id);
        if (telemetry == null)
        {
            return NotFound();
        }

        return Ok(telemetry);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TwinTelemetry>>> Get()
    {
        return Ok(_store);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TwinTelemetry telemetry)
    {
        _store.Add(telemetry);
        return Created($"/id={telemetry.Id}", telemetry);
    }

    [HttpPut]
    public async Task<IActionResult> Put(IList<TwinTelemetry> telemetry)
    {
        foreach (var model in telemetry)
        {
            var existed = _store.FirstOrDefault(it => it.Id == model.Id);
            if (existed != null)
            {
                _store.Remove(existed);
                _store.Add(model);
            }
        }
        
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(IList<TwinTelemetry> telemetry)
    {
        foreach (var item in telemetry)
        {
            _store.Remove(item);
        }

        return Ok();
    }
}