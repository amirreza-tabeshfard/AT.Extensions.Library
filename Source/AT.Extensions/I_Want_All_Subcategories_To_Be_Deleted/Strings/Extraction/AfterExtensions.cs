namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class AfterExtensions
{
    public static String? After(this String value, String search, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(search);
        // ----------------------------------------------------------------------------------------------------
        StringComparison culture = default;
        Int32 index = default;
        String? result = default;
        // ----------------------------------------------------------------------------------------------------
        culture = ignoreCase
                  ? StringComparison.InvariantCultureIgnoreCase
                  : StringComparison.InvariantCulture;
        // ----------------------------------------------------------------------------------------------------
        index = value?.IndexOf(search, culture) ?? -1;
        if (index >= 0)
            result = value?.Substring(index + search.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String? AfterIgnoreCase(this String value, String search)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(search);
        // ----------------------------------------------------------------------------------------------------
        return value.After(search, true);
    }
}