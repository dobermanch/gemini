using Gemini.Portal.Client.Components.DigitalTwin.Interface;
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
    private readonly List<TwinInterface> _store = new List<TwinInterface>();

    public Task CreateAsync(IList<TwinInterface> interfaces)
    {
        _store.AddRange(interfaces);
        return Task.CompletedTask;
    }

    public ValueTask<IList<TwinInterface>> GetAllAsync()
    {
        return ValueTask.FromResult((IList<TwinInterface>)_store);
    }

    public Task UpdateAsync(IList<TwinInterface> interfaces)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IList<TwinInterface> interfaces)
    {
        foreach (var item in interfaces)
        {
            _store.Remove(item);
        }

        return Task.CompletedTask;
    }
}