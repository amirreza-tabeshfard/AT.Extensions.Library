namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class ReadLinesExtensions
{
    public static IEnumerable<String> ReadLines(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        StringReader reader = new(value);
        String? line;
        // ----------------------------------------------------------------------------------------------------
        while ((line = reader.ReadLine()) != null)
            yield return line;
    }
}