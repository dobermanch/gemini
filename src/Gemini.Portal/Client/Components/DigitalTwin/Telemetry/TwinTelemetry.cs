using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Telemetry;

public class TwinTelemetry
{
    public Iri Type => Iri.Telemetry;

    public string Name { get; set; } = null!;

    public TwinSchema Schema { get; set; } = null!;

    public Dtmi? Id { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DispalyName { get; set; }

    public TwinUnit? Unit { get; set; }
}
