﻿namespace Gemini.Portal.Shared.Models;

public class TwinSchema
{
    public virtual string Type { get; set; }

    public string? Comment { get; set; }

    public string? Description { get; set; }

    public string? DisplayName { get; set; }
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

public abstract class TwinComplexSchema : TwinSchema
{
    public Dtmi? Id { get; set; }
}

public class ArrayTwinSchema : TwinComplexSchema
{
    public override string Type => "Array";

    public TwinSchema ElementSchema { get; } = null!;
}

public class EnumTwinSchema : TwinComplexSchema
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

public class ObjectTwinSchema : TwinComplexSchema
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
