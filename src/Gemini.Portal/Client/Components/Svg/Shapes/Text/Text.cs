using AngleSharp.Dom;
using Microsoft.AspNetCore.Components.Web;

namespace Gemini.Portal.Client.Components.Svg.Shapes.Text
{
    public class Text: Shape
    {
        public Text(IElement element, Svg svg) : base(element, svg) { }

        public override Type Editor => typeof(TextEditor);

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

        public string Content { get; set; } = "Text";

        public override List<(double x, double y)> SelectionPoints => new() { (X, Y) };

        public override void HandleMouseMove(MouseEventArgs eventArgs)
        {
            (double x, double y) = SVG.LocalDetransform((eventArgs.OffsetX, eventArgs.OffsetY));
            switch (SVG.EditMode)
            {
                case EditMode.Add:
                    (X, Y) = (x, y);
                    break;
                case EditMode.Move:
                    (double x, double y) diff = (x: x - SVG.MovePanner.x, y: y - SVG.MovePanner.y);
                    X += diff.x;
                    Y += diff.y;                    
                    break;
                case EditMode.MoveAnchor:
                    if (SVG.CurrentAnchor == null)
                    {
                        SVG.CurrentAnchor = 0;
                    }
                    switch (SVG.CurrentAnchor)
                    {
                        case 0:
                            (X, Y) = (x, y);
                            break;
                        case 1:
                            (X, Y) = (x, y);
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
            IElement element = SVG.Document.CreateElement("TEXT");

            Text line = new(element, SVG)
            {
                Changed = SVG.UpdateInput,
                Fill = "Black"
            };
            SVG.EditMode = EditMode.Add;

            (double x, double y) start = SVG.LocalDetransform((SVG.LastRightClick.x, SVG.LastRightClick.y));
            (line.X, line.Y) = start;

            SVG.SelectedShapes.Clear();
            SVG.SelectedShapes.Add(line);
            SVG.AddElement(line);
        }

        public override void Complete()
        {
        }
    }
}
