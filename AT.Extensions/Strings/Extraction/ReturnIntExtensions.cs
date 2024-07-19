using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class ReturnIntExtensions : Object
{
    public static Int32 AsInt(this String value)
    {
        return value.AsInt(0);
    }

    public static Int32 AsInt(this String value, Int32 defaultValue)
    {
        Int32 result;
        if (!Int32.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static Int32 Count(this String source)
    {
        if (source == default)
        {
            throw new ArgumentNullException("source");
        }
        return source.Length;
    }

    public static Int32 CountNonSpecialSymbols(this String str)
    {
        Int32 count = 0;
        count = str.Count(c => Char.IsLetterOrDigit(c));
        return count;
    }

    public static Int32 CountOccurrences(this String val, String stringToMatch)
    {
        return System.Text.RegularExpressions.Regex.Matches(val, stringToMatch, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;
    }

    public static Int32 CountSentences(this String text)
    {
        if (String.IsNullOrWhiteSpace(text))
            return -1;

        return text.Sentences()?.Count() ?? -1;
    }

    public static Int32 CountSubstring(this String input, String value)
    {
        return CountSubstring(input, value, StringComparison.Ordinal);
    }

    public static Int32 CountSubstring(this String input, String value, StringComparison comparisonType)
    {
        // preconditions
        if (input == default)
            throw new ArgumentNullException("input");

        return CountSubstring(input, value, 0, input.Length, comparisonType);
    }

    public static Int32 CountSubstring(this String input, String value, Int32 startIndex, Int32 count, StringComparison comparisonType)
    {
        // preconditions
        if (input == default)
            throw new ArgumentNullException("input");
        if (value == default)
            throw new ArgumentNullException("value");
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");

        Int32 occurences = 0;
        Int32 valueLength = value.Length;

        // prevent empty startsWiths from being counted
        if (valueLength > 0)
        {
            Int32 currentIndex = startIndex;
            Int32 maxIndex = startIndex + count;

            while (currentIndex < maxIndex)
            {
                Int32 lastIndex = currentIndex;
                Int32 newIndex = input.IndexOf(value, lastIndex, maxIndex - lastIndex, comparisonType);

                if (newIndex != -1)
                {
                    occurences++;
                    currentIndex = newIndex + valueLength;
                }
                else
                {
                    break;
                }
            }
        }

        return occurences;
    }

    public static Int32 CountSubstringEnd(this String input, String endsWith)
    {
        return CountSubstringEnd(input, endsWith, StringComparison.Ordinal);
    }

    public static Int32 CountSubstringEnd(this String input, String endsWith, StringComparison comparisonType)
    {
        // preconditions
        if (input == default)
            throw new ArgumentNullException("input");
        if (endsWith == default)
            throw new ArgumentNullException("endsWith");

        Int32 occurences = 0;
        Int32 endsWithLength = endsWith.Length;

        // prevent empty startsWiths from being counted
        if (endsWithLength > 0)
        {
            String currentComparand = input;

            // keep on looping until no occurrence is found which is guarded by the break statement
            while (true)
            {
                Boolean occurenceResult = currentComparand.EndsWith(endsWith, comparisonType);

                if (occurenceResult)
                {
                    occurences++;
                    currentComparand = currentComparand.Substring(0, currentComparand.Length - endsWithLength);
                }
                else
                {
                    break;
                }
            }
        }

        return occurences;
    }

    public static Int32 CountSubstringStart(this String input, String startsWith)
    {
        return CountSubstringStart(input, startsWith, StringComparison.Ordinal);
    }

    public static Int32 CountSubstringStart(this String input, String startsWith, StringComparison comparisonType)
    {
        // preconditions
        if (input == default)
            throw new ArgumentNullException("input");
        if (startsWith == default)
            throw new ArgumentNullException("startsWith");

        Int32 occurences = 0;
        Int32 startsWithLength = startsWith.Length;

        // prevent empty startsWiths from being counted
        if (startsWithLength > 0)
        {
            String currentComparand = input;

            // keep on looping until no occurrence is found which is guarded by the break statement
            while (true)
            {
                Boolean occurenceResult = currentComparand.StartsWith(startsWith, comparisonType);

                if (occurenceResult)
                {
                    occurences++;
                    currentComparand = currentComparand.Substring(startsWithLength);
                }
                else
                {
                    break;
                }
            }
        }

        return occurences;
    }

    public static Int32 CountUniqueWords(this String text)
    {
        if (String.IsNullOrWhiteSpace(text))
            return -1;

        return text.UniqueWords()?.Count() ?? -1;
    }

    public static Int32 CountWords(this String text)
    {
        if (String.IsNullOrWhiteSpace(text))
            return -1;

        return text.Words()?.Count() ?? -1;
    }

    public static Int32 CountWords2(this String s)
    {
        Boolean wasSpace = true;
        Int32 words = 0;

        foreach (Char c in s)
        {
            if (Char.IsWhiteSpace(c))
            {
                wasSpace = true;
            }
            else
            {
                if (wasSpace)
                    words++;
                wasSpace = false;
            }
        }

        return words;
    }

    public static Int32 CountWords3(this String str)
    {
        String sTemp = System.Text.RegularExpressions.Regex.Replace(str, "( ){2,}", " ");
        return sTemp.Split(new Char[] { ' ' }).Length; //, ',', '.', '?', '!' 

    }

    public static Int32 GetByteSize(this String val, System.Text.Encoding encoding)
    {
        if (val == default)
        {
            throw new ArgumentNullException("val");
        }
        if (encoding == default)
        {
            throw new ArgumentNullException("encoding");
        }
        return encoding.GetByteCount(val);
    }

    public static Int32 Hash(this String text)
    {
        UInt32 uiHash = 0;

        foreach (byte letter in System.Text.Encoding.Unicode.GetBytes(text))
        {
            uiHash += letter;
            uiHash += (uiHash << 10);
            uiHash ^= (uiHash >> 6);
        }

        uiHash += (uiHash << 3);
        uiHash ^= (uiHash >> 11);
        uiHash += (uiHash << 15);
        return (Int32)(uiHash % Int32.MaxValue);
    }

    public static Int32 IndexOfInvariant(this String s, String substring)
    {
        return s.SafeIndexOf(substring, StringComparison.Ordinal);
    }

    public static Int32 IndexOfInvariant(this String value, String substring, Int32 startIndex)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (substring.IsNullOrEmpty() || substring.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(substring));
        // ----------------------------------------------------------------------------------------------------
        return value.IndexOf(substring, startIndex, StringComparison.Ordinal);
    }

    public static Int32 IndexOfInvariantIgnoreCase(this String s, String substring)
    {
        return s.SafeIndexOf(substring, StringComparison.OrdinalIgnoreCase);
    }

    public static Int32 LastIndexOfInvariant(this String s, String substring)
    {
        return s.LastIndexOf(substring, StringComparison.Ordinal);
    }

    public static Int32 PrefixCount(this String value, String prefix)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        Int32 result = default;
        // ----------------------------------------------------------------------------------------------------
        if (prefix.IsNullOrEmpty() || prefix.IsNullOrWhiteSpace())
            result = -1;
        else
            while (value.StartsWithInvariant(prefix))
            {
                value = value.Substring(prefix.Length);
                result++;
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Int32 SafeIndexOf(this String value, String substring, StringComparison comparison)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (substring.IsNullOrEmpty() || substring.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(substring));
        // ----------------------------------------------------------------------------------------------------
        return value.IndexOf(substring, comparison);
    }

    public static Int32 SafeLength(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length;
    }

    public static Int32 Size(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Length * sizeof(Char);
    }

    public static Int32 Size(this String value, System.Text.Encoding encoding)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (encoding == default)
            throw new ArgumentNullException(nameof(encoding));
        // ----------------------------------------------------------------------------------------------------
        return encoding.GetByteCount(value);
    }

    public static Int32 SizeAs(this String input, System.Text.Encoding targetEncoding)
    {
        return SizeAs(input, targetEncoding, System.Text.Encoding.Unicode);
    }

    public static Int32 SizeAs(this String input, System.Text.Encoding targetEncoding, System.Text.Encoding sourceEncoding)
    {
        if (input == default)
            throw new ArgumentNullException("input");
        if (targetEncoding == default)
            throw new ArgumentNullException("targetEncoding");
        if (sourceEncoding == default)
            throw new ArgumentNullException("sourceEncoding");

        byte[] sourceBytes = sourceEncoding.GetBytes(input);

        byte[] targetBytes = System.Text.Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);

        return targetBytes.Length;
    }

    public static Int32 Width(this String input)
    {
        if (input == default)
            throw new ArgumentNullException("input");

        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(input);
        Int32 count = 0;

        while (elementEnumerator.MoveNext())
        {
            count++;
        }

        return count;
    }
}