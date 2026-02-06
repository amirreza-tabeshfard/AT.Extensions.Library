namespace AT.Extensions.Strings.Collections;
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