using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class StringWithExtensions
{
    public static String StringWithPrefix(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        if (value.StartsWithInvariant(prefix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return prefix + value;
    }

    public static String StringWithSuffix(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        if (value.EndsWithInvariant(suffix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value + suffix;
    }
}