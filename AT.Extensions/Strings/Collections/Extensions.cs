using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Strings.Collections;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly Dictionary<Char, byte> CharToNibbleMapping = new Dictionary<Char, byte>
        {
            { '0', 0 }, { '1', 1 }, { '2', 2 }, { '3', 3 }, { '4', 4 }, { '5', 5 }, { '6', 6 }, { '7', 7 }, { '8', 8 }, { '9', 9 },
            { 'A', 10 }, { 'a', 10}, { 'B', 11 }, { 'b', 11 }, { 'C', 12 }, { 'c', 12 }, { 'D', 13 }, { 'd', 13 }, { 'E', 14 }, { 'e', 14 }, { 'F', 15 }, { 'f', 15 }
        };

    #endregion

    #region Method(s): Private

    private static AT.Infrastructure.TextElementSegment CreateSegment(Int32 offset, Int32 length)
    {
        return new AT.Infrastructure.TextElementSegment(offset, length);
    }

    #endregion

    public static IEnumerable<String> AllBetween(this String input, Char enclosureCharacter)
    {
        return AllBetween(input, enclosureCharacter, enclosureCharacter);
    }

    public static IEnumerable<String> AllBetween(this String input, Char firstEnclosureCharacter, Char secondEnclosureCharacter)
    {
        if (input == default)
            throw new ArgumentNullException("input");

        return AllBetweenCore(input, firstEnclosureCharacter, secondEnclosureCharacter);
    }

    public static IEnumerable<String> AllBetween(this String input, String enclosure)
    {
        return AllBetween(input, enclosure, StringComparison.Ordinal);
    }

    public static IEnumerable<String> AllBetween(this String input, String enclosure, StringComparison comparisonType)
    {
        return AllBetween(input, enclosure, enclosure, comparisonType);
    }

    public static IEnumerable<String> AllBetween(this String input, String firstEnclosure, String secondEnclosure, StringComparison comparisonType)
    {
        // preconditions
        if (input == default)
            throw new ArgumentNullException("input");
        if (firstEnclosure == default)
            throw new ArgumentNullException("firstEnclosure");
        if (secondEnclosure == default)
            throw new ArgumentNullException("secondEnclosure");

        return AllBetweenImpl(input, firstEnclosure, secondEnclosure, comparisonType);
    }

    private static IEnumerable<String> AllBetweenCore(this String input, Char firstEnclosureCharacter, Char secondEnclosureCharacter)
    {
        Int32 firstEnclosureCharacterIndex = input.IndexOf(firstEnclosureCharacter);
        while (firstEnclosureCharacterIndex != -1 && firstEnclosureCharacterIndex < input.Length - 1)
        {
            Int32 firstAdjustedIndex = firstEnclosureCharacterIndex + 1;
            Int32 secondEnclosureCharacterIndex = input.IndexOf(secondEnclosureCharacter, firstAdjustedIndex);
            if (secondEnclosureCharacterIndex == -1)
                break;
            else
            {
                Int32 length = secondEnclosureCharacterIndex - firstAdjustedIndex;

                String part = input.Substring(firstAdjustedIndex, length);

                yield return part;

                firstEnclosureCharacterIndex = input.IndexOf(firstEnclosureCharacter, secondEnclosureCharacterIndex + 1);
            }
        }
    }

    private static IEnumerable<String> AllBetweenImpl(this String input, String firstEnclosure, String secondEnclosure, StringComparison comparisonType)
    {
        Int32 firstEnclosureIndex = input.IndexOf(firstEnclosure, comparisonType);
        while (firstEnclosureIndex != -1 && firstEnclosureIndex + firstEnclosure.Length < input.Length)
        {
            Int32 firstAdjustedIndex = firstEnclosureIndex + firstEnclosure.Length;
            Int32 secondEnclosureIndex = input.IndexOf(secondEnclosure, firstAdjustedIndex, comparisonType);
            if (secondEnclosureIndex == -1)
            {
                break;
            }
            else
            {
                Int32 length = secondEnclosureIndex - firstAdjustedIndex;

                String substring = input.Substring(firstAdjustedIndex, length);

                yield return substring;

                firstEnclosureIndex = input.IndexOf(firstEnclosure, secondEnclosureIndex + secondEnclosure.Length);
            }
        }
    }

    public static IOrderedEnumerable<(Int32 Length, Int32 Count)> CountWordLengths(this String text)
    {
        var defaultValue = Enumerable.Empty<(Int32 Length, Int32 Count)>().OrderBy(wordLength => wordLength.Length);

        if (String.IsNullOrWhiteSpace(text))
            return defaultValue;

        var lengths = text.Words()?.GroupBy(word => word.Length);

        var result = lengths?.Select(lengthGroup => (Length: lengthGroup.Key, Count: lengthGroup.Count())).OrderBy(wordLength => wordLength.Length) ?? defaultValue;

        return result;
    }

    public static IEnumerable<Int32> ExtractInts(this String? value)
    {
        if (value == default)
            return Enumerable.Empty<Int32>();
        return System.Text.RegularExpressions.Regex.Matches(value, @"-?( )?\d+").Select(i => Int32.Parse(i.Value.Replace(" ", "")));
    }

    public static byte[]? FromBase64StringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
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

    public static byte[]? FromBase64UrlStringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
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
        var padding = paddingRequired == 0 ? String.Empty : new String('=', 4 - paddingRequired);
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

    public static List<String> FromCommaSeparatedToList(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        List<String> values = new();
        foreach (string item in value.Split(','))
            values.Add(item);
        // ----------------------------------------------------------------------------------------------------
        return values;
    }

    public static byte[]? FromHexStringToByteArray(this String? value, Boolean shouldReturnNullIfConversionFailed = true)
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

    public static Int32[] GetAscii(this String str)
    {
        Int32[] asciiArr = new Int32[str.Length];
        for (Int32 i = 0; i < str.Length; i++)
        {
            asciiArr[i] = (Int32)str[i];
        }
        return asciiArr;
    }

    public static IList<String> GetPathParts(this String path)
    {
        return path.Split(new[] { Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries).ToList();
    }

    public static IDictionary<String, object> JsonToDictionary(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return (Dictionary<String, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(value, typeof(Dictionary<String, object>));
    }

    public static String[] PascalToSpacedStringArray(this String input)
    {
        return input.PascalToSpacedString().Split(' ');
    }

    public static IDictionary<String, String> QueryStringToDictionary(this String queryString)
    {
        if (String.IsNullOrWhiteSpace(queryString))
        {
            return null;
        }
        if (!queryString.Contains("?"))
        {
            return null;
        }
        String query = queryString.Replace("?", "");
        if (!query.Contains("="))
        {
            return null;
        }
        return query.Split('&').Select(p => p.Split('=')).ToDictionary(
            key => key[0].ToLower().Trim(), value => value[1]);
    }

    public static IEnumerable<String> Range(this String str, Int32 start, Int32 end, params Char[] split)
    {
        return str.Split(split).Skip(start).Take(end - start + 1);
    }

    public static IEnumerable<String> Range(this String str, Int32 start, Int32 end, params String[] split)
    {
        return str.Split(split, StringSplitOptions.RemoveEmptyEntries).Skip(start).Take(end - start);
    }

    public static IEnumerable<String> ReadLines(this String text)
    {
        var reader = new StringReader(text);
        String? line;
        while ((line = reader.ReadLine()) != null)
        {
            yield return line;
        }
    }

    public static IEnumerable<String> Sentences(this String text, Boolean cleanNewLine = true, Boolean cleanWhitepace = true)
    {
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();

        if (String.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(text, @"((\s[^\.\!\?]\.)+|([^\.\!\?]\.)+|[^\.\!\?]+)+[\.\!\?]+(\s|$)") as IEnumerable<System.Text.RegularExpressions.Match>;
        IEnumerable<String> result = matches.Where(match => match.Success && !String.IsNullOrWhiteSpace(match.Value)).Select(match => match.Value);

        if (cleanNewLine)
            result = result.Select(sentence => sentence.Replace(Environment.NewLine, String.Empty));

        if (cleanWhitepace)
            result = result.Select(sentence => sentence.CleanWhiteSpace1());

        return result;
    }

    public static IEnumerable<String> SplitAndTrim(this String value, params Char[] separators)
    {
        return value.Trim().Split(separators, StringSplitOptions.RemoveEmptyEntries).Select(s => s.Trim());
    }

    public static IEnumerable<String> SplitCamelCase(this String self)
    {
        const String PATTERN = @"[A-Z][a-z]*|[a-z]+|\d+";
        var matches = System.Text.RegularExpressions.Regex.Matches(self, PATTERN);

        ICollection<String> words = new List<String>();
        foreach (System.Text.RegularExpressions.Match match in matches.Cast<System.Text.RegularExpressions.Match>())
            words.Add(match.Value);
        return words;
    }

    public static String[] SplitIntoLines(this String s)
    {
        String cleaned = s.Replace("\r\n", "\n");
        String[] r = cleaned.Split(new[] { '\r', '\n' });
        return r;
    }

    public static IEnumerable<String> TextElement(this String input)
    {
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(input);

        while (elementEnumerator.MoveNext())
        {
            String textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }

    public static IEnumerable<AT.Infrastructure.TextElementSegment> TextElementSegments(String input)
    {
        Int32[] elementOffsets = System.Globalization.StringInfo.ParseCombiningCharacters(input);

        Int32 lastOffset = -1;
        foreach (Int32 offset in elementOffsets)
        {
            if (lastOffset != -1)
            {
                Int32 elementLength = offset - lastOffset;
                AT.Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, elementLength);
                yield return segment;
            }

            lastOffset = offset;
        }

        if (lastOffset != -1)
        {
            Int32 lastSegmentLength = input.Length - lastOffset;

            AT.Infrastructure.TextElementSegment segment = CreateSegment(lastOffset, lastSegmentLength);
            yield return segment;
        }
    }

    public static byte[] ToBytes(this String val)
    {
        var bytes = new byte[val.Length * sizeof(Char)];
        Buffer.BlockCopy(val.ToCharArray(), 0, bytes, 0, bytes.Length);
        return bytes;
    }

    public static ICollection<String> ToChunks(this String str, Int32 chunkSize)
    {
        System.Collections.ObjectModel.Collection<String> c = new();
        if (str != null)
        {
            Int32 cnt = 0;
            System.Text.StringBuilder sb = new();
            foreach (Char ch in str)
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

    public static List<String> ToCollection(this String input, Char delimiter = ',')
    {
        if (!input.Contains(delimiter))
        {
            return new List<String> { input };
        }
        return input.Split(delimiter).ToList();
    }

    public static String[] ToDelimitedArray(this String content, Char delimiter = ',')
    {
        String[] array = content.Split(delimiter);
        for (Int32 i = 0; i < array.Length; i++)
        {
            array[i] = array[i].Trim();
        }

        return array;
    }

    public static List<String> Tokenize(this String s, Func<Char, Boolean> predicate)
    {
        List<String> tokens = new();
        String? token;
        Int32 pos = 0;

        while ((token = s.GetNextToken(predicate, ref pos)) != null)
            tokens.Add(token);

        return tokens;
    }

    public static List<String> Tokenize(this String s, String delimiterChars, Boolean ignoreCase = false)
    {
        HashSet<Char> hashSet = new(delimiterChars, Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));
        List<String> tokens = new();
        String? token;
        Int32 pos = 0;

        while ((token = s.GetNextToken(hashSet.Contains, ref pos)) != null)
            tokens.Add(token);

        return tokens;
    }

    public static List<String> ToList(this String s)
    {
        return new List<String> { s };
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

    public static IEnumerable<String> ToTextElements(this String val)
    {
        if (val == default)
        {
            throw new ArgumentNullException("val");
        }
        System.Globalization.TextElementEnumerator elementEnumerator = System.Globalization.StringInfo.GetTextElementEnumerator(val);
        while (elementEnumerator.MoveNext())
        {
            String textElement = elementEnumerator.GetTextElement();
            yield return textElement;
        }
    }

    public static HashSet<String> UncapitalizedTitleWords { get; set; } = new(StringComparer.OrdinalIgnoreCase)
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

    public static IEnumerable<String> UniqueWords(this String text)
    {
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();

        if (String.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<String> result = text.Words()?.GroupBy(word => word).Select(wordGroup => wordGroup.Key) ?? defaultValue;

        return result;
    }

    public static IEnumerable<String> Words(this String text)
    {
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();

        if (String.IsNullOrWhiteSpace(text))
            return defaultValue;

        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(text, @"\w+") as IEnumerable<System.Text.RegularExpressions.Match>;

        return matches.Where(match => match.Success && !String.IsNullOrWhiteSpace(match.Value)).Select(match => match.Value);
    }
}