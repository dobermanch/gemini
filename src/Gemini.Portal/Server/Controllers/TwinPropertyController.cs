using Gemini.Portal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gemini.Portal.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TwinPropertyController : ControllerBase
{
    private readonly ILogger<TwinPropertyController> _logger;

    private readonly static List<TwinProperty> _store = new();

    public TwinPropertyController(ILogger<TwinPropertyController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<TwinProperty>> Get(string id)
    {
        var models = _store.FirstOrDefault(it => it.Id == id);
        if (models == null)
        {
            return NotFound();
        }

        return Ok(models);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TwinProperty>>> Get()
    {
        return Ok(_store);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TwinProperty model)
    {
        _store.Add(model);
        return Created($"/id={model.Id}", model);
    }

    [HttpPut]
    public async Task<IActionResult> Put(IList<TwinProperty> models)
    {
        foreach (var model in models)
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
    public async Task<IActionResult> Delete(IList<TwinProperty> models)
    {
        foreach (var item in models)
        {
            _store.Remove(item);
        }

        return Ok();
    }
}