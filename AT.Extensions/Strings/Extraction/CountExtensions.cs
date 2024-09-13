using AT.Extensions.Strings.Collections;

namespace AT.Extensions.Strings.Extraction;
public static class CountExtensions : Object
{
    public static Int32 Count(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length;
    }

    public static Int32 CountNonSpecialSymbols(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 count = default;
        count = value.Count(c => Char.IsLetterOrDigit(c));
        // ----------------------------------------------------------------------------------------------------
        return count;
    }

    public static Int32 CountOccurrences(this String value, String valueToMatch)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(valueToMatch);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Matches(value, valueToMatch, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;
    }

    public static Int32 CountSentences(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Sentences()?.Count() ?? -1;
    }

    public static Int32 CountSubstring(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return CountSubstring(input, value, StringComparison.Ordinal);
    }

    public static Int32 CountSubstring(this String input, String value, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return CountSubstring(input, value, 0, input.Length, comparisonType);
    }

    public static Int32 CountSubstring(this String input, String value, Int32 startIndex, Int32 count, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");
        // ----------------------------------------------------------------------------------------------------
        Int32 occurences = 0;
        Int32 valueLength = value.Length;
        // ----------------------------------------------------------------------------------------------------
        if (valueLength > 0)
        {
            Int32 currentIndex = startIndex;
            Int32 maxIndex = startIndex + count;
            // ----------------------------------------------------------------------------------------------------
            while (currentIndex < maxIndex)
            {
                Int32 lastIndex = currentIndex;
                Int32 newIndex = input.IndexOf(value, lastIndex, maxIndex - lastIndex, comparisonType);
                // ----------------------------------------------------------------------------------------------------
                if (newIndex != -1)
                {
                    occurences++;
                    currentIndex = newIndex + valueLength;
                }
                else
                    break;
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return occurences;
    }

    public static Int32 CountSubstringEnd(this String input, String endsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        return CountSubstringEnd(input, endsWith, StringComparison.Ordinal);
    }

    public static Int32 CountSubstringEnd(this String input, String endsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(endsWith);
        // ----------------------------------------------------------------------------------------------------
        Int32 occurences = 0;
        Int32 endsWithLength = endsWith.Length;
        // ----------------------------------------------------------------------------------------------------
        if (endsWithLength > 0)
        {
            String currentComparand = input;
            // ----------------------------------------------------------------------------------------------------
            while (true)
            {
                Boolean occurenceResult = currentComparand.EndsWith(endsWith, comparisonType);
                // ----------------------------------------------------------------------------------------------------
                if (occurenceResult)
                {
                    occurences++;
                    currentComparand = currentComparand.Substring(0, currentComparand.Length - endsWithLength);
                }
                else
                    break;
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return occurences;
    }

    public static Int32 CountSubstringStart(this String input, String startsWith)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        return CountSubstringStart(input, startsWith, StringComparison.Ordinal);
    }

    public static Int32 CountSubstringStart(this String input, String startsWith, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(startsWith);
        // ----------------------------------------------------------------------------------------------------
        Int32 occurences = 0;
        Int32 startsWithLength = startsWith.Length;
        // ----------------------------------------------------------------------------------------------------
        if (startsWithLength > 0)
        {
            String currentComparand = input;
            // ----------------------------------------------------------------------------------------------------
            while (true)
            {
                Boolean occurenceResult = currentComparand.StartsWith(startsWith, comparisonType);
                // ----------------------------------------------------------------------------------------------------
                if (occurenceResult)
                {
                    occurences++;
                    currentComparand = currentComparand.Substring(startsWithLength);
                }
                else
                    break;
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return occurences;
    }

    public static Int32 CountUniqueWords(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.UniqueWords()?.Count() ?? -1;
    }

    public static Int32 CountWords(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Words()?.Count() ?? -1;
    }
}