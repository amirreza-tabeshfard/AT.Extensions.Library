namespace AT.Extensions.Strings.Extraction;
public static partial class TrimExtensions : Object
{
    #region Method(s): Private

    [System.Text.RegularExpressions.GeneratedRegex(" {2,}")]
    private static partial System.Text.RegularExpressions.Regex ReduceMultipleSpaces();

    #endregion

    public static String TrimCrLf(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!String.IsNullOrWhiteSpace(value))
            value = value.Replace("\\r\\n", " ")
                        .Replace("\\r", " ")
                        .Replace("\\n", " ")
                        .Replace("\r\n", " ")
                        .Replace("\r", " ")
                        .Replace("\n", " ")
                        .Replace("\\", String.Empty)
                        .Replace("  ", " ")
                        .Trim();
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String TrimEnd(this String value, String endsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimEnd(endsWith, StringComparison.Ordinal);
    }

    public static String TrimEnd(this String value, String endsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimEnd(endsWith, comparisonType, Int32.MaxValue);
    }

    public static String TrimEnd(this String value, String endsWith, StringComparison comparisonType, Int32 max)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimEnd(endsWith, comparisonType, max, out Int32 count);
    }

    public static String TrimEnd(this String value, String endsWith, StringComparison comparisonType, Int32 max, out Int32 total)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        if (max <= 0)
            throw new ArgumentOutOfRangeException(nameof(max), "Max cannot be smaller or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        total = default;
        // ----------------------------------------------------------------------------------------------------
        if (endsWith.Length > 0)
            for (; total < max; total++)
                if (result.EndsWith(endsWith, comparisonType))
                    result = result.Remove(result.Length - endsWith.Length);
                else
                    break;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String TrimEndOnce(this String value, String endsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimEndOnce(endsWith, StringComparison.Ordinal);
    }

    public static String TrimEndOnce(this String value, String endsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimEnd(endsWith, comparisonType, 1);
    }

    public static String TrimEveryThing(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return ReduceMultipleSpaces().Replace(value, " ").Trim();
    }

    public static String TrimStart(this String value, String startsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimStart(startsWith, StringComparison.Ordinal);
    }

    public static String TrimStart(this String value, String startsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimStart(startsWith, comparisonType, Int32.MaxValue);
    }

    public static String TrimStart(this String value, String startsWith, StringComparison comparisonType, Int32 max)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimStart(startsWith, comparisonType, max, out Int32 count);
    }

    public static String TrimStart(this String value, String startsWith, StringComparison comparisonType, Int32 max, out Int32 total)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        if (max <= 0)
            throw new ArgumentOutOfRangeException(nameof(max), "Max cannot be smaller or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        total = default;
        // ----------------------------------------------------------------------------------------------------
        if (startsWith.Length > 0)
            for (; total < max; total++)
                if (result.StartsWith(startsWith, comparisonType))
                    result = result.Remove(0, startsWith.Length);
                else
                    break;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String TrimStartOnce(this String value, String startsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimStartOnce(startsWith, StringComparison.Ordinal);
    }

    public static String TrimStartOnce(this String value, String startsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return value.TrimStart(startsWith, comparisonType, 1);
    }
}