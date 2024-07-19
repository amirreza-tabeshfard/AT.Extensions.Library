using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Strings.Comparison;
public static partial class Extensions : Object
{
    #region Method(s): Private

    private class CaseSensitiveEqualityComparer : IEqualityComparer<Char>
    {
        public Boolean Equals(Char c1, Char c2)
        {
            return c1.Equals(c2);
        }

        public Int32 GetHashCode([System.Diagnostics.CodeAnalysis.DisallowNull] Char obj)
        {
            return obj.GetHashCode();
        }
    }

    private class CaseInsensitiveEqualityComparer : IEqualityComparer<Char>
    {
        public Boolean Equals(Char c1, Char c2)
        {
            return Char.ToUpper(c1).Equals(Char.ToUpper(c2));
        }

        public Int32 GetHashCode([System.Diagnostics.CodeAnalysis.DisallowNull] Char obj)
        {
            return Char.ToUpper(obj).GetHashCode();
        }
    }

    private class CaseSensitiveComparer : IComparer<Char>
    {
        public Int32 Compare(Char c1, Char c2)
        {
            return c1.CompareTo(c2);
        }
    }

    private class CaseInsensitiveComparer : IComparer<Char>
    {
        public Int32 Compare(Char c1, Char c2)
        {
            return Char.ToUpper(c1).CompareTo(Char.ToUpper(c2));
        }
    }

    internal static class CharComparer
    {
        public static IEqualityComparer<Char> GetEqualityComparer(Boolean ignoreCase) => ignoreCase ?
            new CaseInsensitiveEqualityComparer() :
            new CaseSensitiveEqualityComparer();

        public static IComparer<Char> GetComparer(Boolean ignoreCase) => ignoreCase ?
            new CaseInsensitiveComparer() :
            new CaseSensitiveComparer();
    }

    #endregion

    public static Boolean Any(this String source)
    {
        if (source == default)
        {
            throw new ArgumentNullException("source");
        }
        return source.Length != 0;
    }

    public static Boolean AsBool(this String value)
    {
        return value.AsBool(false);
    }

