namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class DoesNotExtensions
{
    public static Boolean DoesNotEndWith(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        return value == default || suffix == default ||
               !value.EndsWith(suffix, StringComparison.InvariantCulture);
    }

    public static Boolean DoesNotStartWith(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        return value == default || prefix == default ||
               !value.StartsWith(prefix, StringComparison.InvariantCulture);
    }
}