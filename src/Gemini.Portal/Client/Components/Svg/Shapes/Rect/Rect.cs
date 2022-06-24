using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;

namespace Gemini.Portal.Client.Components.Svg.Shapes.Rect;

public class Rect : Shape
{
    public Rect(IElement element, Svg svg) : base(element, svg) { }

    public override Type Editor => typeof(RectEditor);

    public double X
    {
        get => Element.GetAttributeOrZero("x");
        set { Element.SetAttribute("x", value.AsString()); Changed.Invoke(this); }
    }
    public double Y
    {
        get => Element.GetAttributeOrZero("y");
        set { Element.SetAttribute("y", value.AsString()); Changed.Invoke(this); }
    }
    public double Width
    {
        get => Element.GetAttributeOrZero("width");
        set { Element.SetAttribute("width", value.AsString()); Changed.Invoke(this); }
    }
    public double Height
    {
        get => Element.GetAttributeOrZero("height");
        set { Element.SetAttribute("height", value.AsString()); Changed.Invoke(this); }
    }

    private (double x, double y)? AddPos { get; set; }

    public override List<(double x, double y)> SelectionPoints => new() { (X, Y), (X + Width, Y), (X + Width, Y + Height), (X, Y + Height) };

    public override void HandleMouseMove(MouseEventArgs eventArgs)
    {
        (double x, double y) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));
        switch (SVG.EditMode)
        {
            case EditMode.Add:
                AddResize((x, y));
                break;
            case EditMode.Move:
                Move((x, y));
                break;
            case EditMode.MoveAnchor:
                Resize((x, y));
                break;
        }
    }

    protected virtual void AddResize((double x, double y) position)
    {
        if (AddPos is null)
        {
            AddPos = (X, Y);
        }
        if (position.x < AddPos.Value.x)
        {
            X = position.x;
            Width = AddPos.Value.x - position.x;
        }
        else
        {
            X = AddPos.Value.x;
            Width = position.x - AddPos.Value.x;
        }
        if (position.y < AddPos.Value.y)
        {
            Y = position.y;
            Height = AddPos.Value.y - position.y;
        }
        else
        {
            Y = AddPos.Value.y;
            Height = position.y - AddPos.Value.y;
        }
    }

    protected virtual void Move((double x, double y) position)
    {
        (double x, double y) diff = (x: position.x - SVG.MovePanner.x, y: position.y - SVG.MovePanner.y);
        X += diff.x;
        Y += diff.y;
    }

    protected virtual void Resize((double x, double y) position)
    {
        if (SVG.CurrentAnchor == null)
        {
            SVG.CurrentAnchor = 0;
        }
        switch (SVG.CurrentAnchor)
        {
            case 0:
                Width -= position.x - X;
                Height -= position.y - Y;
                X = position.x;
                Y = position.y;
                break;
            case 1:
                Width = position.x - X;
                Height -= position.y - Y;
                Y = position.y;
                break;
            case 2:
                Width = position.x - X;
                Height = position.y - Y;
                break;
            case 3:
                Width -= position.x - X;
                Height = position.y - Y;
                X = position.x;
                break;
        }
    }

    public override void HandleMouseUp(MouseEventArgs eventArgs)
    {
        switch (SVG.EditMode)
        {
            case EditMode.Move or EditMode.MoveAnchor or EditMode.Add:
                SVG.EditMode = EditMode.None;
                break;
        }
    }

    public override void HandleMouseOut(MouseEventArgs eventArgs)
    {
    }

    public static void AddNew(Svg SVG)
    {
        IElement element = SVG.Document.CreateElement("RECT");

        Rect rect = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "black",
            StrokeWidth = "1",
            Fill = "lightgrey",
            Width = 100,
            Height = 20
        };
        SVG.EditMode = EditMode.Add;

        (rect.X, rect.Y) = SVG.LocalDetransform((SVG.LastRightClick.x, SVG.LastRightClick.y));

        SVG.SelectedShapes.Clear();
        SVG.SelectedShapes.Add(rect);
        SVG.AddElement(rect);
    }

    public override void Complete()
    {
    }
}
