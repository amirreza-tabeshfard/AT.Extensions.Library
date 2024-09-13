namespace AT.Extensions.Strings.Comparison;
public static class EndsWithExtensions : Object
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

    public static Boolean EndsWithIgnoreCase2(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return input.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
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