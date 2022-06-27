﻿using Gemini.Portal.Client.Components.DigitalTwin.Schema;

namespace Gemini.Portal.Client.Components.DigitalTwin.Interface;

public class TwinInterfaceSchema
{
    public Dtmi Id { get; set; } = null!;

    public ComplexTwinSchema Type { get; set; } = null!;

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }
}    