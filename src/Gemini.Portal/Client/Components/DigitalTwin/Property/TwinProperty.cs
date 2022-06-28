using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Property;

public class TwinProperty
{
    public Iri Type => Iri.Property;

    public string Name { get; set; } = null!;

    public TwinSchema Schema { get; set; } = null!;

    public Dtmi? Id { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public TwinUnit? Unit { get; set; }

    public bool? Writable { get; set; }
}
