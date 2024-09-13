using AT.Extensions.Chars.Conversion;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class FirstExtensions : Object
{
    public static String? FirstCharacter(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return (!value.IsNullOrEmpty() || !value.IsNullOrWhiteSpace())
               ? (value.Length >= 1)
               ? value.Substring(0, 1)
               : value
               : default;
    }

    public static String FirstLetterLower(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.FirstOrDefault().ToLower() + value.Substring(1);
    }

    public static String FirstLetterUpper(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.FirstOrDefault().ToUpper() + value.Substring(1);
    }

    public static String FirstWordUpper(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.FirstOrDefault().ToUpper() + value.Substring(1).ToLower();
    }
}