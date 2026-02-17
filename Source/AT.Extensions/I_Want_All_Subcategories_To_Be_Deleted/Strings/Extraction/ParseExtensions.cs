using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class ParseExtensions
{
    public static String ParseStringToCsv(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return '"' + value.GetEmptyStringIfNull().Replace("\"", "\"\"") + '"';
    }
}