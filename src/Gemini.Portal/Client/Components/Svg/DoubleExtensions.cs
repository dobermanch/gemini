using System.Globalization;

namespace Gemini.Portal.Client.Components.Svg;

internal static class DoubleExtensions
{
    internal static string AsString(this double d)
    {
        return Math.Round(d, 9).ToString(CultureInfo.InvariantCulture);
    }
}
