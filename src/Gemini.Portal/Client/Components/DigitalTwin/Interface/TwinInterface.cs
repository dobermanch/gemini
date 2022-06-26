using AngleSharp.Dom;
using Gemini.Portal.Client.Components.Svg.Shapes.G;
using Gemini.Portal.Client.Components.Svg.Shapes.Rect;
using Gemini.Portal.Client.Components.Svg.Shapes.Text;
using System.Globalization;

namespace Gemini.Portal.Client.Components.DigitalTwin.Interface
{

    public class TwinShape : G
    {
        public TwinShape(IElement element, Svg.Svg svg) : base(element, svg)
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

    public abstract class TwinModelBase 
    {
        public abstract Iri Type { get; }

    }


    public class TwinInterface : TwinModelBase
    {
        public Dtmi Id { get; init; } = null!;

        public override Iri Type => Iri.Interface;

        public Iri Context { get; init; } = "dtmi:dtdl:context;2";

        public string? Comment { get; set; }

        public IList<TwinModelBase> Contents { get; init; } = new List<TwinModelBase>();

        public string? Description { get; set; }

        public string? DisplayName { get; set; }

        public IList<Dtmi> Extends {get;set;} = new List<Dtmi>();
        
        public TwinSchema? Schema { get; set; }
    }

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

    public record TwinUnit();

    public class TwinProperty
    {
        public Iri Type => Iri.Property;

        public string Name { get; set; } = null!;

        public TwinSchema Schema { get; set; } = null!;

        public Dtmi? Id { get; set; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DispalyName { get; set; }

        public TwinUnit? Unit { get; set; }

        public bool? Writable { get; set; }
    }

    public class TwinRelationship
    {
        public Iri Type => Iri.Relationship;

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


    public class Iri : Dictionary<string, string>
    {
        //TODO: Implement it properlly according to speck

        public static readonly Iri Interface = "Interface";

        public static readonly Iri Property = "Property";

        public static readonly Iri Telemetry = "Teementry";

        public static readonly Iri Component = "Component";

        public static readonly Iri Relationship = "Relationship";

        public string Value { get; init; } = null!;

        public string GetValue()
        {
            TryGetValue(CultureInfo.CurrentCulture.TwoLetterISOLanguageName, out var value);
            return value ?? string.Empty;
        }

        public static implicit operator string(Iri iri) => iri.Value;

        public static implicit operator Iri(string iri) => new Iri { Value = iri };
    }

    public record Dtmi
    {
        private const string SchemaDelimeter = ":";
        private const string SegmentDelimeter = ":";
        private const string VersionDelimeter = ";";

        public Dtmi(string path, string version = "1")
            : this("dtmi", path, version) { }

        public Dtmi(string schema, string path, string version)
        {
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            Path = path ?? throw new ArgumentNullException(nameof(path));
            Version = version ?? throw new ArgumentNullException(nameof(version));

            Segments = path.Split(SegmentDelimeter, StringSplitOptions.RemoveEmptyEntries);
        }
        
        public string Schema { get; init; } = null!;

        public string Path { get; init; } = null!;

        public string Version { get; init; } = null!;

        public string[] Segments { get; init; } = null!;

        public override string ToString()
        {
            return $"{Schema}{SchemaDelimeter}{Path}{VersionDelimeter}{Version}";
        }

        public static implicit operator string(Dtmi dtmi) => dtmi.ToString();

        public static explicit operator Dtmi(string dtmi) => Parse(dtmi);

        public static Dtmi Parse(string dtmi)
        {
            if (dtmi is null)
            {
                throw new ArgumentNullException();
            }

            int schemaIndex = dtmi.IndexOf(SchemaDelimeter);
            int pathIndex = dtmi.IndexOf(VersionDelimeter);
            if (dtmi.Length > 128 || schemaIndex < 0 || pathIndex < 0 || schemaIndex > pathIndex || pathIndex == dtmi.Length)
            {
                throw new ArgumentException("Wrong format");
            }

            //TODO: Validate path and version total length, according to the guideline
            return new Dtmi(
                schema: dtmi.Substring(0, schemaIndex),
                path: dtmi.Substring(schemaIndex + 1, pathIndex - schemaIndex - 1),
                version: dtmi.Substring(pathIndex + 1, dtmi.Length - pathIndex - 1)
            );
        }
    }

    public class InterfaceSchema
    {
        public Dtmi Id { get; set; } = null!;

        public ComplexTwinSchema Type { get; set; } = null!;

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DisplayName { get; set; }
    }

    public abstract class TwinSchema
    {
        public abstract string Type { get; }
    }

    public class BooleanTwinSchema : TwinSchema
    {
        public override string Type => "boolean";
    }

    public class DateTwinSchema : TwinSchema
    {
        public override string Type => "date";
    }

    public class DateTimeTwinSchema : TwinSchema
    {
        public override string Type => "dateTime";
    }

    public class DoubleTwinSchema : TwinSchema
    {
        public override string Type => "double";
    }

    public class DurationTwinSchema : TwinSchema
    {
        public override string Type => "duration";
    }

    public class FloatTwinSchema : TwinSchema
    {
        public override string Type => "float";
    }

    public class IntegerTwinSchema : TwinSchema
    {
        public override string Type => "integer";
    }

    public class LongTwinSchema : TwinSchema
    {
        public override string Type => "long";
    }

    public class StringTwinSchema : TwinSchema
    {
        public override string Type => "string";
    }

    public class TimeTwinSchema : TwinSchema
    {
        public override string Type => "time";
    }

    public abstract class ComplexTwinSchema : TwinSchema
    {
        public Dtmi? Id { get; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DisplayName { get; set; }
    }

    public class ArrayTwinSchema : ComplexTwinSchema
    {
        public override string Type => "Array";

        public TwinSchema ElementSchema { get; } = null!;
    }

    public class EnumTwinSchema : ComplexTwinSchema
    {
        public override string Type => "Enum";

        public TwinSchema EnumSchema { get; } = null!;        
    }

    //public class MapTwinSchema : ComplexTwinSchema
    //{
    //    public override string Type => "Map";

    //    public MapKey MapKey { get; } = null!;

    //    public MapValue MapValue { get; } = null!;
    //}

    public class ObjectTwinSchema : ComplexTwinSchema
    {
        public override string Type => "Object";

        public IList<TwinObjectField> Fields { get; } = new List<TwinObjectField>();
    }

    public class TwinObjectField
    {
        public string Name { get; } = null!;

        public TwinSchema Schema { get; } = null!;

        public Dtmi? Id { get; }

        public string? Comment { get; set; }

        public string? Description { get; set; }

        public string? DisplayName { get; set; }
    }
}
