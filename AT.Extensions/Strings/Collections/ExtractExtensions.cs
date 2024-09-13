namespace AT.Extensions.Strings.Collections;
public static class ExtractExtensions : Object
{
    public static IEnumerable<Int32> ExtractInts(this String? value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value == default)
            return Enumerable.Empty<Int32>();
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Matches(value, @"-?( )?\d+").Select(i => Int32.Parse(i.Value.Replace(" ", "")));
    }
}