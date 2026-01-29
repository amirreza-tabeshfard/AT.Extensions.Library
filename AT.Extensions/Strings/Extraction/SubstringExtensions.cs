namespace AT.Extensions.Strings.Extraction;
public static class SubstringExtensions
{
    public static String SubstringFromFirstOccurrence(this String value, String afterThis)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 index = value.IndexOfInvariant(afterThis);
        String result;
        // ----------------------------------------------------------------------------------------------------
        if (index == -1)
            result = value;
        else
            result = value.Substring(index + afterThis.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String SubstringFromLastOccurrence(this String value, String afterMe)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(afterMe);
        // ----------------------------------------------------------------------------------------------------
        Int32 index = value.LastIndexOfInvariant(afterMe);
        String result;
        // ----------------------------------------------------------------------------------------------------
        if (index == -1)
            result = value;
        else
            result = value.Substring(index + afterMe.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String? SubstringToFirstOccurrence(this String value, String toThis)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(toThis);
        // ----------------------------------------------------------------------------------------------------
        if (value == default)
            return default;
        // ----------------------------------------------------------------------------------------------------
        Int32 index = value.IndexOfInvariant(toThis);
        // ----------------------------------------------------------------------------------------------------
        if (index == -1)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(0, index);
    }
}