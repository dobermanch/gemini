using Gemini.Portal.Client.Components.DigitalTwin.Interface;
using Gemini.Portal.Client.Components.DigitalTwin.Property;

namespace Gemini.Portal.Client.Components.DigitalTwin.Relationship;

public class TwinRelationship
{
    public Iri Type => Iri.Relationship;

    public string Name { get; set; } = null!;

    public Dtmi? Id { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DispalyName { get; set; }

    public uint? maxMultiplicity { get; set; }

    public uint? minMultiplicity { get; set; }

    public TwinProperty[]? Properties { get; set; }

    public TwinInterface? Target { get; set; }

    public bool? Writable { get; set; }
}
