using System.Net.Http.Json;
using Gemini.Portal.Shared.Models;

namespace Gemini.Portal.Client.Services;

public interface ITwinInterfaceService
{
    ValueTask<IList<TwinInterface>> GetAllAsync();

    Task CreateAsync(IList<TwinInterface> interfaces);

    Task UpdateAsync(IList<TwinInterface> interfaces);

    Task DeleteAsync(IList<TwinInterface> interfaces);
}

public class TwinInterfaceService : ITwinInterfaceService
{
    private readonly HttpClient _client;

    public TwinInterfaceService(HttpClient client)
    {
        _client = client;
    }

    public async Task CreateAsync(IList<TwinInterface> interfaces)
    {
        foreach (var model in interfaces)
        {
            await _client.PostAsJsonAsync("TwinInterface", model);
        }
    }

    public async ValueTask<IList<TwinInterface>> GetAllAsync()
    {
        var models = await _client.GetFromJsonAsync<TwinInterface[]>("TwinInterface");

        return (IList<TwinInterface>)models;
    }

    public async Task UpdateAsync(IList<TwinInterface> interfaces)
    {
        await _client.PutAsJsonAsync("TwinInterface", interfaces);
    }

    public Task DeleteAsync(IList<TwinInterface> interfaces)
    {
        // _client.DeleteAsync();

        return Task.CompletedTask;
    }
}