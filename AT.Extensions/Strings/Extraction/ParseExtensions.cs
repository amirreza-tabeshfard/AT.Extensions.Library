namespace AT.Extensions.Strings.Extraction;
public static class ParseExtensions : Object
{
    public static String ParseStringToCsv(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return '"' + value.GetEmptyStringIfNull().Replace("\"", "\"\"") + '"';
    }
}