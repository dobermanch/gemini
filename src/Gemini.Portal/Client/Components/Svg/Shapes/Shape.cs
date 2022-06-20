using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;
using System.Text.Json;

namespace Gemini.Portal.Client.Components.Svg.Shapes;

public abstract class Shape : ISVGElement
{
    internal string _stateRepresentation;

    protected Shape(IElement element, Svg svg)
    {
        Element = element;
        SVG = svg;
    }

    public IElement Element { get; init; }

    public Svg SVG { get; init; }

    public abstract Type Editor { get; }

    public string Fill
    {
        get => Element.GetAttribute("fill") ?? string.Empty;
        set
        {
            Element.SetAttribute("fill", value);
            Changed.Invoke(this);
        }
    }
    public string Stroke
    {
        get => Element.GetAttribute("stroke") ?? string.Empty;
        set
        {
            Element.SetAttribute("stroke", value);
            Changed.Invoke(this);
        }
    }
    public string StrokeWidth
    {
        get => Element.GetAttribute("stroke-width") ?? string.Empty;
        set
        {
            Element.SetAttribute("stroke-width", value);
            Changed.Invoke(this);
        }
    }
    
    public Box BoundingBox { get; set; } = new();
    
    public Action<ISVGElement> Changed { get; set; }
    
    public bool Selected => SVG.VisibleSelectionShapes.Contains(this);
    
    public bool IsChildElement => Element.ParentElement?.TagName is "G" or null;

    public virtual string StateRepresentation =>
        string.Join("-", 
            Element.Attributes.Select(a => a.Value)) +
            Selected.ToString() +
            SVG.EditMode.ToString() +
            SVG.Scale + 
            SVG.Translate.x +
            SVG.Translate.y +
            JsonSerializer.Serialize(BoundingBox);

    public string StoredHtml { get; set; }

    public virtual void UpdateHtml()
    {
        StoredHtml = $"<{Element.LocalName}{string.Join("", Element.Attributes.Select(a => $" {a.Name}=\"{a.Value}\""))}> </{Element.LocalName}>";
    }

    public virtual void Rerender()
    {
        _stateRepresentation = null;
    }

    public abstract IEnumerable<(double x, double y)> SelectionPoints { get; }

    public abstract void HandleMouseMove(MouseEventArgs eventArgs);

    public abstract void HandleMouseUp(MouseEventArgs eventArgs);
    
    public abstract void HandleMouseOut(MouseEventArgs eventArgs);
    
    public abstract void Complete();
}
