﻿using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;

namespace Gemini.Portal.Client.Components.Svg.Shapes.Line;

public class Line : Shape
{
    public Line(IElement element, Svg svg) : base(element, svg) { }

    public override Type Editor => typeof(LineEditor);

    public double X1
    {
        get => Element.GetAttributeOrZero("x1");
        set { Element.SetAttribute("x1", value.AsString()); Changed.Invoke(this); }
    }
    public double Y1
    {
        get => Element.GetAttributeOrZero("y1");
        set { Element.SetAttribute("y1", value.AsString()); Changed.Invoke(this); }
    }
    public double X2
    {
        get => Element.GetAttributeOrZero("x2");
        set { Element.SetAttribute("x2", value.AsString()); Changed.Invoke(this); }
    }
    public double Y2
    {
        get => Element.GetAttributeOrZero("y2");
        set { Element.SetAttribute("y2", value.AsString()); Changed.Invoke(this); }
    }

    public override List<(double x, double y)> SelectionPoints => new() { (X1, Y1), (X2, Y2) };

    public override void HandleMouseMove(MouseEventArgs eventArgs)
    {
        (double x, double y) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));
        switch (SVG.EditMode)
        {
            case EditMode.Add:
                (X2, Y2) = (x, y);
                break;
            case EditMode.Move:
                (double x, double y) diff = (x: x - SVG.MovePanner.x, y: y - SVG.MovePanner.y);
                X1 += diff.x;
                Y1 += diff.y;
                X2 += diff.x;
                Y2 += diff.y;
                break;
            case EditMode.MoveAnchor:
                if (SVG.CurrentAnchor == null)
                {
                    SVG.CurrentAnchor = 0;
                }
                switch (SVG.CurrentAnchor)
                {
                    case 0:
                        (X1, Y1) = (x, y);
                        break;
                    case 1:
                        (X2, Y2) = (x, y);
                        break;
                }
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
        IElement element = SVG.Document.CreateElement("LINE");

        Line line = new(element, SVG)
        {
            Changed = SVG.UpdateInput,
            Stroke = "black",
            StrokeWidth = "3"
        };
        SVG.EditMode = EditMode.Add;

        (double x, double y) start = SVG.LocalDetransform((SVG.LastRightClick.x, SVG.LastRightClick.y));
        (line.X1, line.Y1) = start;
        (line.X2, line.Y2) = start;

        SVG.SelectedShapes.Clear();
        SVG.SelectedShapes.Add(line);
        SVG.AddElement(line);
    }

    public override void Complete()
    {
    }
}