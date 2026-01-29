namespace AT.Extensions.Strings.Extraction;
public static class ParseExtensions
{
    public static String ParseStringToCsv(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return '"' + value.GetEmptyStringIfNull().Replace("\"", "\"\"") + '"';
    }
}