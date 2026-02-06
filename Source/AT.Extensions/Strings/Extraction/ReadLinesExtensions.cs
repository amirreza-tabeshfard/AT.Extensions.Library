namespace AT.Extensions.Strings.Extraction;
public static class ReadLinesExtensions
{
    public static void ReadLines(this String value, Action<String> callback)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        StringReader reader = new(value);
        String? line;
        // ----------------------------------------------------------------------------------------------------
        while ((line = reader.ReadLine()) != null)
            callback(line);
    }
}