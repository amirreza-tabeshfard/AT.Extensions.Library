namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
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