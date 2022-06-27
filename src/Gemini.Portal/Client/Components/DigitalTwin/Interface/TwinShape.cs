using AngleSharp.Dom;
using Gemini.Portal.Client.Components.Svg.Shapes.G;
using Gemini.Portal.Client.Components.Svg.Shapes.Rect;
using Gemini.Portal.Client.Components.Svg.Shapes.Text;

namespace Gemini.Portal.Client.Components.DigitalTwin.Interface;

public class TwinInterfaceShape : G
{
    public TwinInterfaceShape(IElement element, Svg.Svg svg) : base(element, svg)
    {
    }

    //public TResource Resource { get; }


    public static void AddNew(Svg.Svg SVG)
    {
        IElement element = SVG.Document.CreateElement("G");

        var bigRect = new Rect(SVG.Document.CreateElement("Rect"), SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "rgb(30, 120, 190)",
            StrokeWidth = "3",
            Fill = "rgb(233, 233, 233)",
            Width = 250,
            Height = 100
        };

        var smallRect = new Rect(SVG.Document.CreateElement("Rect"), SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "rgb(30, 120, 190)",
            StrokeWidth = "2",
            Fill = "rgb(189, 189, 189)",
            Width = 45,
            Height = 45,
            X = 10,
            Y = 10
        };

        var title = new Text(SVG.Document.CreateElement("Text"), SVG)
        {
            Changed = SVG.UpdateInput,
            Fill = "Black",
            X = 70,
            Y = 30
        };

        element.AppendChild(bigRect.Element);
        element.AppendChild(smallRect.Element);
        element.AppendChild(title.Element);

        G group = new(element, SVG)
        {
            Changed = SVG.UpdateInput
        };

        SVG.SelectedShapes.Clear();
        SVG.SelectedShapes.Add(group);
        SVG.AddElement(group);
    }
}
