namespace AT.Extensions.Strings.Extraction;
public static  class LastIndexOfExtensions : Object
{
    public static Int32 LastIndexOfInvariant(this String value, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.LastIndexOf(substring, StringComparison.Ordinal);
    }
}