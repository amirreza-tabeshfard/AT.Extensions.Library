namespace AT.Extensions.Strings.Comparison;
public static class EndsWithExtensions
{
    public static Boolean EndsWithIgnoreCase(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length < suffix.Length)
            return false;
        // ----------------------------------------------------------------------------------------------------
        return value.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean EndsWithInvariant(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        Boolean result = false;
        // ----------------------------------------------------------------------------------------------------
        if (value != null && suffix != null)
            result = value.EndsWith(suffix, StringComparison.Ordinal);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean EndsWithInvariantIgnoreCase(this String value, String suffix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(suffix);
        // ----------------------------------------------------------------------------------------------------
        Boolean result = false;
        // ----------------------------------------------------------------------------------------------------
        if (value != null && suffix != null)
            result = value.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}