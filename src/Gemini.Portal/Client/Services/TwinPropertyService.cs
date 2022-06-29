using System.Net.Http.Json;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Services;

public interface ITwinPropertyService
{
    ValueTask<IList<TwinProperty>> GetAllAsync();

    Task CreateAsync(IList<TwinProperty> properties);

    Task UpdateAsync(IList<TwinProperty> properties);

    Task DeleteAsync(IList<TwinProperty> properties);
}

public class TwinPropertyService : ITwinPropertyService
{
    private readonly HttpClient _client;

    public TwinPropertyService(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateAsync(IList<TwinProperty> properties)
    {
        foreach (var model in properties)
        {
            await _client.PostAsJsonAsync("TwinProperty", model);
        }
    }

    public async ValueTask<IList<TwinProperty>> GetAllAsync()
    {
        var models = await _client.GetFromJsonAsync<TwinProperty[]>("TwinProperty");

        return (IList<TwinProperty>)models;
    }

    public async Task UpdateAsync(IList<TwinProperty> properties)
    {
        await _client.PutAsJsonAsync("TwinProperty", properties);
    }

    public Task DeleteAsync(IList<TwinProperty> properties)
    {
        // _client.DeleteAsync();

        return Task.CompletedTask;
    }
}