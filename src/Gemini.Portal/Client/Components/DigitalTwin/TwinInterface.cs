using AngleSharp.Dom;
using Gemini.Portal.Client.Components.Svg.Shapes.G;
using Gemini.Portal.Client.Components.Svg.Shapes.Rect;

namespace Gemini.Portal.Client.Components.DigitalTwin
{

    public class TwinShape: G
    {
        public TwinShape(IElement element, Svg.Svg svg) : base(element, svg)
        {
        }

        //public TResource Resource { get; }


        public static void AddNew(Svg.Svg SVG)
        {
//            < g >
//    < rect x = ""120"" y = ""380"" height = ""100"" width = ""250"" fill = ""rgb(233, 233, 233)"" stroke = ""rgb(30, 120, 190)"" stroke - width = ""3"" >
//        < text x = ""0"" y = ""15"" fill = ""red"" > I love SVG!</ text >
//    </ rect >
//    < rect x = ""130"" y = ""390"" height = ""45"" width = ""45"" fill = ""rgb(189, 189, 189)"" stroke = ""rgb(30, 120, 190)"" stroke - width = ""2"" ></ rect >
//</ g >

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

            element.AppendChild(bigRect.Element);
            element.AppendChild(smallRect.Element);

            G group = new(element, SVG)
            {
                Changed = SVG.UpdateInput
            };

//            SVG.EditMode = Svg.EditMode.None;

            SVG.SelectedShapes.Clear();
            SVG.SelectedShapes.Add(group);
            SVG.AddElement(group);

       
        }
    }

    public class TwinInterface
    {
        public Dtmi Id { get; init; } = null!;

        private Iri? _type;
        public Iri Type => _type ??= new Iri { Value = "Interface" };


        public Iri Context { get; init; } = null!;

        public string? Comment { get; set; }

        //public string Contents

        public string? Description { get; set; }

        public string? DisplayName { get; set; }

        //public Extends {get;set;}
        //public Schema {get;set;}
    }

    public class TwinTelemetry
    {
        private Iri? _type;
        public Iri Type => _type ??= new Iri { Value = "Telemetry" };

        public string Name { get; set; } = null!;

        //public Schema {get;set;}

        public Dtmi Id { get; init; } = null!;

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DispalyName { get; set; }

        public string? Unit { get; set; }        
    }

    public class TwinProperty
    {
        private Iri? _type;
        public Iri Type => _type ??= new Iri { Value = "Property" };

        public string Name { get; set; } = null!;

        //public Schema {get;set;}

        public Dtmi? Id { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DispalyName { get; set; }

        public string? Unit { get; set; }

        public bool? Writable { get; set; }
    }

    public class TwinRelationship
    {
        private Iri? _type;
        public Iri Type => _type ??= new Iri { Value = "Relationship" };

        public string Name { get; set; } = null!;

        public Dtmi? Id { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DispalyName { get; set; }

        public uint? maxMultiplicity { get; set; }

        public uint? minMultiplicity { get; set; }

        public TwinProperty[]? Properties { get; set; }

        public TwinInterface? Target { get; set; }

        public bool? Writable { get; set; }
    }


    public class Iri
    {
        public string Value { get; init; } = null!;
    }

    public class Dtmi
    {
        public string Value { get; init; } = null!;

        public string Schema { get; init; } = null!;

        public string Path { get; init; } = null!;

        public string Version { get; init; } = null!;
    }

    public enum ComponentType 
    {
        Interface, 
        Telemetry, 
        Property, 
        Command, 
        Relationship, 
        Component
    }
}
