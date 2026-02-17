namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class ExtractExtensions
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