namespace Gemini.Portal.Shared.Models;

public class TwinTelemetry
{
    public Iri Type => Iri.Telemetry;

    public string Name { get; set; } = null!;

    public TwinSchema Schema { get; set; } = null!;

    public Dtmi? Id { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public TwinUnit? Unit { get; set; }
}
