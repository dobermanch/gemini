using Gemini.Portal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gemini.Portal.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TwinInterfaceController : ControllerBase
{
    private readonly ILogger<TwinInterfaceController> _logger;

    private readonly static List<TwinInterface> _store = new();

    public TwinInterfaceController(ILogger<TwinInterfaceController> logger)
    {
        _logger = logger;
    }

    [HttpGet("/{id}")]
    public async Task<ActionResult<TwinInterface>> Get(string id)
    {
        var models = _store.FirstOrDefault(it => it.Id == id);
        if (models == null)
        {
            return NotFound();
        }

        return Ok(models);
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<TwinInterface>>> Get()
    {
        return Ok(_store);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TwinInterface model)
    {
        _store.Add(model);
        return Created($"/id={model.Id}", model);
    }

    [HttpPut]
    public async Task<IActionResult> Put(IList<TwinInterface> models)
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
    public async Task<IActionResult> Delete(IList<TwinInterface> models)
    {
        foreach (var item in models)
        {
            _store.Remove(item);
        }

        return Ok();
    }
}