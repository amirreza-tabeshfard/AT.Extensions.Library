using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class LastExtensions : Object
{
    public static String? Last(this String value, params Char[] split)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Split(split).LastOrDefault();
    }

    public static String? Last(this String value, params String[] split)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Split(split, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
    }

    public static String? LastCharacter(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return (!value.IsNullOrEmpty() || !value.IsNullOrWhiteSpace())
               ? (value.Length >= 1)
               ? value.Substring(value.Length - 1, 1)
               : value
               : default;
    }

    public static String? LastWord(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Last(' ');
    }
}