using AT.Extensions.Strings.Extraction;
using static AT.Extensions.Strings.Comparison.Extensions;

namespace AT.Extensions.Strings.Collections;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly Dictionary<char, byte> CharToNibbleMapping = new Dictionary<char, byte>
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'A', 10 }, { 'a', 10}, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 }, { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }
        };

    #endregion

    #region Method(s): Private

    private static AT.Infrastructure.TextElementSegment CreateSegment(int offset, int length)
    {
        return new AT.Infrastructure.TextElementSegment(offset, length);
    }

    #endregion

    public static IEnumerable<string> AllBetween(this String input, char enclosureCharacter)
    {
        return AllBetween(input, enclosureCharacter, enclosureCharacter);
    }

    public static IEnumerable<string> AllBetween(this String input, char firstEnclosureCharacter, char secondEnclosureCharacter)
    {
        if (input == null)
            throw new ArgumentNullException("input");

        return AllBetweenCore(input, firstEnclosureCharacter, secondEnclosureCharacter);
    }

    public static IEnumerable<string> AllBetween(this String input, string enclosure)
    {
        return AllBetween(input, enclosure, StringComparison.Ordinal);
    }

    public static IEnumerable<string> AllBetween(this String input, string enclosure, StringComparison comparisonType)
    {
        return AllBetween(input, enclosure, enclosure, comparisonType);
    }

    public static IEnumerable<string> AllBetween(this String input, string firstEnclosure, string secondEnclosure, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (firstEnclosure == null)
            throw new ArgumentNullException("firstEnclosure");
        if (secondEnclosure == null)
            throw new ArgumentNullException("secondEnclosure");

        return AllBetweenImpl(input, firstEnclosure, secondEnclosure, comparisonType);
    }

    private static IEnumerable<string> AllBetweenCore(this String input, char firstEnclosureCharacter, char secondEnclosureCharacter)
    {
        int firstEnclosureCharacterIndex = input.IndexOf(firstEnclosureCharacter);
        while (firstEnclosureCharacterIndex != -1 && firstEnclosureCharacterIndex < input.Length - 1)
        {
            int firstAdjustedIndex = firstEnclosureCharacterIndex + 1;
            int secondEnclosureCharacterIndex = input.IndexOf(secondEnclosureCharacter, firstAdjustedIndex);
            if (secondEnclosureCharacterIndex == -1)
                break;
            else
            {
                int length = secondEnclosureCharacterIndex - firstAdjustedIndex;

                string part = input.Substring(firstAdjustedIndex, length);

                yield return part;

                firstEnclosureCharacterIndex = input.IndexOf(firstEnclosureCharacter, secondEnclosureCharacterIndex + 1);
            }
        }
    }

    private static IEnumerable<string> AllBetweenImpl(this String input, string firstEnclosure, string secondEnclosure, StringComparison comparisonType)
    {
        int firstEnclosureIndex = input.IndexOf(firstEnclosure, comparisonType);
        while (firstEnclosureIndex != -1 && firstEnclosureIndex + firstEnclosure.Length < input.Length)
        {
            int firstAdjustedIndex = firstEnclosureIndex + firstEnclosure.Length;
            int secondEnclosureIndex = input.IndexOf(secondEnclosure, firstAdjustedIndex, comparisonType);
            if (secondEnclosureIndex == -1)
            {
                break;
            }
            else
            {
                int length = secondEnclosureIndex - firstAdjustedIndex;

                string substring = input.Substring(firstAdjustedIndex, length);

                yield return substring;

                firstEnclosureIndex = input.IndexOf(firstEnclosure, secondEnclosureIndex + secondEnclosure.Length);
            }
        }
    }

    public static IOrderedEnumerable<(int Length, int Count)> CountWordLengths(this String text)
    {
        var defaultValue = Enumerable.Empty<(int Length, int Count)>().OrderBy(wordLength => wordLength.Length);

        if (string.IsNullOrWhiteSpace(text))
            return defaultValue;

        var lengths = text.Words()?.GroupBy(word => word.Length);

        var result = lengths?.Select(lengthGroup => (Length: lengthGroup.Key, Count: lengthGroup.Count())).OrderBy(wordLength => wordLength.Length) ?? defaultValue;

        return result;
    }

    public static IEnumerable<int> ExtractInts(this string? value)
    {
        if (value == null)
            return Enumerable.Empty<int>();
        return System.Text.RegularExpressions.Regex.Matches(value, @"-?( )?\d+").Select(i => int.Parse(i.Value.Replace(" ", "")));
    }

    public static byte[]? FromBase64StringToByteArray(this string? value, bool shouldReturnNullIfConversionFailed = true)
    {
        if (value is null)
            return Array.Empty<byte>();

        try
        {
            return System.Convert.FromBase64String(value);
        }
        catch (FormatException ex)
        {
            if (shouldReturnNullIfConversionFailed)
                return null;

            throw new ArgumentException(ex.Message, nameof(value), ex);
        }
    }

    public static byte[]? FromBase64UrlStringToByteArray(this string? value, bool shouldReturnNullIfConversionFailed = true)
    {
        if (value is null)
            return Array.Empty<byte>();

        if (value.Contains("+") || value.Contains("/"))
        {
            if (shouldReturnNullIfConversionFailed)
                return null;

            throw new ArgumentException(AT.Infrastructure.ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));
        }

        value = value.Replace("%3D", "=");
        var paddingRequired = value.Length % 4;
        var padding = paddingRequired == 0 ? string.Empty : new string('=', 4 - paddingRequired);
        var base64 = value.Replace("-", "+").Replace("_", "/") + padding;

        try
        {
            return System.Convert.FromBase64String(base64);
        }
        catch (FormatException ex)
        {
            if (shouldReturnNullIfConversionFailed)
                return null;

            if (ex.Message.Contains("illegal"))
                throw new ArgumentException(AT.Infrastructure.ExceptionMessages.Base64UrlIllegalCharacter, nameof(value));

            throw new ArgumentException(ex.Message, nameof(value), ex);
        }
    }

    public static List<string> FromCommaSeparatedToList(this String str)
    {
        List<string> value = new List<string>();
        if (!string.IsNullOrEmpty(str))
        {
            foreach (var item in str.Split(','))
            {
                value.Add(item);
            }
        }

        return value;
    }

    public static byte[]? FromHexStringToByteArray(this string? value, bool shouldReturnNullIfConversionFailed = true)
    {
        if (value is null)
            return Array.Empty<byte>();

        value = value.StartsWith("0x")
            ? value[2..]
            : value;

        var output = new byte[value.Length / 2];
        if (value.Length % 2 != 0)
        {
            if (shouldReturnNullIfConversionFailed) return null;
            throw new ArgumentException(AT.Infrastructure.ExceptionMessages.HexStringInvalidLength, nameof(value));
        }

        var outputIndex = 0;
        for (var idx = 0; idx < value.Length; idx += 2, outputIndex += 1)
        {
            try
            {
                var highNibble = CharToNibbleMapping[value[idx]];
                var lowNibble = CharToNibbleMapping[value[idx + 1]];
                output[outputIndex] = (byte)((highNibble << 4) | lowNibble);
            }
            catch (KeyNotFoundException)
            {
                if (shouldReturnNullIfConversionFailed) return null;
                throw new ArgumentException(AT.Infrastructure.ExceptionMessages.HexStringHasIllegalCharacter, nameof(value));
            }
        }

        return output;
    }

    public static int[] GetAscii(this String str)
    {
        int[] asciiArr = new int[str.Length];
        for (int i = 0; i < str.Length; i++)
        {
            asciiArr[i] = (int)str[i];
        }
        return asciiArr;
    }

    public static IList<string> GetPathParts(this String path)
    {
        return path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static IDictionary<string, object> JsonToDictionary(this String val)
    {
        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException("val");
        }
        return
            (Dictionary<string, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(val, typeof(Dictionary<string, object>));
    }

    public static string[] PascalToSpacedStringArray(this String input)
    {
        return input.PascalToSpacedString().Split(' ');
    }

    public static IDictionary<string, string> QueryStringToDictionary(this String queryString)
    {
        if (string.IsNullOrWhiteSpace(queryString))
        {
            return null;
        }
        if (!queryString.Contains("?"))
        {
            return null;
        }
        string query = queryString.Replace("?", "");
        if (!query.Contains("="))
        {
            return null;
        }
        return query.Split('&').Select(p => p.Split('=')).ToDictionary(
            key => key[0].ToLower().Trim(), value => value[1]);
    }

    public static IEnumerable<string> Range(this String str, int start, int end, params char[] split)
    {
        return str.Split(split).Skip(start).Take(end - start + 1);
    }

    public static IEnumerable<string> Range(this String str, int start, int end, params string[] split)
    {
        return str.Split(split, StringSplitOptions.RemoveEmptyEntries).Skip(start).Take(end - start);
    }

    public static IEnumerable<string> ReadLines(this String text)
    {
        var reader = new StringReader(text);
        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }

    public static IEnumerable<string> Sentences(this String text, bool cleanNewLine = true, bool cleanWhitepace = true)
    {
        IEnumerable<string> defaultValue = Enumerable.Empty<string>();

        if (string.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(text, @"((\s[^\.\!\?]\.)+|([^\.\!\?]\.)+|[^\.\!\?]+)+[\.\!\?]+(\s|$)") as IEnumerable<System.Text.RegularExpressions.Match>;
        IEnumerable<string> result = matches.Where(match => match.Success && !string.IsNullOrWhiteSpace(match.Value)).Select(match => match.Value);

        if (cleanNewLine)
            result = result.Select(sentence => sentence.Replace(Environment.NewLine, string.Empty));

        if (cleanWhitepace)
            result = result.Select(sentence => sentence.CleanWhiteSpace1());

        return result;
    }

    public static IEnumerable<string> SplitAndTrim(this String value, params char[] separators)
    {
        return value.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
    }

    public static IEnumerable<string> SplitCamelCase(this String self)
    {
        const string PATTERN = @"[A-Z][a-z]*|[a-z]+|\d+";
        var matches = System.Text.RegularExpressions.Regex.Matches(self, PATTERN);

        ICollection<string> words = new List<string>();
        foreach (System.Text.RegularExpressions.Match match in matches.Cast<System.Text.RegularExpressions.Match>())
            words.Add(match.Value);
        return words;
    }

    public static string[] SplitIntoLines(this String s)
    {
        string cleaned = s.Replace("\r\n", "\n");
        string[] r = cleaned.Split(new[] { '\r', '\n' });
        return r;
    }

    public static IEnumerable<string> TextElement(this String input)
    {
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(input);

        while (elementEnumerator.MoveNext())
        {
            string textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }

    public static IEnumerable<AT.Infrastructure.TextElementSegment> TextElementSegments(string input)
    {
        int[] elementOffsets = System.Globalization.StringInfo.ParseCombiningCharacters(input);

        int lastOffset = -1;
        foreach (int offset in elementOffsets)
        {
            if (lastOffset != -1)
            {
                int elementLength = offset - lastOffset;
                AT.Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, elementLength);
                yield return segment;
            }

            lastOffset = offset;
        }

        if (lastOffset != -1)
        {
            int lastSegmentLength = input.Length - lastOffset;

            AT.Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, lastSegmentLength);
            yield return segment;
        }
    }

    public static byte[] ToBytes(this String val)
    {
        var bytes = new byte[val.Length * sizeof(char)];
        Buffer.BlockCopy(val.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static ICollection<string> ToChunks(this String str, int chunkSize)
    {
        System.Collections.ObjectModel.Collection<string> c = new();
        if (str != null)
        {
            int cnt = 0;
            System.Text.StringBuilder sb = new();
            foreach (char ch in str)
            {
                sb.Append(ch);
                if (++cnt == chunkSize)
                {
                    cnt = 0;
                    c.Add(sb.ToString());
                    sb.Clear();
                }
            }
            if (cnt > 0) { c.Add(sb.ToString()); }
        }
        return c;
    }

    public static List<string> ToCollection(this String input, char delimiter = ',')
    {
        if (!input.Contains(delimiter))
        {
            return new List<string> { input };
        }
        return input.Split(delimiter).ToList();
    }

    public static string[] ToDelimitedArray(this String content, char delimiter = ',')
    {
        string[] array = content.Split(delimiter);
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Trim();
        }

        return array;
    }

    public static List<string> Tokenize(this String s, Func<char, bool> predicate)
    {
        List<string> tokens = new();
        string? token;
        int pos = 0;

        while ((token = s.GetNextToken(predicate, ref pos)) != null)
            tokens.Add(token);

        return tokens;
    }

    public static List<string> Tokenize(this String s, string delimiterChars, bool ignoreCase = false)
    {
        HashSet<char> hashSet = new(delimiterChars, CharComparer.GetEqualityComparer(ignoreCase));
        List<string> tokens = new();
        string? token;
        int pos = 0;

        while ((token = s.GetNextToken(hashSet.Contains, ref pos)) != null)
            tokens.Add(token);

        return tokens;
    }

    public static List<string> ToList(this String s)
    {
        return new List<string> { s };
    }

    public static (MemoryStream MemoryStream, System.Text.Encoding Encoding) ToMemoryStream(this String self, System.Text.Encoding? encoding = default)
    {
        MemoryStream memoryStream = new MemoryStream();
        (Stream Stream, System.Text.Encoding Encoding) streamInfo = self.ToStream(encoding);
        streamInfo.Stream.CopyTo(memoryStream);

        return (memoryStream, streamInfo.Encoding);
    }

    public static (Stream Stream, System.Text.Encoding Encoding) ToStream(this String self, System.Text.Encoding? encoding = default)
    {
        if (encoding is null)
            encoding = System.Text.Encoding.UTF8;

        byte[] bytes = encoding.GetBytes(self);
        return (new MemoryStream(bytes), encoding);
    }

    public static IEnumerable<string> ToTextElements(this String val)
    {
        if (val == null)
        {
            throw new ArgumentNullException("val");
        }
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(val);
        while (elementEnumerator.MoveNext())
        {
            string textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }

    public static HashSet<string> UncapitalizedTitleWords { get; set; } = new(StringComparer.OrdinalIgnoreCase)
        {
            "a",
            "about",
            "after",
            "an",
            "and",
            "are",
            "around",
            "as",
            "at",
            "be",
            "before",
            "but",
            "by",
            "else",
            "for",
            "from",
            "how",
            "if",
            "in",
            "is",
            "into",
            "nor",
            "of",
            "on",
            "or",
            "over",
            "than",
            "that",
            "the",
            "then",
            "this",
            "through",
            "to",
            "under",
            "when",
            "where",
            "why",
            "with"
        };

    public static IEnumerable<string> UniqueWords(this String text)
    {
        IEnumerable<string> defaultValue = Enumerable.Empty<string>();

        if (string.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<string> result = text.Words()?.GroupBy(word => word).Select(wordGroup => wordGroup.Key) ?? defaultValue;

        return result;
    }

    public static IEnumerable<string> Words(this String text)
    {
        IEnumerable<string> defaultValue = Enumerable.Empty<string>();

        if (string.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(text, @"\w+") as IEnumerable<System.Text.RegularExpressions.Match>;

        return matches.Where(match => match.Success && !string.IsNullOrWhiteSpace(match.Value)).Select(match => match.Value);
    }
}