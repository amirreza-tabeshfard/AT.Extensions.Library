using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
using static AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison.StartsWithExtensions;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class ContainsExtensions
{
    public static Boolean Contains(this String source, Char value)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        // ----------------------------------------------------------------------------------------------------
        return source.IndexOf(value) >= 0;
    }

    public static Boolean Contains(this String source, String text, StringComparison stringComparison)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(text);
        // ----------------------------------------------------------------------------------------------------
        return source.IndexOf(text, stringComparison) >= 0;
    }

    public static Boolean Contains(this String input, String value, Int32 startIndex, Int32 count, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");
        // ----------------------------------------------------------------------------------------------------
        Int32 firstIndex = input.IndexOf(value, startIndex, count, comparisonType);
        Boolean result = firstIndex != -1;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean Contains(this String text, IEnumerable<String> options)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        // ----------------------------------------------------------------------------------------------------
        return options.Any(opción => text.Contains(opción));
    }

    public static Boolean Contains(this IEnumerable<String> input, String value, StringComparison stringComparison)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return input.Any(item => item.Equals(value, stringComparison));
    }

    public static Boolean ContainsAny(this String value, String findChars, Boolean ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(findChars);
        // ----------------------------------------------------------------------------------------------------
        HashSet<Char> hashSet = new(findChars, CharComparer.GetEqualityComparer(ignoreCase));
        foreach (Char c in value)
            if (hashSet.Contains(c))
                return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean ContainsDigits(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Any(x => x >= '0' && x <= '9');
    }

    public static Boolean ContainsIgnoreAccents(this String source, String value, Boolean ignoreCase = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.CompareOptions compareOptions = System.Globalization.CompareOptions.IgnoreNonSpace;
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase)
            compareOptions |= System.Globalization.CompareOptions.IgnoreCase;
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, compareOptions) != -1;
    }

    public static Boolean ContainsIgnoreCase(this String source, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return source.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    public static Boolean ContainsIgnoreSpaces(this String source, String substring, Boolean ignoreCase = true, Boolean ignoreCommas = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        String withoutSpaces = source.Replace(" ", "");
        String withoutSpaces2 = substring.Replace(" ", "");
        // ----------------------------------------------------------------------------------------------------
        return withoutSpaces.ContainsInvariant(withoutSpaces2, ignoreCase, ignoreCommas);
    }

    public static Boolean ContainsInvariant(this String source, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        Int32 index = source.IndexOfInvariant(substring);
        // ----------------------------------------------------------------------------------------------------
        return (index != -1);
    }

    public static Boolean ContainsInvariant(this String source, String substring, Boolean ignoreCase, Boolean ignoreCommas)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCommas)
        {
            source = source.Replace(",", "");
            substring = substring.Replace(",", "");
        }
        // ----------------------------------------------------------------------------------------------------
        return ignoreCase 
               ? source.ContainsInvariantIgnoreCase(substring) 
               : source.ContainsInvariant(substring);
    }

    public static Boolean ContainsInvariantIgnoreCase(this String source, String substring)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        Int32 index = source.IndexOfInvariantIgnoreCase(substring);
        // ----------------------------------------------------------------------------------------------------
        return (index != -1);
    }

    public static Boolean ContainsLower(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Any(x => x >= 'a' && x <= 'z');
    }

    public static Boolean ContainsNarrowString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
        Int32 stringBytes = enc.GetByteCount(value);
        // ----------------------------------------------------------------------------------------------------
        return stringBytes != value.Length * 2;
    }

    public static Boolean ContainsUpper(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Any(x => x >= 'A' && x <= 'Z');
    }

    public static Boolean ContainsWideString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");
        Int32 stringBytes = enc.GetByteCount(value);
        // ----------------------------------------------------------------------------------------------------
        return stringBytes != value.Length;
    }
}