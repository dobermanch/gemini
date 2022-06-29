using System.Net.Http.Json;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Services;

public interface ITwinTelemetryService
{
    ValueTask<IList<TwinTelemetry>> GetAllAsync();

    Task CreateAsync(IList<TwinTelemetry> telemetry);

    Task UpdateAsync(IList<TwinTelemetry> telemetry);

    Task DeleteAsync(IList<TwinTelemetry> telemetry);
}

public class TwinTelemetryService : ITwinTelemetryService
{
    private readonly HttpClient _client;

    public TwinTelemetryService(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateAsync(IList<TwinTelemetry> telemetry)
    {
        foreach (var model in telemetry)
        {
            var response = await _client.PostAsJsonAsync("TwinTelemetry", model);
        }
    }

    public async ValueTask<IList<TwinTelemetry>> GetAllAsync()
    {
        var models = await _client.GetFromJsonAsync<TwinTelemetry[]>("TwinTelemetry");

        return (IList<TwinTelemetry>)models;
    }

    public async Task UpdateAsync(IList<TwinTelemetry> telemetry)
    {
        await _client.PutAsJsonAsync("TwinTelemetry", telemetry);
    }

    public Task DeleteAsync(IList<TwinTelemetry> telemetry)
    {
       // _client.DeleteAsync();

        return Task.CompletedTask;
    }
}