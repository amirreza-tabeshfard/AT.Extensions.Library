using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Conversion;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class FirstExtensions
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