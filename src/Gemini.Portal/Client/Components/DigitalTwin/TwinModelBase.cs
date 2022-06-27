using System.Globalization;

namespace Gemini.Portal.Client.Components.DigitalTwin;

public abstract class TwinModelBase
{
    public abstract Iri Type { get; }

}


public record TwinUnit();

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

public class Dtmi : IEquatable<Dtmi>
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

    public string Schema { get; set; } = null!;

    public string Path { get; set; } = null!;

    public string Version { get; set; } = null!;

    public string[] Segments { get; set; } = null!;

    public override string ToString()
    {
        return $"{Schema}{SchemaDelimeter}{Path}{VersionDelimeter}{Version}";
    }

    public static implicit operator string(Dtmi dtmi) => dtmi.ToString();

    public static implicit operator Dtmi(string dtmi) => Parse(dtmi);

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

    public override bool Equals(object? obj)
    {
        return Equals((Dtmi?)obj);
    }

    public bool Equals(Dtmi? other)
    {
        return other != null && (ReferenceEquals(this, other) || ToString() == other.ToString());
    }
}
