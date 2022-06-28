using Gemini.Portal.Client.Components.DigitalTwin.Property;

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
    private readonly List<TwinProperty> _store = new List<TwinProperty>();

    public Task CreateAsync(IList<TwinProperty> properties)
    {
        _store.AddRange(properties);
        return Task.CompletedTask;
    }

    public ValueTask<IList<TwinProperty>> GetAllAsync()
    {
        return ValueTask.FromResult((IList<TwinProperty>)_store);
    }

    public Task UpdateAsync(IList<TwinProperty> properties)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IList<TwinProperty> properties)
    {
        foreach (var item in properties)
        {
            _store.Remove(item);
        }

        return Task.CompletedTask;
    }
}