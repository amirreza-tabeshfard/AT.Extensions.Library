using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class ReturnIntExtensions : Object
{
    public static int AsInt(this String value)
    {
        return value.AsInt(0);
    }

    public static int AsInt(this String value, int defaultValue)
    {
        int result;
        if (!int.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static int Count(this String source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return source.Length;
    }

    public static int CountNonSpecialSymbols(this String str)
    {
        int count = 0;
        count = str.Count(c => char.IsLetterOrDigit(c));
        return count;
    }

    public static int CountOccurrences(this String val, string stringToMatch)
    {
        return System.Text.RegularExpressions.Regex.Matches(val, stringToMatch, System.Text.RegularExpressions.RegexOptions.IgnoreCase).Count;
    }

    public static int CountSentences(this String text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return -1;

        return text.Sentences()?.Count() ?? -1;
    }

    public static int CountSubstring(this String input, string value)
    {
        return CountSubstring(input, value, StringComparison.Ordinal);
    }

    public static int CountSubstring(this String input, string value, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        return CountSubstring(input, value, 0, input.Length, comparisonType);
    }

    public static int CountSubstring(this String input, string value, int startIndex, int count, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (value == null)
            throw new ArgumentNullException("value");
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");

        int occurences = 0;
        int valueLength = value.Length;

        // prevent empty startsWiths from being counted
        if (valueLength > 0)
        {
            int currentIndex = startIndex;
            int maxIndex = startIndex + count;

            while (currentIndex < maxIndex)
            {
                int lastIndex = currentIndex;
                int newIndex = input.IndexOf(value, lastIndex, maxIndex - lastIndex, comparisonType);

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

    public static int CountSubstringEnd(this String input, string endsWith)
    {
        return CountSubstringEnd(input, endsWith, StringComparison.Ordinal);
    }

    public static int CountSubstringEnd(this String input, string endsWith, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (endsWith == null)
            throw new ArgumentNullException("endsWith");

        int occurences = 0;
        int endsWithLength = endsWith.Length;

        // prevent empty startsWiths from being counted
        if (endsWithLength > 0)
        {
            string currentComparand = input;

            // keep on looping until no occurrence is found which is guarded by the break statement
            while (true)
            {
                bool occurenceResult = currentComparand.EndsWith(endsWith, comparisonType);

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

    public static int CountSubstringStart(this String input, string startsWith)
    {
        return CountSubstringStart(input, startsWith, StringComparison.Ordinal);
    }

    public static int CountSubstringStart(this String input, string startsWith, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (startsWith == null)
            throw new ArgumentNullException("startsWith");

        int occurences = 0;
        int startsWithLength = startsWith.Length;

        // prevent empty startsWiths from being counted
        if (startsWithLength > 0)
        {
            string currentComparand = input;

            // keep on looping until no occurrence is found which is guarded by the break statement
            while (true)
            {
                bool occurenceResult = currentComparand.StartsWith(startsWith, comparisonType);

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

    public static int CountUniqueWords(this String text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return -1;

        return text.UniqueWords()?.Count() ?? -1;
    }

    public static int CountWords(this String text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return -1;

        return text.Words()?.Count() ?? -1;
    }

    public static int CountWords2(this String s)
    {
        bool wasSpace = true;
        int words = 0;

        foreach (char c in s)
        {
            if (char.IsWhiteSpace(c))
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

    public static int CountWords3(this String str)
    {
        string sTemp = System.Text.RegularExpressions.Regex.Replace(str, "( ){2,}", " ");
        return sTemp.Split(new char[] { ' ' }).Length; //, ',', '.', '?', '!' 

    }

    public static int GetByteSize(this String val, System.Text.Encoding encoding)
    {
        if (val == null)
        {
            throw new ArgumentNullException("val");
        }
        if (encoding == null)
        {
            throw new ArgumentNullException("encoding");
        }
        return encoding.GetByteCount(val);
    }

    public static int Hash(this String text)
    {
        uint uiHash = 0;

        foreach (byte letter in System.Text.Encoding.Unicode.GetBytes(text))
        {
            uiHash += letter;
            uiHash += (uiHash << 10);
            uiHash ^= (uiHash >> 6);
        }

        uiHash += (uiHash << 3);
        uiHash ^= (uiHash >> 11);
        uiHash += (uiHash << 15);
        return (int)(uiHash % int.MaxValue);
    }

    public static int IndexOfInvariant(this String s, string substring)
    {
        return s.SafeIndexOf(substring, StringComparison.Ordinal);
    }

    public static int IndexOfInvariant(this String s, string substring, int startIndex)
    {
        int r = -1;
        if (s != null && !(string.IsNullOrEmpty(substring)))
        {
            r = s.IndexOf(substring, startIndex, StringComparison.Ordinal);
        }
        return r;
    }

    public static int IndexOfInvariantIgnoreCase(this String s, string substring)
    {
        return s.SafeIndexOf(substring, StringComparison.OrdinalIgnoreCase);
    }

    public static int LastIndexOfInvariant(this String s, string substring)
    {
        return s.LastIndexOf(substring, StringComparison.Ordinal);
    }

    public static int PrefixCount(this String s, string prefix)
    {
        int r = 0;
        if (string.IsNullOrEmpty(prefix))
        {
            r = -1;
        }
        else
        {
            while (s.StartsWithInvariant(prefix))
            {
                s = s.Substring(prefix.Length);
                r++;
            }
        }
        return r;
    }

    public static int SafeIndexOf(this String s, string substring, StringComparison comparison)
    {
        int r = -1;
        if (s != null && !(string.IsNullOrEmpty(substring)))
        {
            r = s.IndexOf(substring, comparison);
        }
        return r;
    }

    public static int SafeLength(this String str)
    {
        if (str == null)
        {
            return 0;
        }
        else
        {
            return str.Length;
        }
    }

    public static int Size(this String input)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        // simple implementation for utf16 which is the default encoding where chars are of a fixed size
        int result = input.Length * sizeof(char);

        return result;
    }

    public static int Size(this String input, System.Text.Encoding encoding)
    {
        if (input == null)
            throw new ArgumentNullException("input");
        if (encoding == null)
            throw new ArgumentNullException("encoding");

        return encoding.GetByteCount(input);
    }

    public static int SizeAs(this String input, System.Text.Encoding targetEncoding)
    {
        return SizeAs(input, targetEncoding, System.Text.Encoding.Unicode);
    }

    public static int SizeAs(this String input, System.Text.Encoding targetEncoding, System.Text.Encoding sourceEncoding)
    {
        if (input == null)
            throw new ArgumentNullException("input");
        if (targetEncoding == null)
            throw new ArgumentNullException("targetEncoding");
        if (sourceEncoding == null)
            throw new ArgumentNullException("sourceEncoding");

        byte[] sourceBytes = sourceEncoding.GetBytes(input);

        byte[] targetBytes = System.Text.Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);

        return targetBytes.Length;
    }

    public static int Width(this String input)
    {
        if (input == null)
            throw new ArgumentNullException("input");

        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(input);
        int count = 0;

        while (elementEnumerator.MoveNext())
        {
            count++;
        }

        return count;
    }
}