    public static Boolean AsBool(this String value, Boolean defaultValue)
    {
        Boolean result;
        if (!Boolean.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static Boolean CheckIfPalindrome(this String dataText)
    {
        Int32 counter = dataText.Length / 2;

        for (Int32 i = 0; i < counter; i++)
            if (dataText[i].ToString().ToLower() != dataText[dataText.Length - 1 - i].ToString().ToLower())
                return false;

        return true;
    }

    public static Boolean Contains(this String source, Char value)
    {
        if (source == default)
        {
            throw new ArgumentNullException("source");
        }
        return source.IndexOf(value) >= 0;
    }

    public static Boolean Contains(this String source, String text, StringComparison stringComparison)
    {
        return source.IndexOf(text, stringComparison) >= 0;
    }

    public static Boolean Contains(this String input, String value, Int32 startIndex, Int32 count, StringComparison comparisonType)
    {
        if (input == default)
            throw new ArgumentNullException("input");
        if (value == default)
            throw new ArgumentNullException("value");
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");

        Int32 firstIndex = input.IndexOf(value, startIndex, count, comparisonType);

        Boolean result = firstIndex != -1;

        return result;
    }

    public static Boolean Contains(this String text, IEnumerable<String> options)
    {
        if (text == default) return false;
        return options.Any(opción => text.Contains(opción));
    }

    public static Boolean Contains(this IEnumerable<String> input, String value, StringComparison stringComparison)
    {
        return input.Any(item => item.Equals(value, stringComparison));
    }

    public static Boolean Contains2(this String input, String value, StringComparison comparisonType)
    {
        if (input == default)
            throw new ArgumentNullException("input");

        return Contains(input, value, 0, input.Length, comparisonType);
    }

    public static Boolean Contains3(this String source, String value, StringComparison comparison)
    {
        return source.IndexOf(value, comparison) >= 0;
    }

    public static Boolean Contains4(this String input, String value, StringComparison stringComparison)
    {
        return input.IndexOf(value, stringComparison) >= 0;
    }

    public static Boolean Contains5(this String source, String input, StringComparison comparison)
    {
        return source.IndexOf(input, comparison) >= 0;
    }

    public static Boolean ContainsAny(this String value, String findChars, Boolean ignoreCase = false)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (findChars.IsNullOrEmpty() || findChars.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(findChars));
        // ----------------------------------------------------------------------------------------------------
        HashSet<Char> hashSet = new(findChars, CharComparer.GetEqualityComparer(ignoreCase));
        foreach (Char c in value)
            if (hashSet.Contains(c))
                return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Boolean ContainsAny(this String str, params Char[] values)
    {
        SharperHacks.CoreLibs.Constraints.Verify.IsNotNull(str, nameof(str));

        foreach (var item in values)
        {
            if (str.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    public static Boolean ContainsAny(this String str, params String[] values)
    {
        SharperHacks.CoreLibs.Constraints.Verify.IsNotNull(str, nameof(str));

        foreach (var item in values)
        {
            if (str.Contains(item))
            {
                return true;
            }
        }
        return false;
    }

    public static Boolean ContainsDigits(this String str)
    {
        return str.Any(x => x >= '0' && x <= '9');
    }

    public static Boolean ContainsIgnoreAccents(this String source, String value, Boolean ignoreCase = true)
    {
        var compareOptions = System.Globalization.CompareOptions.IgnoreNonSpace;
        if (ignoreCase)
        {
            compareOptions |= System.Globalization.CompareOptions.IgnoreCase;
        }
        return System.Globalization.CultureInfo.InvariantCulture.CompareInfo.IndexOf(source, value, compareOptions) != -1;
    }

    public static Boolean ContainsIgnoreCase(this String source, String value)
    {
        return source.Contains(value, StringComparison.OrdinalIgnoreCase);
    }

    public static Boolean ContainsIgnoreCase2(this String input, String substringValue)
    {
        return (input == default || substringValue == default)
            ? input == substringValue
            : input.IndexOf(substringValue, StringComparison.InvariantCultureIgnoreCase) >= 0;
    }

    public static Boolean ContainsIgnoreSpaces(this String s, String substring, Boolean ignoreCase = true, Boolean ignoreCommas = false)
    {
        String withoutSpaces = s.Replace(" ", "");
        String withoutSpaces2 = substring.Replace(" ", "");
        Boolean r = withoutSpaces.ContainsInvariant(withoutSpaces2, ignoreCase, ignoreCommas);
        return r;
    }

    public static Boolean ContainsInvariant(this String s, String substring)
    {
        Int32 index = s.IndexOfInvariant(substring);
        return (index != -1);
    }

    public static Boolean ContainsInvariant(this String s, String substring, Boolean ignoreCase, Boolean ignoreCommas)
    {
        if (ignoreCommas)
        {
            s = s.Replace(",", "");
            substring = substring.Replace(",", "");
        }
        Boolean r = ignoreCase ? s.ContainsInvariantIgnoreCase(substring) : s.ContainsInvariant(substring);
        return r;
    }

    public static Boolean ContainsInvariantIgnoreCase(this String s, String substring)
    {
        Int32 index = s.IndexOfInvariantIgnoreCase(substring);
        return (index != -1);
    }

    public static Boolean ContainsLower(this String str)
    {
        return str.Any(x => x >= 'a' && x <= 'z');
    }

    public static Boolean ContainsNarrowString(this String value)
    {
        if (value == default)
        {
            throw new ArgumentException();
        }

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

        Int32 stringBytes = enc.GetByteCount(value);

        return stringBytes != value.Length * 2;

    }

    public static Boolean ContainsUpper(this String str)
    {
        return str.Any(x => x >= 'A' && x <= 'Z');
    }

    public static Boolean ContainsWideString(this String value)
    {
        if (value == default)
        {
            throw new ArgumentException();
        }

        if (value.IsNullOrEmpty())
        {
            return false;
        }

        System.Text.Encoding enc = System.Text.Encoding.GetEncoding("Shift_JIS");

        Int32 stringBytes = enc.GetByteCount(value);

        return stringBytes != value.Length;
    }

    public static Boolean DoesNotEndWith(this String val, String suffix)
    {
        return val == default || suffix == default ||
               !val.EndsWith(suffix, StringComparison.InvariantCulture);
    }

    public static Boolean DoesNotStartWith(this String val, String prefix)
    {
        return val == default || prefix == default ||
               !val.StartsWith(prefix, StringComparison.InvariantCulture);
    }

    public static Boolean EndsWithIgnoreCase(this String val, String suffix)
    {
        if (val == default)
        {
            throw new ArgumentNullException("val", "val parameter is null");
        }
        if (suffix == default)
        {
            throw new ArgumentNullException("suffix", "suffix parameter is null");
        }
        if (val.Length < suffix.Length)
        {
            return false;
        }
        return val.EndsWith(suffix, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean EndsWithIgnoreCase2(this String input, String value)
    {
        return input.EndsWith(value, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean EndsWithInvariant(this String s, String suffix)
    {
        Boolean r = false;
        if (s != null && suffix != null)
        {
            r = s.EndsWith(suffix, StringComparison.Ordinal);
        }
        return r;
    }

    public static Boolean EndsWithInvariantIgnoreCase(this String s, String suffix)
    {
        Boolean r = false;
        if (s != null && suffix != null)
        {
            r = s.EndsWith(suffix, StringComparison.OrdinalIgnoreCase);
        }
        return r;
    }

    public static Boolean EqualsIgnoreCase(this String thisString, String otherString)
    {
        return thisString.Equals(otherString, StringComparison.CurrentCultureIgnoreCase);
    }

    public static Boolean EqualsIgnoreCase2(this String instance, String value)
    {
        return String.Equals(instance, value, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean Match(this String self, String pattern)
    {
        if (String.IsNullOrWhiteSpace(pattern))
            throw new ArgumentException("Regex pattern cannot be empty.", nameof(pattern));

        return System.Text.RegularExpressions.Regex.IsMatch(self, pattern);
    }

    public static Boolean SequenceEqual(this String first, String second)
    {
        if (first == default)
        {
            throw new ArgumentNullException("first");
        }
        if (second == default)
        {
            throw new ArgumentNullException("second");
        }
        return first == second;
    }

    public static Boolean SequenceEqual(this String first, String second, StringComparison comparison)
    {
        if (first == default)
        {
            throw new ArgumentNullException("first");
        }
        if (second == default)
        {
            throw new ArgumentNullException("second");
        }
        return String.Equals(first, second, comparison);
    }

    public static Boolean SpanSearcherContains(ReadOnlySpan<Char> stringToSearch, String searchFor, Int32 startAt = 0, Int32 endAt = -1)
    {
        if (stringToSearch == default) return false;
        if (stringToSearch.IsEmpty) return false;
        if (searchFor == default) return false;
        if (searchFor == String.Empty) return false;

        if (startAt < 0) throw new ArgumentException("Starting Index must be positive number");
        if (endAt < -1) throw new ArgumentException("Ending Index must be positive number");
        if (startAt > stringToSearch.Length) return false;
        if (endAt == -1) endAt = stringToSearch.Length;
        if (endAt < startAt) throw new ArgumentException("Ending index cannot be less than the starting index");
        if (endAt > stringToSearch.Length) endAt = stringToSearch.Length;

        ReadOnlySpan<Char> lookingFor = searchFor;

        for (Int32 i = startAt; i <= endAt - lookingFor.Length; i++)
            if (stringToSearch.Slice(i, lookingFor.Length).SequenceEqual(lookingFor))
                return true;

        return false;
    }

    public static Boolean StartsWithIgnoreCase(this String val, String prefix)
    {
        if (val == default)
        {
            throw new ArgumentNullException("val", "val parameter is null");
        }
        if (prefix == default)
        {
            throw new ArgumentNullException("prefix", "prefix parameter is null");
        }
        if (val.Length < prefix.Length)
        {
            return false;
        }
        return val.StartsWith(prefix, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean StartsWithIgnoreCase2(this String input, String value)
    {
        return input.StartsWith(value, StringComparison.InvariantCultureIgnoreCase);
    }

    public static Boolean StartsWithInvariant(this String s, String prefix)
    {
        Boolean r = false;
        if (s != null && prefix != null)
        {
            r = s.StartsWith(prefix, StringComparison.Ordinal);
        }
        return r;
    }
    
    public static Boolean StartsWithInvariant(this String s, String prefix1, String prefix2)
    {
        Boolean r = s.StartsWithInvariant(prefix1) || s.StartsWithInvariant(prefix2);
        return r;
    }
    
    public static Boolean StartsWithInvariant(this String s, String prefix1, String prefix2, String prefix3)
    {
        Boolean r = s.StartsWithInvariant(prefix1, prefix2) || s.StartsWithInvariant(prefix3);
        return r;
    }

    public static Boolean StartsWithInvariantIgnoreCase(this String s, String prefix)
    {
        Boolean r = false;
        if (s != null && prefix != null)
        {
            r = s.StartsWith(prefix, StringComparison.OrdinalIgnoreCase);
        }
        return r;
    }
}