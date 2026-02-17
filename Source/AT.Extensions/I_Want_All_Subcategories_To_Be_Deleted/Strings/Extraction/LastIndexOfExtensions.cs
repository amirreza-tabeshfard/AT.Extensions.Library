namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static  class LastIndexOfExtensions
{
    public static Int32 LastIndexOfInvariant(this String value, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.LastIndexOf(substring, StringComparison.Ordinal);
    }
}