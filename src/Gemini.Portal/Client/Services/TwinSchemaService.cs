using System.Net.Http.Json;
using Gemini.Portal.Client.Components.DigitalTwin.Schema;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Services;

public interface ITwinSchemaService
{
    ValueTask<IList<TwinSchema>> GetAllAsync();
}

public class TwinSchemaService : ITwinSchemaService
{
    private readonly HttpClient _client;

    //private readonly IList<TwinSchema> _store = new TwinSchema[]
    //{
    //    new BooleanTwinSchema(),
    //    new DateTwinSchema(),
    //    new DateTimeTwinSchema(),
    //    new DoubleTwinSchema(),
    //    new DurationTwinSchema(),
    //    new FloatTwinSchema(),
    //    new IntegerTwinSchema(),
    //    new LongTwinSchema(),
    //    new StringTwinSchema(),
    //    new TimeTwinSchema(),
    //};

    public TwinSchemaService(HttpClient client)
    {
        _client = client;
    }

    public async ValueTask<IList<TwinSchema>> GetAllAsync()
    {
        var response = await _client.GetFromJsonAsync<TwinSchema[]>("TwinSchema");
        return response;
    }
}
