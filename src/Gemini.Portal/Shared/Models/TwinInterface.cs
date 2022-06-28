namespace Gemini.Portal.Shared.Models;


public class TwinInterface : TwinModelBase
{
    public Dtmi Id { get; set; } = null!;

    public override Iri Type => Iri.Interface;

    public Iri Context { get; init; } = "dtmi:dtdl:context;2";

    public string? Comment { get; set; }

    public IList<TwinModelBase>? Contents { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }

    public IList<Dtmi> Extends {get;set;} = new List<Dtmi>();
    
    public TwinSchema? Schema { get; set; }
}
