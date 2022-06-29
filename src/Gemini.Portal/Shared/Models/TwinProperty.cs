namespace Gemini.Portal.Shared.Models;

public class TwinProperty: TwinModelBase
{
    public string Type { get; set; } = Iri.Property;

    public string Name { get; set; } = null!;

    public TwinSchema Schema { get; set; } = null!;

    public string Id { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public TwinUnit? Unit { get; set; }

    public bool? Writable { get; set; }
}
