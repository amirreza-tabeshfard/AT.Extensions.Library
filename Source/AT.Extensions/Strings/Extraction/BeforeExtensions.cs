namespace AT.Extensions.Strings.Extraction;
public static class BeforeExtensions
{
    public static String Before(this String value, String search, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(search);
        // ----------------------------------------------------------------------------------------------------
        StringComparison culture = ignoreCase
                                   ? StringComparison.InvariantCultureIgnoreCase
                                   : StringComparison.InvariantCulture;
        // ----------------------------------------------------------------------------------------------------
        Int32 idx = value?.IndexOf(search, culture) ?? -1;
        if (idx >= 0)
            value = value.Substring(0, idx);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String BeforeIgnoreCase(this String value, String search)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(search);
        // ----------------------------------------------------------------------------------------------------
        return value.Before(search, true);
    }
}