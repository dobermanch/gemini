namespace Gemini.Portal.Shared.Models;

public class TwinTelemetry: TwinModelBase
{
    public string Id { get; set; }

    public string Type { get; set; } = Iri.Telemetry;

    public string Name { get; set; } = null!;

    public TwinSchema Schema { get; set; } = null!;

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public TwinUnit? Unit { get; set; }
}
