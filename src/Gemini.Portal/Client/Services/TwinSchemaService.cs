using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Services;

public interface ITwinSchemaService
{
    ValueTask<IList<TwinSchema>> GetAllAsync();
}

public class TwinSchemaService : ITwinSchemaService
{
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

    public ValueTask<IList<TwinSchema>> GetAllAsync()
    {
        return ValueTask.FromResult(_store);
    }
}
