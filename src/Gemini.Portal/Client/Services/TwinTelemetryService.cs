using Gemini.Portal.Client.Components.DigitalTwin.Telemetry;

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
    private readonly List<TwinTelemetry> _store = new List<TwinTelemetry>();

    public Task CreateAsync(IList<TwinTelemetry> telemetry)
    {
        _store.AddRange(telemetry);
        return Task.CompletedTask;
    }

    public ValueTask<IList<TwinTelemetry>> GetAllAsync()
    {
        return ValueTask.FromResult((IList<TwinTelemetry>)_store);
    }

    public Task UpdateAsync(IList<TwinTelemetry> telemetry)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(IList<TwinTelemetry> telemetry)
    {
        foreach (var item in telemetry)
        {
            _store.Remove(item);
        }

        return Task.CompletedTask;
    }
}