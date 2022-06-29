using Gemini.Portal.Shared.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gemini.Portal.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TwinSchemaController : ControllerBase
{
    private readonly ILogger<TwinSchemaController> _logger;
    private readonly IList<TwinSchema> _store = new TwinSchema[]
    {
        new BooleanTwinSchema(),
        new DateTwinSchema(),
        new DateTimeTwinSchema(),
        new DoubleTwinSchema(),
        new DurationTwinSchema(),
        new FloatTwinSchema(),
        new IntegerTwinSchema(),
        new LongTwinSchema(),
        new StringTwinSchema(),
        new TimeTwinSchema(),
    };

    public TwinSchemaController(ILogger<TwinSchemaController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IEnumerable<TwinSchema> Get()
    {
        return _store;
    }
}