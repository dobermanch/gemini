namespace Gemini.Portal.Shared.Models;


public class TwinInterface : TwinModelBase
{
    public string Id { get; set; } = null!;

    public string Type { get; set; } = Iri.Interface;

    public string Context { get; init; } = "dtmi:dtdl:context;2";

    public string? Comment { get; set; }

    public IList<TwinModelBase>? Contents { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public IList<string> Extends {get;set;} = new List<string>();
    
    public TwinSchema? Schema { get; set; }
}
