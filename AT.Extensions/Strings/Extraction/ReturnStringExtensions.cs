using System.Reflection;

using AT.Extensions.Chars.Comparison;
using AT.Extensions.Chars.Conversion;
using AT.Extensions.Chars.Extraction;
using AT.Extensions.Strings.Collections;
using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class ReturnStringExtensions : Object
{
    #region Enum(s)

    public enum WordCase
    {
        AllLower = 0,
        AllUpper = 1,
        Title = 2,
        Sentence = 3
    }

    #endregion

    #region Field(s)

    private const string Quote = "\"";
    private const string SingleQuote = @"'";

    private static readonly string NegativePrefix = "negative ";

    private static readonly string[] Ones =
    {
            "zero",
            "one",
            "two",
            "three",
            "four",
            "five",
            "six",
            "seven",
            "eight",
            "nine"
        };

    private static readonly string[] Teens =
    {
            "ten",
            "eleven",
            "twelve",
            "thirteen",
            "fourteen",
            "fifteen",
            "sixteen",
            "seventeen",
            "eighteen",
            "nineteen"
        };

    private static readonly string[] Tens =
    {
            "",
            "ten",
            "twenty",
            "thirty",
            "forty",
            "fifty",
            "sixty",
            "seventy",
            "eighty",
            "ninety"
        };

    private static readonly string[] Thousands =
    {
            "",
            "thousand",
            "million",
            "billion",
            "trillion",
            "quadrillion",
            "quintillion",
            "sextillion",
            "septillion",
            "octillion",
        };

    private static readonly System.Text.RegularExpressions.RegexOptions InvariantCultureIgnoreCaseRegexOptions = System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.CultureInvariant;

    /// <summary>Three ASCII periods (...)</summary>
    private const string EllipsisAsciiSymbol = "...";
    /// <summary>The UTF-8 character representing ellipsis (\u2026)</summary>
    private const string EllipsisUtf8Symbol = "\u2026";

    #endregion

    #region Method(s): Private

    private static void EachChar(System.Text.StringBuilder builder, int start, int end, Func<char, char> action)
    {
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= builder.Length);

        for (int i = start; i < end; i++)
        {
            builder[i] = action(builder[i]);
        }
    }

    private static bool IncludesLowerCase(string s, int start, int end)
    {
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= s.Length);

        for (int i = start; i < end; i++)
        {
            if (char.IsLower(s[i]))
                return true;
        }

        return false;
    }

    private static bool IncludesLowerCase(System.Text.StringBuilder builder, int start, int end)
    {
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= builder.Length);

        for (int i = start; i < end; i++)
        {
            if (char.IsLower(builder[i]))
                return true;
        }

        return false;
    }

    private static void SetWordTitleCase(System.Text.StringBuilder builder, int wordStart, int wordEnd, ref bool inSentence)
    {
        System.Diagnostics.Debug.Assert(wordStart != -1);
        System.Diagnostics.Debug.Assert(wordStart <= wordEnd);
        System.Diagnostics.Debug.Assert(wordEnd <= builder.Length);

        // Set word to lower case if not acronym
        if (IncludesLowerCase(builder, wordStart, wordEnd))
            EachChar(builder, wordStart, wordEnd, c => char.ToLower(c));

        if (!inSentence || !Collections.Extensions.UncapitalizedTitleWords.Contains(builder.ToString(wordStart, wordEnd - wordStart)))
        {
            builder[wordStart] = char.ToUpper(builder[wordStart]);
            inSentence = true;
        }

        if (inSentence && wordEnd < builder.Length /* && builder.IsEndOfSentenceCharacter(wordEnd) */)
            inSentence = false;
    }

    private static string ConvertToWords(string input)
    {
        try
        {
            string pattern = "(?<=[A-Z])(?=[A-Z][a-z])|(?<=[^A-Z])(?=[A-Z])|(?<=[A-Za-z])(?=[^A-Za-z])";
            return System.Text.RegularExpressions.Regex.Replace(input, pattern, " ").RemoveExcessWhiteSpace();
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    private static long GetGreatestCommonDivisor(long a, long b)
    {
        while (a != 0 && b != 0)
        {
            if (a > b)
                a %= b;
            else
                b %= a;
        }

        return a | b;
    }

    private static string DecimalToFraction(decimal value)
    {
        if (value == 0m)
            return string.Empty;

        // Consider precision value to convert fractional part to integral equivalent
        long pVal = 1000000000;

        // Calculate GCD of integral equivalent of fractional part and precision value
        long gcd = GetGreatestCommonDivisor((long)Math.Round(value * pVal), pVal);

        // Calculate numerator and denominator
        long numerator = (long)Math.Round(value * pVal) / gcd;
        long denominator = pVal / gcd;

        return $"{numerator}/{denominator}";
    }

    private static void FormatNumber(System.Text.StringBuilder builder, string digits)
    {
        string s;
        bool allZeros = true;

        for (int i = digits.Length - 1; i >= 0; i--)
        {
            int ndigit = digits[i] - '0';
            int column = digits.Length - (i + 1);

            // Determine if ones, tens, or hundreds column
            switch (column % 3)
            {
                case 0:        // Ones position
                    bool showThousands = true;
                    if (i == 0)
                    {
                        // First digit in number (last in loop)
                        s = string.Format("{0} ", Ones[ndigit]);
                    }
                    else if (digits[i - 1] == '1')
                    {
                        // This digit is part of "teen" value
                        s = string.Format("{0} ", Teens[ndigit]);
                        // Skip tens position
                        i--;
                    }
                    else if (ndigit != 0)
                    {
                        // Any non-zero digit
                        s = string.Format("{0} ", Ones[ndigit]);
                    }
                    else
                    {
                        // This digit is zero. If digit in tens and hundreds
                        // column are also zero, don't show "thousands"
                        s = string.Empty;
                        // Test for non-zero digit in this grouping
                        if (digits[i - 1] != '0' || (i > 1 && digits[i - 2] != '0'))
                            showThousands = true;
                        else
                            showThousands = false;
                    }

                    // Show "thousands" if non-zero in grouping
                    if (showThousands)
                    {
                        if (column > 0)
                        {
                            s = string.Format("{0}{1}{2}",
                                s,
                                Thousands[column / 3],
                                allZeros ? " " : ", ");
                        }
                        // Indicate non-zero digit encountered
                        allZeros = false;
                    }
                    builder.Insert(0, s);
                    break;

                case 1:        // Tens column
                    if (ndigit > 0)
                    {
                        s = string.Format("{0}{1}",
                            Tens[ndigit],
                            (digits[i + 1] != '0') ? "-" : " ");
                        builder.Insert(0, s);
                    }
                    break;

                case 2:        // Hundreds column
                    if (ndigit > 0)
                    {
                        s = string.Format("{0} hundred ", Ones[ndigit]);
                        builder.Insert(0, s);
                    }
                    break;
            }
        }

        // Trim trailing space
        System.Diagnostics.Debug.Assert(builder.Length > 0 && builder[^1] == ' ');
        builder.Length--;
    }

    private static string RemoveAccent(string value)
    {
        var bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(value);
        return System.Text.Encoding.ASCII.GetString(bytes);
    }

    private static string GenerateSlug(string value, int? maxLength = null)
    {
        // prepare string, remove accents, lower case and convert hyphens to whitespace
        var result = RemoveAccent(value).Replace("-", " ").ToLowerInvariant();

        result = System.Text.RegularExpressions.Regex.Replace(result, @"[^a-z0-9\s-]", string.Empty); // remove invalid characters
        result = System.Text.RegularExpressions.Regex.Replace(result, @"\s+", " ").Trim(); // convert multiple spaces into one space

        if (maxLength.HasValue) // cut and trim
            result = result.Substring(0, result.Length <= maxLength ? result.Length : maxLength.Value).Trim();

        return System.Text.RegularExpressions.Regex.Replace(result, @"\s", "-"); // replace all spaces with hyphens
    }

    private static Dictionary<string, object?> ExtractKeyValues(object keyValues)
    {
        var props = keyValues.GetType().GetTypeInfo().GetProperties(BindingFlags.Public | BindingFlags.Instance);
        return props.ToDictionary(prop => prop.Name, prop => prop.GetValue(keyValues));
    }

    private static string SetCapitalizeFirstCharacter(string s)
    {
        System.Text.StringBuilder builder = new(s);

        if (builder.Length > 0)
            builder[0] = char.ToUpper(builder[0]);

        return builder.ToString();
    }

    private static void SetWordSentenceCase(System.Text.StringBuilder builder, int wordStart, int wordEnd, ref bool inSentence)
    {
        System.Diagnostics.Debug.Assert(wordStart != -1);
        System.Diagnostics.Debug.Assert(wordStart <= wordEnd);
        System.Diagnostics.Debug.Assert(wordEnd <= builder.Length);

        // Set word to lower case if not acronym
        if (IncludesLowerCase(builder, wordStart, wordEnd))
            EachChar(builder, wordStart, wordEnd, c => char.ToLower(c));

        if (!inSentence)
        {
            builder[wordStart] = char.ToUpper(builder[wordStart]);
            inSentence = true;
        }

        if (inSentence && wordEnd < builder.Length /*&& builder.IsEndOfSentenceCharacter(wordEnd)*/)
            inSentence = false;
    }

    private static string SetSentenceCase(string s)
    {
        System.Text.StringBuilder builder = new(s);

        bool inSentence = false;
        int wordStart = -1;
        int i;

        for (i = 0; i < builder.Length; i++)
        {
            // Remove Comment

            //if (s.IsWordCharacter(i))
            //{
            //    if (wordStart == -1)
            //        wordStart = i;
            //}
            //else if (wordStart != -1)
            //{
            //    SetWordSentenceCase(builder, wordStart, i, ref inSentence);
            //    wordStart = -1;
            //}
        }

        if (wordStart != -1)
            SetWordSentenceCase(builder, wordStart, i, ref inSentence);

        return builder.ToString();
    }

    private static string SetTitleCase(string s)
    {
        System.Text.StringBuilder builder = new(s);

        bool inSentence = false;
        int wordStart = -1;
        int i;

        for (i = 0; i < builder.Length; i++)
        {
            // Remove Comment

            //if (s.IsWordCharacter(i))
            //{
            //    if (wordStart == -1)
            //        wordStart = i;
            //}
            //else if (wordStart != -1)
            //{
            //    SetWordTitleCase(builder, wordStart, i, ref inSentence);
            //    wordStart = -1;
            //}
        }

        if (wordStart != -1)
            SetWordTitleCase(builder, wordStart, i, ref inSentence);

        return builder.ToString();
    }

    #endregion

    public static string? After(this String value, string search, bool ignoreCase = false)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (search.IsNullOrEmpty() || search.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(search));
        // ----------------------------------------------------------------------------------------------------
        StringComparison culture = default;
        int index = default;
        string? result = default;
        // ----------------------------------------------------------------------------------------------------
        culture = ignoreCase
                  ? StringComparison.InvariantCultureIgnoreCase
                  : StringComparison.InvariantCulture;

        index = value?.IndexOf(search, culture) ?? -1;
        if (index >= 0)
            result = value?.Substring(index + search.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static string? AfterIgnoreCase(this String input, string search)
    {
        return input.After(search, true);
    }

    public static string? Anagram(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return new string(value.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray()).ToUpper();
    }

    public static string AsBase64Decoded(this String value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        byte[] base64EncodedBytes = System.Convert.FromBase64String(value);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    public static string AsBase64Encoded(this String value)
    {
        if (string.IsNullOrWhiteSpace(value)) return value;
        byte[] plainTextBytes = System.Text.Encoding.UTF8.GetBytes(value);
        return System.Convert.ToBase64String(plainTextBytes);
    }

    public static string AsPrettyJson(this String json)
    {
        if (json.IsEmpty()) return json;

        const string INDENT_STRING = "    ";
        int indentation = 0;
        int quoteCount = 0;

        try
        {
            var result =
                from ch in json
                let quotes = ch == '"' ? quoteCount++ : quoteCount
                let lineBreak = ch == ',' && quotes % 2 == 0 ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(INDENT_STRING, indentation)) : null
                let openChar = ch == '{' || ch == '[' ? ch + Environment.NewLine + string.Concat(Enumerable.Repeat(INDENT_STRING, ++indentation)) : ch.ToString()
                let closeChar = ch == '}' || ch == ']' ? Environment.NewLine + string.Concat(Enumerable.Repeat(INDENT_STRING, --indentation)) + ch : ch.ToString()
                select lineBreak == null
                            ? openChar.Length > 1
                                ? openChar
                                : closeChar
                            : lineBreak;

            return string.Concat(result);
        }
        catch (Exception)
        {
            return json;
        }
    }

    public static string AsPrettyXml(this String xmlString)
    {
        System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
        doc.LoadXml(xmlString);
        return AsPrettyXml(doc);
    }

    public static string AsPrettyXml(this System.Xml.XmlDocument doc)
    {
        StringWriter stringWriter = new StringWriter();

        try
        {
            // Format the XML text.
            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(stringWriter);
            xmlTextWriter.Formatting = System.Xml.Formatting.Indented;
            doc.WriteTo(xmlTextWriter);
            return stringWriter.ToString();
        }
        catch (Exception)
        {
            return string.Empty;
        }
        finally
        {
            stringWriter.Dispose();
        }
    }

    public static string AssembleFromLines(params string[] lines)
    {
        return string.Join("\n", lines);
    }
    
    public static string AssembleFromLines(IEnumerable<string> lines)
    {
        return AssembleFromLines(lines.ToArray());
    }

    public static string AsString(this String value)
    {
        return value.IsEmpty() ? string.Empty : value;
    }

    public static string Before(this String input, string search, bool ignoreCase = false)
    {
        if (!search.IsNullOrEmpty())
        {
            var culture = ignoreCase
                ? StringComparison.InvariantCultureIgnoreCase
                : StringComparison.InvariantCulture;

            int idx = input?.IndexOf(search, culture) ?? -1;
            if (idx >= 0)
            {
                input = input.Substring(0, idx);
            }
        }

        return input;
    }

    public static string BeforeIgnoreCase(this string input, string search)
    {
        return input.Before(search, true);
    }

    public static string Between(this String input, char enclosureCharacter)
    {
        return Between(input, enclosureCharacter, enclosureCharacter);
    }

    public static string Between(this String input, char firstEnclosureCharacter, char secondEnclosureCharacter)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        string result;

        int firstEnclosureCharacterIndex = input.IndexOf(firstEnclosureCharacter);
        if (firstEnclosureCharacterIndex == -1 || firstEnclosureCharacterIndex == input.Length - 1)
        {
            result = null;
        }
        else
        {
            int firstAdjustedIndex = firstEnclosureCharacterIndex + 1;
            int secondEnclosureCharacterIndex = input.IndexOf(secondEnclosureCharacter, firstAdjustedIndex);
            if (secondEnclosureCharacterIndex == -1)
            {
                result = null;
            }
            else
            {
                int length = secondEnclosureCharacterIndex - firstAdjustedIndex;

                result = input.Substring(firstAdjustedIndex, length);
            }
        }

        return result;
    }

    public static string Between(this String input, string enclosure)
    {
        return Between(input, enclosure, StringComparison.Ordinal);
    }

    public static string Between(this String input, string enclosure, StringComparison comparisonType)
    {
        return Between(input, enclosure, enclosure, comparisonType);
    }

    public static string Between(this String input, string firstEnclosure, string secondEnclosure, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (firstEnclosure == null)
            throw new ArgumentNullException("firstEnclosure");
        if (secondEnclosure == null)
            throw new ArgumentNullException("secondEnclosure");

        string result;

        int firstEnclosureIndex = input.IndexOf(firstEnclosure, comparisonType);
        if (firstEnclosureIndex == -1 || firstEnclosureIndex + firstEnclosure.Length == input.Length)
        {
            result = null;
        }
        else
        {
            int firstAdjustedIndex = firstEnclosureIndex + firstEnclosure.Length;
            int secondEnclosureIndex = input.IndexOf(secondEnclosure, firstAdjustedIndex, comparisonType);
            if (secondEnclosureIndex == -1)
            {
                result = null;
            }
            else
            {
                int length = secondEnclosureIndex - firstAdjustedIndex;

                result = input.Substring(firstAdjustedIndex, length);
            }
        }

        return result;
    }

    public static string CamelcaseEveryChar(this String message)
    {
        System.Text.StringBuilder result = new();
        int counter = 0;
        foreach (char character in message)
        {
            result.Append(counter % 2 == 0 ? character.ToString().ToUpper() : character.ToString().ToLower());
            counter++;
        }
        return result.ToString();
    }

    public static string CamelcaseEveryLetter(this String message)
    {
        string[] splittedMessage = message.Split(' ');
        System.Text.StringBuilder result = new();

        foreach (string word in splittedMessage)
        {
            for (int i = 0; i < word.Length; i++)
                result.Append(i % 2 == 0 ? word[i].ToString().ToUpper() : word[i].ToString().ToLower());

            result.Append(' ');
        }
        result.Remove(result.Length - 1, 1);
        return result.ToString();
    }

    public static string CamelCaseToHumanCase(this String self)
    {
        IEnumerable<string> words = self.SplitCamelCase();
        string humanCased = string.Join(" ", words);
        return humanCased;
    }

    public static string CapitaliseFirstLetter(this String input)
    {
        return input.Substring(0, 1).ToUpper() + input.Substring(1);
    }

    public static string Capitalize1(this String stringValue)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder(stringValue);
        result[0] = char.ToUpper(result[0]);
        for (int i = 1; i < result.Length; ++i)
        {
            if (char.IsWhiteSpace(result[i - 1]) && !char.IsWhiteSpace(result[i]))
                result[i] = char.ToUpper(result[i]);
        }
        return result.ToString();
    }

    public static string Capitalize2(this String s)
    {
        if (s.Length == 0)
        {
            return s;
        }
        return s.Substring(0, 1).ToUpper() + s.Substring(1).ToLower();
    }

    public static string CapitalizeAfterCharacter(this String value, char separator, bool minimizeOtherChar = false)
    {
        return CapitalizeAfterCharacters(value, new[] { separator }, minimizeOtherChar);
    }

    public static string CapitalizeAfterCharacters(this String value, IEnumerable<char> separators, bool minimizeOtherChar = false)
    {
        var stringToCapitalize = value.ToCharArray();

        if (string.IsNullOrEmpty(value))
            return value;
        else if (!separators.Any() || !stringToCapitalize.Intersect(separators).Any())
        {
            var firstChar = value.Substring(0, 1).ToUpper();
            var otherChars = value.Substring(1, value.Length - 1);
            if (minimizeOtherChar)
                otherChars = otherChars.ToLower();
            return firstChar + otherChars;
        }
        else
        {
            var stringCapitalized = new System.Text.StringBuilder();
            var lastCharacter = separators.First();
            foreach (var character in stringToCapitalize)
            {

                if (separators.Contains(lastCharacter))
                    stringCapitalized.Append(char.ToUpper(character));
                else
                    stringCapitalized.Append(minimizeOtherChar ? char.ToLower(character) : character);

                lastCharacter = character;
            }
            return stringCapitalized.ToString();
        }
    }

    public static string CapitalizeEachLine(this String value, bool minimizeOtherChar = false)
    {
        var sentencesTrimmed = value.Split('\n').Select(sentence => sentence.Trim());
        var textTrimed = string.Join('\n', sentencesTrimmed);
        return textTrimed.CapitalizeAfterCharacter('\n', minimizeOtherChar);
    }

    public static string CapitalizeEachWord(this String value, bool minimizeOtherChar = false)
    {
        return value.CapitalizeAfterCharacters(new char[] { ' ', '\t', '\n', '\r' }, minimizeOtherChar);
    }

    public static string CapitalizeFirstLetter(this String text)
    {
        if (text == null) return null;
        if (text == string.Empty) return text;
        return text.GetFirst().ToString().ToUpper() + text.Substring(1);
    }

    public static string CapitalizeFirstLetters(this String message)
    {
        string[] splittedMessage = message.Split(' ');
        System.Text.StringBuilder result = new();
        foreach (string word in splittedMessage)
        {
            string firstLetter = word[0].ToString().ToUpper();
            //string otherLetters = word.Substring(1, word.Length - 1); // (Old Version)
            string otherLetters = word[1..]; // (New Version)

            result.Append(firstLetter + otherLetters + " ");
        }
        result.Remove(result.Length - 1, 1);
        return result.ToString();
    }

    public static string CommonPathPrefix(string s1, string s2)
    {
        int _, __;
        return CommonPathPrefix(s1, s2, out _, out __);
    }
    
    public static string CommonPathPrefix(string s1, string s2, out int suffix1Length, out int suffix2Length)
    {
        string[] splits1 = s1.Split('/');
        string[] splits2 = s2.Split('/');
        int count1 = splits1.Count();
        int count2 = splits2.Count();
        int count = Math.Max(count1, count2);
        int nMatching = 0;
        for (int i = 0; i < count; i++)
        {
            if (splits1[i] == splits2[i])
            {
                nMatching++;
            }
            else
            {
                break;
            }
        }
        suffix1Length = count1 - nMatching;
        suffix2Length = count2 - nMatching;
        string r = string.Join("/", splits1.Take(nMatching));
        return r;
    }

    public static string ChildUrl(this String url)
    {
        return url.Split('/').Skip(1).Join("/");
    }

    public static string CleanWhiteSpace1(this String text)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return text.Trim().ReplaceWhitespace(" ");
    }

    public static string CleanWhiteSpace2(this String input)
    {
        var stringWithSpaces = new string(input.Trim().ToCharArray()
                               .Select(c => char.IsWhiteSpace(c) ? ' ' : c)
                               .ToArray());
        while (stringWithSpaces.Contains("  "))
            stringWithSpaces = stringWithSpaces.Replace("  ", " ");
        return stringWithSpaces;
    }

    public static string ClearPrefix(this String source, string prefix)
    {
        return source.StartsWith(prefix) ? source.Substring(prefix.Length) : source;
    }

    public static string CombineToPath(this String path, string root)
    {
        if (Path.IsPathRooted(path)) return path;

        return Path.Combine(root, path);
    }

    public static string Convert(this String input, System.Text.Encoding targetEncoding)
    {
        return Convert(input, targetEncoding, System.Text.Encoding.Unicode);
    }

    public static string Convert(this String input, System.Text.Encoding targetEncoding, System.Text.Encoding sourceEncoding)
    {
        if (input == null)
            throw new ArgumentNullException("input");
        if (targetEncoding == null)
            throw new ArgumentNullException("targetEncoding");
        if (sourceEncoding == null)
            throw new ArgumentNullException("sourceEncoding");

        byte[] sourceBytes = sourceEncoding.GetBytes(input);

        byte[] targetBytes = System.Text.Encoding.Convert(sourceEncoding, targetEncoding, sourceBytes);

        string result = targetEncoding.GetString(targetBytes, 0, targetBytes.Length);

        return result;
    }

    public static string ConvertCRLFToBreaks(this String plainText)
    {
        return new System.Text.RegularExpressions.Regex("(\r\n|\n)").Replace(plainText, "<br/>");
    }

    public static string ConvertPascalOrCamelCaseToWords(this String input)
    {
        try
        {
            return ConvertToWords(input);
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static string ConvertPascalOrCamelCaseToWords(this String input, WordCase wordCase)
    {
        try
        {
            string output = ConvertToWords(input);
            switch (wordCase)
            {
                case WordCase.AllLower:
                    output = output.ToLower();
                    break;
                case WordCase.AllUpper:
                    output = output.ToUpper();
                    break;
                case WordCase.Title:
                    output = output.ToTitleCase3();
                    break;
                case WordCase.Sentence:
                    output = output.ToSentenceCase();
                    break;
                default:
                    break;
            }
            return output;
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static string ConvertToCamelCase(this String input)
    {
        try
        {
            input.ToTitleCase3();
            input = input.Remove(1).ToLower() + input.Substring(1);
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s+", string.Empty).Trim();
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static string ConvertToPascalCase(this String input)
    {
        try
        {
            input.ToTitleCase3();
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s+", string.Empty).Trim();
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static string Decrypt(this String stringToDecrypt, string key)
    {
        var cspParamters = new System.Security.Cryptography.CspParameters { KeyContainerName = key };
        var rsaServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider(cspParamters) { PersistKeyInCsp = true };
        string[] decryptArray = stringToDecrypt.Split(new[] { "-" }, StringSplitOptions.None);
        byte[] decryptByteArray = Array.ConvertAll(decryptArray,
            (s => System.Convert.ToByte(byte.Parse(s, System.Globalization.NumberStyles.HexNumber))));
        byte[] bytes = rsaServiceProvider.Decrypt(decryptByteArray, true);
        string result = System.Text.Encoding.UTF8.GetString(bytes);
        return result;
    }

    public static string DefaultIfEmpty(this String source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return source == "" ? new string('\0', 1) : source;
    }

    public static string DeleteExtension(this String source, char Delimeter = '.')
    {
        if (source.IndexOf(Delimeter) < 0)
            return source;
        return source.Substring(0, source.LastIndexOf(Delimeter));
    }

    public static string? DirectoryPath(this String path)
    {
        return Path.GetDirectoryName(path);
    }

    public static string Distinct(this String s, bool ignoreCase = false)
    {
        System.Text.StringBuilder builder = new(s.Length);
        HashSet<char> hashSet = new(s.Length, Strings.Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));

        foreach (char c in s)
        {
            if (hashSet.Add(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    public static string EmptyIfNull(this string? s)
    {
        return s ?? string.Empty;
    }

    public static string EmptyIfNullOrWhiteSpace(this string? s)
    {
        return string.IsNullOrWhiteSpace(s) ? string.Empty : s;
    }

    public static string EncodeJson(this String value)
    {
        return string.Concat
        ("\"",
            value.Replace("\\", "\\\\").Replace("\"", "\\\"").Replace("\r", "").Replace("\n", "\\n"),
            "\""
        );
    }

    public static string EncodeXml(this String value)
    {
        return value.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;");
    }

    public static string Encrypt(this String stringToEncrypt, string key)
    {
        var cspParameter = new System.Security.Cryptography.CspParameters { KeyContainerName = key };
        var rsaServiceProvider = new System.Security.Cryptography.RSACryptoServiceProvider(cspParameter) { PersistKeyInCsp = true };
        byte[] bytes = rsaServiceProvider.Encrypt(System.Text.Encoding.UTF8.GetBytes(stringToEncrypt), true);
        return BitConverter.ToString(bytes);
    }

    public static string EveryLetterLower(this String str)
    {
        return string.Join(" ", str.Split(' ').Select(x => x.FirstLetterLower()));
    }

    public static string EveryLetterUpper(this String str)
    {
        return string.Join(" ", str.Split(' ').Select(x => x.FirstLetterUpper()));
    }

    public static string EveryWordUpper(this String str)
    {
        return string.Join(" ", str.Split(' ').Select(x => x.FirstWordUpper()));
    }

    public static string Except(this String s, IEnumerable<char> exceptChars, bool ignoreCase = false)
    {
        System.Text.StringBuilder builder = new(s.Length);
        HashSet<char> hashSet = new(exceptChars, Strings.Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));

        foreach (char c in s)
        {
            if (!hashSet.Contains(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    public static string Except(this String s, string exceptChars, bool ignoreCase = false)
    {
        return Except(s, (IEnumerable<char>)exceptChars, ignoreCase);
    }

    public static string Excerpt(this String input, int characters)
    {
        if (input.Length <= characters)
            return input;
        else return input.Substring(0, characters) + "...";
    }

    public static string FileEscape(this String file)
    {
        return "\"{0}\"".ToFormat(file);
    }

    public static string Filter(this String s, Func<char, bool> predicate)
    {
        System.Text.StringBuilder builder = new(s.Length);

        foreach (char c in s)
        {
            if (predicate(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    public static string First(this String str, params char[] split)
    {
        return str.Split(split).FirstOrDefault();
    }

    public static string First(this String str, params string[] split)
    {
        return str.Split(split, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
    }

    public static string FirstCharacter(this String val)
    {
        return (!string.IsNullOrEmpty(val))
            ? (val.Length >= 1)
                ? val.Substring(0, 1)
                : val
            : null;
    }

    public static string FirstLetterLower(this String str)
    {
        return str.FirstOrDefault().ToLower() + str.Substring(1);
    }

    public static string FirstLetterUpper(this String str)
    {
        return str.FirstOrDefault().ToUpper() + str.Substring(1);
    }

    public static string FirstNonempty(string s1, string s2 = null, string s3 = null, string s4 = null)
    {
        string r = s1;
        if (string.IsNullOrEmpty(r))
        {
            r = s2;
            if (string.IsNullOrEmpty(r))
            {
                r = s3;
                if (string.IsNullOrEmpty(r))
                {
                    r = s4;
                }
            }
        }
        return r;
    }

    public static string FirstWord(this String str)
    {
        return str.First(' ');
    }

    public static string FirstWordUpper(this String str)
    {
        return str.FirstOrDefault().ToUpper() + str.Substring(1).ToLower();
    }

    public static string Format(this String value, object arg0)
    {
        return string.Format(value, arg0);
    }

    public static string Format(this String value, params object[] args)
    {
        return string.Format(value, args);
    }

    public static string FormatWith(this String format, params object[] args)
    {
        return string.Format(format, args);
    }

    public static string FromNumber(float value, AT.Enums.DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

    public static string FromNumber(double value, AT.Enums.DecimalFormat decimalFormat) => FromNumber((decimal)value, decimalFormat);

    public static string FromNumber(decimal value, AT.Enums.DecimalFormat decimalFormat)
    {
        bool isNegative = false;
        if (value < 0m)
        {
            value = Math.Abs(value);
            isNegative = true;
        }

        string integerPart = value.ToString();
        int i = integerPart.IndexOf('.');
        if (i >= 0)
            integerPart = integerPart[0..i];
        decimal @decimal = value - Math.Truncate(value);

        System.Text.StringBuilder builder = new();
        FormatNumber(builder, integerPart);
        if (isNegative)
            builder.Insert(0, NegativePrefix);

        // Handle fractional portion
        switch (decimalFormat)
        {
            case AT.Enums.DecimalFormat.Currency:
                builder.AppendFormat(" and {0:00}/100", @decimal * 100);
                break;
            case AT.Enums.DecimalFormat.Fraction:
                if (@decimal != 0)
                {
                    builder.Append(" and ");
                    builder.Append(DecimalToFraction(@decimal));
                }
                break;
            default:
                break;
        }

        return builder.ToString();
    }

    public static string FromNumber(int value) => FromNumber((long)value);

    public static string FromNumber(long value)
    {
        bool isNegative = false;
        if (value < 0)
        {
            value = Math.Abs(value);
            isNegative = true;
        }

        System.Text.StringBuilder builder = new();
        FormatNumber(builder, value.ToString());
        if (isNegative)
            builder.Insert(0, NegativePrefix);
        return builder.ToString();
    }

    public static string FromNumber(int value, out bool isNegative) => FromNumber((long)value, out isNegative);

    public static string FromNumber(long value, out bool isNegative)
    {
        if (value < 0)
        {
            value = Math.Abs(value);
            isNegative = true;
        }
        else isNegative = false;

        System.Text.StringBuilder builder = new();
        FormatNumber(builder, value.ToString());
        return builder.ToString();
    }

    public static string GetArticle(this String s)
    {
        if (string.IsNullOrWhiteSpace(s))
        {
            return "";
        }
        else if (s.ToLowerInvariant().Trim()[0].IsVowel() == true)
        {
            return "an";
        }
        else
        {
            return "a";
        }
    }

    public static string GetDefaultIfEmpty(this String myValue, string defaultValue)
    {
        if (!String.IsNullOrEmpty(myValue))
        {
            myValue = myValue.Trim();
            return myValue.Length > 0 ? myValue : defaultValue;
        }
        return defaultValue;
    }

    public static string GetDigits(this String value)
    {
        return new string(value.Where(c => char.IsDigit(c)).ToArray());
    }

    public static string GetEmptyStringIfNull(this String val)
    {
        return (val != null ? val.Trim() : "");
    }

    public static string GetExtension(this String source, char Delimeter = '.')
    {
        //return source.Substring(source.LastIndexOf(Delimeter) + 1);
        return source[(source.LastIndexOf(Delimeter) + 1)..];
    }

    public static string GetFirstCharactersAfterSeparators(this String value, IEnumerable<char> separators)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        // String made of separator characters
        if (!value.ToCharArray().Except(separators).Any())
            return string.Empty;

        if (!separators.Any())
            return value.GetFirst().ToString();

        var words = value.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        var firstCharacters = words.Select(word => word[0]);
        return string.Join("", firstCharacters);
    }

    public static string GetFirstLetterOfEachWord(this String value)
    {
        var separators = new[] { ' ', '\t', '\n', '\r' };
        return GetFirstLettersAfterSeparators(value, separators);
    }

    public static string GetFirstLettersAfterSeparators(this String value, IEnumerable<char> separators)
    {
        if (string.IsNullOrEmpty(value))
            return value;

        // String made of separator characters
        if (!value.ToCharArray().Except(separators).Any())
            return string.Empty;

        if (!separators.Any())
            return value.FirstLetter()?.ToString() ?? string.Empty;

        var firstLetters = value.Split(separators.ToArray())
                                .Select(word => word.FirstLetter());
        return string.Join("", firstLetters);
    }

    public static string GetFirstWord(this String text)
    {
        if (text == null) return null;
        return new System.Text.RegularExpressions.Regex(@"([^\s]+)").Match(text).Value;
    }

    public static string GetLetterOrDigit(this String value)
    {
        return new string(value.Where(c => char.IsLetterOrDigit(c)).ToArray());
    }

    public static string GetLetters(this String value)
    {
        return new string(value.Where(c => char.IsLetter(c)).ToArray());
    }

    public static string GetLettersAndSpaces(this String value)
    {
        return new string(value.Where(c => char.IsLetter(c) || char.IsWhiteSpace(c)).ToArray());
    }

    public static string? GetNextToken(this String s, Func<char, bool> predicate, ref int pos)
    {
        // Skip delimiters
        while (pos < s.Length && predicate(s[pos]))
            pos++;

        // Parse token
        int start = pos;
        while (pos < s.Length && !predicate(s[pos]))
            pos++;

        // Extract token
        if (pos > start)
            return s[start..pos];

        return null;
    }

    public static string? GetNextToken(this String s, string delimiterChars, ref int pos, bool ignoreCase = false)
    {
        HashSet<char> hashSet = new(delimiterChars, Strings.Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));
        return s.GetNextToken(hashSet.Contains, ref pos);
    }

    public static string GetNullIfEmptyString(this String myValue)
    {
        if (myValue == null || myValue.Length <= 0)
        {
            return null;
        }
        myValue = myValue.Trim();
        if (myValue.Length > 0)
        {
            return myValue;
        }
        return null;
    }

    public static string GetNumbers(this string text)
    {
        return new string(text?.Where(c => char.IsDigit(c)).ToArray());
    }

    public static string GetSafeString(this String value)
    {
        if (string.IsNullOrWhiteSpace(value)) return "";
        byte[] bytes = System.Text.Encoding.Default.GetBytes(value);
        value = System.Text.Encoding.UTF8.GetString(bytes);
        return new string(value.Where(c => char.IsLetterOrDigit(c) || char.IsWhiteSpace(c) || char.IsPunctuation(c) || char.IsControl(c)).ToArray());
    }

    public static string IndentEachLine(this String s, string prefix = "  ")
    {
        string r;
        if (s == null)
        {
            r = null;
        }
        else
        {
            string[] split = s.SplitIntoLines();
            r = "";
            bool first = true;
            foreach (string line in split)
            {
                if (first)
                {
                    first = false;
                }
                else
                {
                    r += Environment.NewLine;
                }
                r += (prefix + split);
            }
        }
        return r;
    }

    public static string Intersect(this String s, IEnumerable<char> intersectChars, bool ignoreCase = false)
    {
        System.Text.StringBuilder builder = new(s.Length);
        HashSet<char> hashSet = new(s.Length, Strings.Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));

        foreach (char c in intersectChars)
        {
            hashSet.Add(c);
        }

        foreach (char c in s)
        {
            if (hashSet.Remove(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    public static string Intersect(this String s, string intersectChars, bool ignoreCase = false)
    {
        return Intersect(s, (IEnumerable<char>)intersectChars, ignoreCase);
    }

    public static string Join(this string[] values, string separator)
    {
        return string.Join(separator, values);
    }

    public static string Join(this IEnumerable<string> values, string separator)
    {
        return Join(values.ToArray(), separator);
    }

    public static string JoinUpLast(this String source, string stringToJoin)
    {
        var newString = new System.Text.StringBuilder(source);
        newString.Append(stringToJoin);

        return newString.ToString();
    }

    public static string JoinUpStart(this String source, string stringToJoin)
    {
        var newString = new System.Text.StringBuilder(stringToJoin);
        newString.Append(source);

        return newString.ToString();
    }

    public static string KeepDigitsOnly(this String value)
    {
        if (string.IsNullOrEmpty(value))
            return string.Empty;

        return System.Text.RegularExpressions.Regex.Replace(value, @"[^0-9]", string.Empty);
    }

    public static string KeepLettersOnly(this String value, bool withAccentedLetters = true)
    {
        if (withAccentedLetters)
            return System.Text.RegularExpressions.Regex.Replace(value, @"[^\w]*[0-9_]*", string.Empty);
        else
            return System.Text.RegularExpressions.Regex.Replace(value, "[^0a-zA-Z]", string.Empty);
    }

    public static string KeepLettersOrDigitsOnly(this String value, bool withAccentedLetters = true)
    {
        if (withAccentedLetters)
            return System.Text.RegularExpressions.Regex.Replace(value, @"[^\w]*[_]*", string.Empty);
        else
            return System.Text.RegularExpressions.Regex.Replace(value, "[^0-9a-zA-Z]", string.Empty);
    }

    public static string Last(this String str, params char[] split)
    {
        return str.Split(split).LastOrDefault();
    }

    public static string Last(this String str, params string[] split)
    {
        return str.Split(split, StringSplitOptions.RemoveEmptyEntries).LastOrDefault();
    }

    public static string LastCharacter(this String val)
    {
        return (!string.IsNullOrEmpty(val))
            ? (val.Length >= 1)
                ? val.Substring(val.Length - 1, 1)
                : val
            : null;
    }

    public static string LastWord(this String str)
    {
        return str.Last(' ');
    }

    public static string Left(this String source, int length)
    {
        if (source.Length <= length)
            return source;
        else
            //return source.Substring(0, length); // [OLD Version]
            return source[..length]; // (New Version)
    }

    public static string? Left(this string? value, int length, bool withEllipsis = false)
    {
        // TODO manage bad length
        if (value == null)
            return null;
        else if (value.Length <= length)
            return value;
        else
        {
            if (withEllipsis)
                return value.Substring(0, length - 1) + ".";
            else
                return value.Substring(0, length);
        }
    }

    public static string Left2(this String input, int length)
    {
        if (input == null)
            throw new ArgumentNullException("input");
        if (length < 0 || length > input.Length)
            throw new ArgumentOutOfRangeException("length", "length cannot be higher than the amount of available characters in the input or lower than 0");

        string result = input.Substring(0, length);

        return result;
    }

    public static string Left3(this String val, int length)
    {
        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException("val");
        }
        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException("length",
                "length cannot be higher than total string length or less than 0");
        }
        return val.Substring(0, length);
    }

    public static string Left4(this String text, int size)
    {
        if (text == null) return null;
        if (size > text.Length) return text;
        return text.Substring(0, size);
    }

    public static string Left5(this String input, int length)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("The input string is null or empty.", nameof(input));

        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");

        if (length > input.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be smaller or equal to the length of the input string.");

        return input.Substring(0, length);
    }

    public static string LeftOf(this String input, char character)
    {
        return LeftOf(input, character, 0);
    }

    public static string LeftOf(this String input, char character, int skip)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (skip < 0)
            throw new ArgumentOutOfRangeException("skip", "skip should be larger or equal to 0");

        string result;

        if (input.Length == 0)
        {
            result = input;
        }
        else
        {
            int characterPosition = 0;
            int charactersFound = -1;

            while (charactersFound < skip)
            {
                characterPosition = input.IndexOf(character, characterPosition + 1);
                if (characterPosition == -1)
                {
                    break;
                }
                else
                {
                    charactersFound++;
                }
            }

            if (characterPosition == -1)
            {
                result = input;
            }
            else
            {
                result = input.Substring(0, characterPosition);
            }
        }

        return result;
    }

    public static string LeftOf(this String input, string value)
    {
        return LeftOf(input, value, StringComparison.Ordinal);
    }

    public static string LeftOf(this String input, string value, StringComparison comparisonType)
    {
        return LeftOf(input, value, 0, comparisonType);
    }

    public static string LeftOf(this String input, string value, int skip, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (value == null)
            throw new ArgumentNullException("value");
        if (skip < 0)
            throw new ArgumentOutOfRangeException("skip", "skip should be larger or equal to 0");

        string result;

        if (input.Length <= skip)
        {
            result = input;
        }
        else
        {
            int valuePosition = 0;
            int valuesFound = -1;

            while (valuesFound < skip)
            {
                valuePosition = input.IndexOf(value, valuePosition + 1, comparisonType);
                if (valuePosition == -1)
                {
                    break;
                }
                else
                {
                    valuesFound++;
                }
            }

            if (valuePosition == -1)
            {
                result = input;
            }
            else
            {
                result = input.Substring(0, valuePosition);
            }
        }

        return result;
    }

    public static string LeftOfLast(this String input, char character)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        string result;
        int lastCharacterPosition = input.LastIndexOf(character);

        if (lastCharacterPosition == -1)
        {
            result = input;
        }
        else
        {
            result = input.Substring(0, lastCharacterPosition);
        }

        return result;
    }

    public static string LeftOfLast(this String input, string value)
    {
        return LeftOfLast(input, value, StringComparison.Ordinal);
    }

    public static string LeftOfLast(this String input, string value, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (value == null)
            throw new ArgumentNullException("value");

        string result;
        int lastValuePosition = input.LastIndexOf(value, comparisonType);

        if (lastValuePosition == -1)
        {
            result = input;
        }
        else
        {
            result = input.Substring(0, lastValuePosition);
        }

        return result;
    }

    public static string Limit(this String source, int maxLength, string suffix = null)
    {
        if (suffix.IsNotNullOrEmpty())
        {
            maxLength = maxLength - suffix.Length;
        }

        if (source.Length <= maxLength)
        {
            return source;
        }

        return string.Concat(source.Substring(0, maxLength).Trim(), suffix ?? string.Empty);
    }

    public static string LimitLength(this String source, int maxLength)
    {
        if (source.Length <= maxLength)
            return source;
        return source.Substring(0, maxLength);
    }

    public static string LimitSentenceLength(this String paragraph, int maximumLenght)
    {
        //null check
        if (paragraph == null) return null;

        //less than maximum length, return as it is
        if (paragraph.Length <= maximumLenght) return paragraph;

        //split the paragraph into indvidual words
        string[] words = paragraph.Split(' ');
        //initialize return variable
        string paragraphToReturn = string.Empty;

        //construct the return word 
        foreach (string word in words)
        {
            //check if adding 3 to current length and next word is more than maximum length. Constant 3 is for "..." at the end
            if ((paragraphToReturn.Length + word.Length + 3) > maximumLenght)
            {
                //append "..."
                paragraphToReturn = paragraphToReturn.Trim() + "...";
                //exit foreach loop
                break;
            }
            //add next word and continue
            paragraphToReturn += word + " ";
        }
        return paragraphToReturn;
    }

    public static string MaxLengthTrim(this String str, int maxLength)
    {
        if (str == null)
            return str;
        if (str.Length > maxLength)
            return str.Substring(0, maxLength - 1);
        return str;
    }

    public static string Mid(this String source, int startIndex)
    {
        return Mid(source, startIndex, int.MaxValue);
    }

    public static string Mid(this String source, int startIndex, int length)
    {
        if (source.Length <= startIndex)
            return string.Empty;
        else if (length == int.MaxValue || source.Length <= startIndex + length)
            //return source.Substring(startIndex); // [OLD Version]
            return source[startIndex..]; // [New Version]
        else
            return source.Substring(startIndex, length);
    }

    public static string MoveUp(this String relativeUrl)
    {
        if (relativeUrl.IsEmpty()) return relativeUrl;

        var segments = relativeUrl.Split('/');
        if (segments.Count() == 1) return string.Empty;

        return segments.Skip(1).Join("/");
    }

    public static string NormalizeWhiteSpace(this String s)
    {
        bool wasSpace = false;

        System.Text.StringBuilder builder = new(s.Length);

        foreach (char c in s)
        {
            if (char.IsWhiteSpace(c))
            {
                if (builder.Length > 0)
                    wasSpace = true;
            }
            else
            {
                if (wasSpace)
                    builder.Append(' ');
                builder.Append(c);
                wasSpace = false;
            }
        }

        return builder.ToString();
    }

    public static string? NullIfEmpty(this string? s)
    {
        return string.IsNullOrEmpty(s) ? null : s;
    }

    public static string NullIfEmpty2(this String value)
    {
        if (value == string.Empty)
            return null;

        return value;
    }

    public static string? NullIfEmptyOrWhiteSpace(this string? s)
    {
        return string.IsNullOrWhiteSpace(s) ? null : s;
    }

    public static string? OrNullIfEmpty(this string? value)
    {
        if (string.IsNullOrEmpty(value))
            return null;
        else
            return value;
    }

    public static string? OrNullIfWhiteSpace(this string? value)
    {
        if (string.IsNullOrWhiteSpace(value))
            return null;
        else
            return value;
    }

    public static string? ParentDirectory(this String path)
    {
        return Path.GetDirectoryName(path.TrimEnd(Path.DirectorySeparatorChar));
    }

    public static string ParentUrl(this String url)
    {
        url = url.Trim('/');
        return url.Contains("/") ? url.Split('/').Reverse().Skip(1).Reverse().Join("/") : string.Empty;
    }

    public static string ParseStringToCsv(this String val)
    {
        return '"' + GetEmptyStringIfNull(val).Replace("\"", "\"\"") + '"';
    }

    public static string PascalToSpacedString(this String input)
    {
        const string pascalCaseSplit = @"([a-z](?=[A-Z])|[A-Z](?=[A-Z][a-z]))";
        return System.Text.RegularExpressions.Regex.Replace(input, pascalCaseSplit, "$1 ");
    }

    public static string PathRelativeTo(this String path, string root)
    {
        var pathParts = path.GetPathParts();
        var rootParts = root.GetPathParts();

        var length = pathParts.Count > rootParts.Count ? rootParts.Count : pathParts.Count;
        for (int i = 0; i < length; i++)
        {
            if (pathParts.First() == rootParts.First())
            {
                pathParts.RemoveAt(0);
                rootParts.RemoveAt(0);
            }
            else
            {
                break;
            }
        }

        for (int i = 0; i < rootParts.Count; i++)
        {
            pathParts.Insert(0, "..");
        }

        return pathParts.Count > 0 ? Path.Combine(pathParts.ToArray()) : string.Empty;
    }

    public static string PluralString(this String noun, int n, bool includeN)
    {
        if (n == 1)
        {
            if (includeN)
            {
                return "1 " + noun;
            }
            else
            {
                return noun;
            }
        }
        else
        {
            return n + " " + noun + "s";
        }
    }

    public static string RegexReplace(this String input, string pattern, Func<string, string> replaceCallback)
    {
        return input.RegexReplace
        (
            pattern,
            InvariantCultureIgnoreCaseRegexOptions,
            replaceCallback
        );
    }

    public static string RegexReplace(this String input, string pattern, Func<int, string, string> replaceCallback)
    {
        return input.RegexReplace
        (
            pattern,
            InvariantCultureIgnoreCaseRegexOptions,
            replaceCallback
        );
    }

    public static string RegexReplace(this String input, string pattern, System.Text.RegularExpressions.RegexOptions options, Func<string, string> replaceCallback)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }
        if (replaceCallback is null) { throw new ArgumentNullException(nameof(replaceCallback)); }

        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? string.Empty,
            me => replaceCallback(me.Value),
            options
        );
    }

    public static string RegexReplace(this String input, string pattern, System.Text.RegularExpressions.RegexOptions options, Func<int, string, string> replaceCallback)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }
        if (replaceCallback is null) { throw new ArgumentNullException(nameof(replaceCallback)); }

        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? string.Empty,
            me => replaceCallback(me.Index, me.Value),
            options
        );
    }

    public static string RegexReplace(this String input, string pattern, string newValue, System.Text.RegularExpressions.RegexOptions? options = null)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }

        return System.Text.RegularExpressions.Regex.Replace
        (
            input,
            pattern ?? string.Empty,
            newValue ?? string.Empty,
            options ?? InvariantCultureIgnoreCaseRegexOptions
        );
    }

    public static string RemoveAccents(this String text)
    {
        var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
        var stringBuilder = new System.Text.StringBuilder(capacity: normalizedString.Length);

        for (int i = 0; i < normalizedString.Length; i++)
        {
            char c = normalizedString[i];
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
            {
                stringBuilder.Append(c);
            }
        }

        return stringBuilder
            .ToString()
            .Normalize(System.Text.NormalizationForm.FormC);

    }

    public static string RemoveAllWhitespace(this String input)
    {
        return new string(input.ToCharArray().Where(c => !Char.IsWhiteSpace(c)).ToArray());
    }

    public static string RemoveCharacters(this String input, List<char> chars)
    {
        string result = input;
        foreach (var c in chars)
        {
            result = result.Replace(c.ToString(), string.Empty);
        }

        return result;
    }

    public static string RemoveCharactersInString(this String str, string removeUs)
    {
        string r = str;
        if (removeUs != null)
        {
            char[] chars = removeUs.ToCharArray();
            foreach (char c in chars)
            {
                r = r.Replace(c.ToString(), "");
            }
        }
        return r;
    }

    public static string RemoveChars(this String s, params char[] chars)
    {
        var sb = new System.Text.StringBuilder(s.Length);
        foreach (char c in s.Where(c => !chars.Contains(c)))
        {
            sb.Append(c);
        }
        return sb.ToString();
    }

    public static string RemoveDiacritical(this String source)
    {
        return System.Text.Encoding.UTF8.GetString(System.Text.Encoding.GetEncoding("ISO-8859-7").GetBytes(source));
    }

    public static string RemoveDiacritics(this String text)
    {
        if (text == null) return null;
        string normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        foreach (char c in normalizedString)
        {
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(c);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark) stringBuilder.Append(c);
        }

        return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    public static string RemoveDiacritics2(this String text)
    {
        return string.Concat(
            text.Normalize(System.Text.NormalizationForm.FormD)
            .Where(ch => System.Globalization.CharUnicodeInfo.GetUnicodeCategory(ch) !=
                                          System.Globalization.UnicodeCategory.NonSpacingMark)
          ).Normalize(System.Text.NormalizationForm.FormC);
    }

    public static string RemoveDiacritics3(this String text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var normalizedString = text.Normalize(System.Text.NormalizationForm.FormD);
        var stringBuilder = new System.Text.StringBuilder();

        foreach (var value in normalizedString)
        {
            var unicodeCategory = System.Globalization.CharUnicodeInfo.GetUnicodeCategory(value);
            if (unicodeCategory != System.Globalization.UnicodeCategory.NonSpacingMark)
                stringBuilder.Append(value);
        }

        return stringBuilder.ToString().Normalize(System.Text.NormalizationForm.FormC);
    }

    public static string RemoveEmptyLines(this String value)
    {
        return System.Text.RegularExpressions.Regex.Replace(value, @"^\s*$[\r\n]*", string.Empty, System.Text.RegularExpressions.RegexOptions.Multiline).Trim('\r', '\n');
    }

    public static string RemoveExcessWhiteSpace(this String input)
    {
        try
        {
            return System.Text.RegularExpressions.Regex.Replace(input, @"\s+", " ").Trim();
        }
        catch (Exception ex)
        {
            Exception except = new Exception(string.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", MethodBase.GetCurrentMethod().DeclaringType.FullName, MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static string RemoveLastOccuranceOf(this String input, string toMatch)
    {
        if (input.Contains(toMatch))
        {
            int lastIndex = input.LastIndexOf(toMatch);
            return input.Substring(0, lastIndex);
        }
        else return input;
    }

    public static string RemoveLastOccuranceOf(this String input, char toMatch)
    {
        if (input.Contains(toMatch))
        {
            int lastIndex = input.LastIndexOf(toMatch);
            return input.Substring(0, lastIndex);
        }
        else return input;
    }

    public static string RemoveLines(this String s, Predicate<string> remove)
    {
        string r = s;
        if (remove != null)
        {
            List<string> lines = s.SplitIntoLines().ToList();
            for (int i = lines.Count - 1; i >= 0; i--)
            {
                string line = lines[i];
                if (remove(line))
                {
                    lines.RemoveAt(i);
                }
            }
            r = AssembleFromLines(lines);
        }
        return r;
    }

    public static string RemoveNonAlphaNum(this String source, bool keepBlank = false)
    {
        return System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(source, keepBlank ? "[^a-zA-Z\x20]" : "[^a-zA-Z]", ""), "\x20$", "");
    }

    public static string RemovePrefix(this String val, string prefix, bool ignoreCase = true)
    {
        if (!string.IsNullOrEmpty(val) && (ignoreCase ? val.StartsWithIgnoreCase(prefix) : val.StartsWith(prefix)))
        {
            return val.Substring(prefix.Length, val.Length - prefix.Length);
        }
        return val;
    }

    public static string RemovePrefix2(this String s, string prefix, bool repeat = false)
    {
        string r = s;
        if (r != null)
        {
            if (!(string.IsNullOrEmpty(prefix)))
            {
                while (r.StartsWithInvariant(prefix))
                {
                    r = r.Substring(prefix.Length);
                    if (!repeat)
                    {
                        break;
                    }
                }
            }
        }
        return r;
    }
    
    public static string RemoveSuffix2(this String s, string suffix, bool repeat = false)
    {
        string r = s;
        if (r != null)
        {
            if (!(string.IsNullOrEmpty(suffix)))
            {
                while (r.EndsWithInvariant(suffix))
                {
                    r = r.Substring(0, r.Length - suffix.Length);
                    if (!repeat)
                    {
                        break;
                    }
                }
            }
        }
        return r;
    }

    public static string RemoveSuffix(this String val, string suffix, bool ignoreCase = true)
    {
        if (!string.IsNullOrEmpty(val) && (ignoreCase ? val.EndsWithIgnoreCase(suffix) : val.EndsWith(suffix)))
        {
            return val.Substring(0, val.Length - suffix.Length);
        }
        return null;
    }

    public static string RemoveWhitespace(this String text)
    {
        return text.ReplaceWhitespace();
    }

    public static string RemoveWhiteSpace(this String input)
    {
        return new string(input.ToCharArray()
                               .Where(c => !char.IsWhiteSpace(c))
                               .ToArray());

    }

    public static string RemoveWhitespace2(this String str)
    {
        string r = str.RemoveCharactersInString(" \r\n\t");
        return r;
    }

    public static string RemoveWrappingQuotes(this String str)
    {
        foreach (string quote in new string[] { Quote, SingleQuote })
        {
            if (str.StartsWithInvariant(quote) && str.EndsWithInvariant(quote) && str.Length > 1)
            {
                return str.Substring(1, str.Length - 2);
            }
        }
        return str;
    }

    public static string RenameFileIfExists(string FullFilePath)
    {
        int currentFileNumber = 1;
        while (File.Exists(FullFilePath))
        {

            string extension = "";
            int extensionIndex = FullFilePath.LastIndexOf('.');
            if (extensionIndex != -1)
            {
                extension = FullFilePath.Substring(extensionIndex);
                FullFilePath = FullFilePath.Substring(0, extensionIndex);
            }

            if (FullFilePath.Length != 0 && FullFilePath[FullFilePath.Length - 1] == ')')
            {
                int openPranthesesIndex = FullFilePath.Length - 2;

                bool isFound = false;
                while (openPranthesesIndex >= 0)
                {
                    if (FullFilePath[openPranthesesIndex] == '(')
                    {
                        isFound = true;
                        break;
                    }
                    openPranthesesIndex--;
                }

                if (isFound)
                {
                    int fileNumber;
                    if (int.TryParse(FullFilePath.Substring(openPranthesesIndex + 1, FullFilePath.Length - 1 - openPranthesesIndex - 1), out fileNumber))
                    {
                        currentFileNumber = fileNumber + 1;
                        FullFilePath = FullFilePath.Substring(0, openPranthesesIndex);
                    }
                }

            }



            FullFilePath += "(" + currentFileNumber + ")" + extension;

            currentFileNumber++;
        }

        return FullFilePath;
    }

    public static string? Repeat(this String value, int n)
    {
        if (n < 0)
            return null;
        if (n == 0)
            return string.Empty;
        else
            return string.Concat(Enumerable.Repeat(value, n));
    }

    public static string Replace(this String s, params char[] chars)
    {
        return chars.Aggregate(s, (current, c) => current.Replace(c.ToString(System.Globalization.CultureInfo.InvariantCulture), ""));
    }

    public static string Replace(this String input, string oldValue, string newValue, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        return Replace(input, oldValue, newValue, 0, input.Length, comparisonType);
    }

    public static string Replace(this String input, string oldValue, string newValue, int startIndex, int count, StringComparison comparisonType)
    {
        int total;

        return Replace(input, oldValue, newValue, startIndex, count, comparisonType, out total);
    }

    public static string Replace(this String input, string oldValue, string newValue, int startIndex, int count, StringComparison comparisonType, out int total)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (oldValue == null)
            throw new ArgumentNullException("oldValue");
        if (oldValue.IsEmpty())
            throw new ArgumentException("oldValue cannot be empty", "oldValue");
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");

        string result;

        // the initial length of the result for stringBuilder assumes that there is only 1 value to replace
        int initialLength = Math.Max(input.Length - oldValue.Length, 0);
        if (newValue != null)
            initialLength += newValue.Length;

        System.Text.StringBuilder resultBuilder = new System.Text.StringBuilder(initialLength);

        if (startIndex > 0)
        {
            resultBuilder.Append(input, 0, startIndex);
        }

        int currentIndex = startIndex;
        int maxIndex = startIndex + count;

        total = 0;

        while (currentIndex < maxIndex)
        {
            int lastIndex = currentIndex;
            int newIndex = input.IndexOf(oldValue, lastIndex, maxIndex - lastIndex, comparisonType);

            if (newIndex != -1)
            {
                resultBuilder.Append(input, lastIndex, newIndex - lastIndex);
                resultBuilder.Append(newValue);

                currentIndex = newIndex + oldValue.Length;

                total++;
            }
            else
            {
                break;
            }
        }

        // append the final part
        int finalCount = input.Length - currentIndex;
        resultBuilder.Append(input, currentIndex, finalCount);

        result = resultBuilder.ToString();

        return result;
    }

    public static string Replace2(this String str, string oldValue, string newValue, StringComparison comparison)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        int previousIndex = 0;
        int index = str.IndexOf(oldValue, comparison);
        while (index != -1)
        {
            sb.Append(str.Substring(previousIndex, index - previousIndex));
            sb.Append(newValue);
            index += oldValue.Length;

            previousIndex = index;
            index = str.IndexOf(oldValue, index, comparison);
        }
        sb.Append(str.Substring(previousIndex));

        string r = sb.ToString();
        return r;
    }

    public static string ReplaceFirst(this String text, string search, string replace)
    {
        int pos = text.IndexOf(search);
        if (pos < 0)
        {
            return text;
        }

        return text.Substring(0, pos) + replace + text.Substring(pos + search.Length);
    }

    public static string ReplaceFirst2(this String text, string searchText, string replacement)
    {
        if (text == null) return null;
        replacement = replacement ?? string.Empty;
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(searchText, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        return regex.Replace(text, replacement, 1);
    }

    public static string ReplaceIgnoreCase(this String input, string oldValue, string newValue)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, oldValue, newValue, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
    }

    public static string ReplaceIgnoreCase2(this String input, string oldValue, string newValue)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }

        return input.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? string.Empty),
            newValue ?? string.Empty,
            InvariantCultureIgnoreCaseRegexOptions
        );
    }

    public static string ReplaceIgnoreCase2(this String input, string oldValue, Func<string, string> replaceCallback)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }

        return input.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? string.Empty),
            replaceCallback
        );
    }

    public static string ReplaceIgnoreCase2(this String input, string oldValue, Func<int, string, string> replaceCallback)
    {
        if (input is null) { throw new ArgumentNullException(nameof(input)); }

        return input.RegexReplace
        (
            System.Text.RegularExpressions.Regex.Escape(oldValue ?? string.Empty),
            replaceCallback
        );
    }

    public static string ReplaceKeyWithValue(this string? input, object? keyValues, string keyPrefix, string keySuffix)
    {
        ArgumentNullException.ThrowIfNull(keyValues);
        if (string.IsNullOrWhiteSpace(input))
            return input ?? string.Empty;

        var sb = new System.Text.StringBuilder(input);
        foreach (var (key, value) in ExtractKeyValues(keyValues))
        {
            sb.Replace(keyPrefix + key + keySuffix, value?.ToString());
        }

        return sb.ToString();
    }

    public static string ReplaceLineFeeds(this String val)
    {
        return System.Text.RegularExpressions.Regex.Replace(val, @"^[\r\n]+|\.|[\r\n]+$", "");
    }

    public static string ReplaceWhitespace(this String text, string replacement = "", bool groupreplace = true)
    {
        if (string.IsNullOrWhiteSpace(text))
            return string.Empty;

        return System.Text.RegularExpressions.Regex.Replace(text, groupreplace ? @"[\s]+" : @"\s", replacement);
    }
    
    public static string Resize(this String value, int count, bool isEndModified = true, char paddingChar = ' ')
    {
        if (count < 0)
            throw new ArgumentOutOfRangeException(nameof(count));

        if (string.IsNullOrEmpty(value))
        {
            return new string(paddingChar, count);
        }
        else
        {
            if (isEndModified)
            {
                if (value.Length > count)
                    return value.Substring(0, count);
                else
                    return value.PadRight(count, paddingChar);
            }
            else
            {
                if (value.Length > count)
                    return value.Substring(value.Length - count, count);
                else
                    return value.PadLeft(count, paddingChar);
            }

        }
    }

    internal static string ReturnOrThrowIfEmpty(this String input, string paramName)
    {
        input.ThrowIfEmpty(paramName);
        return input;
    }

    internal static string ReturnOrThrowIfNull([System.Diagnostics.CodeAnalysis.NotNull] this string? input, string paramName)
    {
        input.ThrowIfNull(paramName);
        return input;
    }

    public static string ReturnOrThrowIfNullEmptyOrWhitespace([System.Diagnostics.CodeAnalysis.NotNull] this string? input, string paramName)
    {
        input.ThrowIfNullEmptyOrWhitespace(paramName);
        return input;
    }

    internal static string ReturnOrThrowIfWhitespace(this String input, string paramName)
    {
        input.ThrowIfWhitespace(paramName);
        return input;
    }

    public static string Reverse(this String self)
    {
        ICollection<char> reversedCharacters = new List<char>();

        for (int i = self.Length - 1; i >= 0; i--)
            reversedCharacters.Add(self[i]);

        return string.Join(string.Empty, reversedCharacters);
    }

    public static string Reverse(this String input, int startIndex, int count)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (startIndex < 0 || startIndex >= input.Length)
            throw new ArgumentOutOfRangeException("startIndex", "startIndex should be between 0 and input.Length");
        if (count < 0 || count > input.Length - startIndex)
            throw new ArgumentOutOfRangeException("count", "count should be larger or equal to 0 and smaller than input.Length - startIndex");

        string result = input;

        // prevent reversing 0 characters
        if (count > 0)
        {
            char[] characters = input.ToCharArray();

            // reverse the intended characters
            Array.Reverse(characters, startIndex, count);

            result = new string(characters);
        }

        return result;
    }

    public static string Reverse2(this String input)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        return Reverse(input, 0, input.Length);
    }

    public static string Reverse3(this String s)
    {
        System.Text.StringBuilder builder = new(s.Length);

        for (int i = s.Length - 1; i >= 0; i--)
            builder.Append(s[i]);

        return builder.ToString();
    }

    public static string Reverse4(this String val)
    {
        var chars = new char[val.Length];
        for (int i = val.Length - 1, j = 0; i >= 0; --i, ++j)
        {
            chars[j] = val[i];
        }
        val = new String(chars);
        return val;
    }

    public static string ReverseSlash(this String val, int direction)
    {
        switch (direction)
        {
            case 0:
                return val.Replace(@"/", @"\");
            case 1:
                return val.Replace(@"\", @"/");
            default:
                return val;
        }
    }

    public static string ReverseString(this String str)
    {
        string s = "";
        for (int i = str.Length - 1; i >= 0; i--)
        {
            s += str[i];
        }
        return s;
    }

    public static string Right(this String source, int length)
    {
        if (length >= source.Length)
            return source;
        else
            //return source.Substring(source.Length - length); // (OLD Version)
            return source[^length..]; // (New Version)
    }

    public static string? Right(this String value, int length, bool withEllipsis = false)
    {
        // TODO manage bad length
        if (value == null)
            return null;
        else if (value.Length <= length)
            return value;
        else
        {
            if (withEllipsis)
                return "." + value.Substring(value.Length - length + 1);
            else
                return value.Substring(value.Length - length);
        }
    }

    public static string Right2(this String input, int length)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (length < 0 || length > input.Length)
            throw new ArgumentOutOfRangeException("length", "length cannot be higher than the amount of available characters in the input or lower than 0");

        int startIndex = input.Length - length;
        string result = input.Substring(startIndex);

        return result;
    }

    public static string Right3(this String val, int length)
    {
        if (string.IsNullOrEmpty(val))
        {
            throw new ArgumentNullException("val");
        }
        if (length < 0 || length > val.Length)
        {
            throw new ArgumentOutOfRangeException("length",
                "length cannot be higher than total string length or less than 0");
        }
        return val.Substring(val.Length - length);
    }

    public static string Right4(this String input, int length)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentException("The input string is null or empty.", nameof(input));

        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");

        if (length > input.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be smaller or equal to the length of the input string.");

        return input.Substring(input.Length - length, length);
    }

    public static string RightOf(this String input, char character)
    {
        return RightOf(input, character, 0);
    }

    public static string RightOf(this String input, char character, int skip)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (skip < 0)
            throw new ArgumentOutOfRangeException("skip", "skip should be larger or equal to 0");

        string result;

        if (input.Length <= skip)
        {
            result = input;
        }
        else
        {
            int characterPosition = input.Length;
            int foundCharacters = -1;

            while (foundCharacters < skip)
            {
                characterPosition = input.LastIndexOf(character, characterPosition - 1);
                if (characterPosition == -1)
                {
                    break;
                }
                else
                {
                    foundCharacters++;

                    if (characterPosition == 0)
                    {
                        break;
                    }
                }
            }

            if (characterPosition == -1)
            {
                result = input;
            }
            else
            {
                result = input.Substring(characterPosition + 1);
            }
        }

        return result;
    }

    public static string RightOf(this String input, string value)
    {
        return RightOf(input, value, StringComparison.Ordinal);
    }

    public static string RightOf(this String input, string value, StringComparison comparisonType)
    {
        return RightOf(input, value, 0, comparisonType);
    }

    public static string RightOf(this String input, string value, int skip, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (value == null)
            throw new ArgumentNullException("value");
        if (skip < 0)
            throw new ArgumentOutOfRangeException("skip", "skip should be larger or equal to 0");

        string result;
        if (input.Length <= skip)
        {
            result = input;
        }
        else
        {
            int valuePosition = -1;
            int valuesFound = -1;

            while (valuesFound < skip)
            {
                valuePosition = input.IndexOf(value, valuePosition + 1, comparisonType);
                if (valuePosition == -1)
                {
                    break;
                }
                else
                {
                    valuesFound++;
                }
            }

            if (valuePosition == -1)
            {
                result = input;
            }
            else
            {
                result = input.Substring(valuePosition + value.Length);
            }
        }

        return result;
    }

    public static string RightOfLast(this String input, char character)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");

        string result;
        int lastCharacterPosition = input.LastIndexOf(character);

        if (lastCharacterPosition == -1)
        {
            result = input;
        }
        else
        {
            result = input.Substring(lastCharacterPosition + 1);
        }

        return result;
    }

    public static string RightOfLast(this String input, string value)
    {
        return RightOfLast(input, value, StringComparison.Ordinal);
    }

    public static string RightOfLast(this String input, string value, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (value == null)
            throw new ArgumentNullException("value");

        string result;
        int lastValuePosition = input.LastIndexOf(value, comparisonType);

        if (lastValuePosition == -1)
        {
            result = input;
        }
        else
        {
            result = input.Substring(lastValuePosition + value.Length);
        }

        return result;
    }

    public static string SimplifyNewlines(this String str)
    {
        return str.Replace("\r\n", "\n");
    }

    public static string SeparatePascalCase(this String value)
    {
        return System.Text.RegularExpressions.Regex.Replace(value, "([A-Z])", " $1").Trim();
    }

    public static string Serialize(this object dataToSerialize)
    {
        if (dataToSerialize == null) return null;
        var options = new System.Text.Json.JsonSerializerOptions { WriteIndented = true };
        return System.Text.Json.JsonSerializer.Serialize(dataToSerialize, options);
    }

    public static string SetCase(this String s, AT.Enums.CaseType caseType)
    {
        return caseType switch
        {
            AT.Enums.CaseType.Lower => s.ToLower(),
            AT.Enums.CaseType.Upper => s.ToUpper(),
            AT.Enums.CaseType.CapitalizeFirstCharacter => SetCapitalizeFirstCharacter(s),
            AT.Enums.CaseType.Sentence => SetSentenceCase(s),
            AT.Enums.CaseType.Title => SetTitleCase(s),
            _ => s,
        };
    }

    public static string SetValue(object value)
    {
        if (value == null)
        {
            return null;
        }
        else if (value is float)
        {
            return ((float)value).ToString("R");
        }
        else if (value is double)
        {
            return ((double)value).ToString("R");
        }
        else if (value is DateTime)
        {
            return ((DateTime)value).ToString("O");
        }
        else
        {
            return value.ToString();
        }
    }

    public static string Shorten(this String source, int maxLength)
    {
        if (source.Length <= maxLength)
            return source;

        return string.Concat(source.AsSpan(0, maxLength - 3), "...");
    }

    public static string Skip(this String source, int count)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return count <= 0
                   ? source
                   : count >= source.Length
                         ? String.Empty
                         : source.Substring(count);
    }

    public static string Slug(this String text, int maxLength)
    {
        if (maxLength <= 0)
            throw new ArgumentOutOfRangeException(nameof(maxLength));

        if (text.IsNullOrEmpty())
            return text;

        var slug = text.RemoveDiacritics().ToLower();
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"[^a-z0-9\s-]", "");
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s+", " ").Trim();
        slug = slug.Substring(0, slug.Length <= maxLength ? slug.Length : maxLength).Trim();
        slug = System.Text.RegularExpressions.Regex.Replace(slug, @"\s", "-");

        return slug;
    }

    public static string Slugify(this String str)
    {
        return string.Join("", str.ToKebabCase()
            .Where(x => x.IsLetterOrDigit() || x == '-'));
    }

    public static string Sort(this String s, bool ignoreCase = false)
    {
        System.Text.StringBuilder builder = new(s.Length);
        IComparer<char> comparer = Strings.Comparison.Extensions.CharComparer.GetComparer(ignoreCase);

        foreach (char c in s.OrderBy(c => c, comparer))
        {
            builder.Append(c);
        }

        return builder.ToString();
    }

    public static string SplitCamelCase2(this String str)
    {
        return System.Text.RegularExpressions.Regex.Replace(System.Text.RegularExpressions.Regex.Replace(str, @"(\P{Ll})(\P{Ll}\p{Ll})", "$1 $2"), @"(\p{Ll})(\P{Ll})", "$1 $2");
    }

    public static string SplitPascalCase(this String str)
    {
        return SplitCamelCase2(str);
    }

    public static string StringWithPrefix(this String s, string prefix)
    {
        if (string.IsNullOrEmpty(prefix))
        {
            return s;
        }
        if (string.IsNullOrEmpty(s))
        {
            return prefix;
        }
        if (s.StartsWithInvariant(prefix))
        {
            return s;
        }
        return prefix + s;
    }

    public static string StringWithSuffix(this String s, string suffix)
    {
        if (string.IsNullOrEmpty(suffix))
        {
            return s;
        }
        if (string.IsNullOrEmpty(s))
        {
            return suffix;
        }
        if (s.EndsWithInvariant(suffix))
        {
            return s;
        }
        return s + suffix;
    }

    public static string SubstringFromFirstOccurrence(this String str, string afterThis)
    {
        int index = str.IndexOfInvariant(afterThis);
        string r;
        if (index == -1)
        {
            r = str;
        }
        else
        {
            r = str.Substring(index + afterThis.Length);
        }
        return r;
    }

    public static string SubstringFromLastOccurrence(this String str, string afterMe)
    {
        int index = str.LastIndexOfInvariant(afterMe);
        string r;
        if (index == -1)
        {
            r = str;
        }
        else
        {
            r = str.Substring(index + afterMe.Length);
        }
        return r;
    }

    public static string SubstringToFirstOccurrence(this String str, string toThis)
    {
        if (str == null)
        {
            return null;
        }
        int index = str.IndexOfInvariant(toThis);
        if (index == -1)
        {
            return str;
        }
        return str.Substring(0, index);
    }

    public static string Take(this String source, int count)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return count <= 0
            ? String.Empty
            : source.Substring(0, Math.Min(count, source.Length));
    }

    public static string TrimCrLf(this String value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            value = value.Replace("\\r\\n", " ")
                        .Replace("\\r", " ")
                        .Replace("\\n", " ")
                        .Replace("\r\n", " ")
                        .Replace("\r", " ")
                        .Replace("\n", " ")
                        .Replace("\\", string.Empty)
                        .Replace("  ", " ")
                        .Trim();
        }

        return value;
    }
    
    public static string TrimEnd(this String input, string endsWith)
    {
        return TrimEnd(input, endsWith, StringComparison.Ordinal);
    }

    public static string TrimEnd(this String input, string endsWith, StringComparison comparisonType)
    {
        // its safe to call trimStart with int.MaxValue as max since a string cannot the currently restricted 2gb size limit.
        // however, this might change in the future
        return TrimEnd(input, endsWith, comparisonType, int.MaxValue);
    }

    public static string TrimEnd(this String input, string endsWith, StringComparison comparisonType, int max)
    {
        int count;

        return TrimEnd(input, endsWith, comparisonType, max, out count);
    }

    public static string TrimEnd(this String input, string endsWith, StringComparison comparisonType, int max, out int total)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (endsWith == null)
            throw new ArgumentNullException("endsWith");
        if (max <= 0)
            throw new ArgumentOutOfRangeException("max", "Max cannot be smaller or equal to 0");

        string result = input;
        total = 0;

        // optimization to prevent empty value sequences from being removed
        if (endsWith.Length > 0)
        {
            for (; total < max; total++)
            {
                if (result.EndsWith(endsWith, comparisonType))
                {
                    result = result.Remove(result.Length - endsWith.Length);
                }
                else
                {
                    break;
                }
            }
        }

        return result;
    }

    public static string TrimEndOnce(this String input, string endsWith)
    {
        return TrimEndOnce(input, endsWith, StringComparison.Ordinal);
    }

    public static string TrimEndOnce(this String input, string endsWith, StringComparison comparisonType)
    {
        return TrimEnd(input, endsWith, comparisonType, 1);
    }

    public static string TrimEveryThing(this String source)
    {
        return System.Text.RegularExpressions.Regex.Replace(source, @" {2,}", " ").Trim();
    }

    public static string TrimStart(this String input, string startsWith)
    {
        return TrimStart(input, startsWith, StringComparison.Ordinal);
    }

    public static string TrimStart(this String input, string startsWith, StringComparison comparisonType)
    {
        // its safe to call trimStart with int.MaxValue as max since a string cannot the currently restricted 2gb size limit.
        // however, this might change in the future
        return TrimStart(input, startsWith, comparisonType, int.MaxValue);
    }

    public static string TrimStart(this String input, string startsWith, StringComparison comparisonType, int max)
    {
        int count;

        return TrimStart(input, startsWith, comparisonType, max, out count);
    }

    public static string TrimStart(this String input, string startsWith, StringComparison comparisonType, int max, out int total)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (startsWith == null)
            throw new ArgumentNullException("startsWith");
        if (max <= 0)
            throw new ArgumentOutOfRangeException("max", "Max cannot be smaller or equal to 0");

        string result = input;
        total = 0;

        // optimization to prevent empty startWith sequences from being removed
        if (startsWith.Length > 0)
        {
            for (; total < max; total++)
            {
                if (result.StartsWith(startsWith, comparisonType))
                {
                    result = result.Remove(0, startsWith.Length);
                }
                else
                {
                    break;
                }
            }
        }

        return result;
    }

    public static string TrimStartOnce(this String input, string startsWith)
    {
        return TrimStartOnce(input, startsWith, StringComparison.Ordinal);
    }

    public static string TrimStartOnce(this String input, string startsWith, StringComparison comparisonType)
    {
        return TrimStart(input, startsWith, comparisonType, 1);
    }

    public static string Truncate(this String input, int length, string ellipsis)
    {
        return Truncate(input, length, ellipsis, true);
    }

    public static string Truncate(this String input, int length, string ellipsis, bool inclusiveEllipsis)
    {
        return Truncate(input, length, ellipsis, inclusiveEllipsis, null, false, StringComparison.Ordinal);
    }

    public static string Truncate(this String input, int length, string ellipsis, bool inclusiveEllipsis, string boundary, bool emptyOnNoBoundary, StringComparison comparisonType)
    {
        // preconditions
        if (input == null)
            throw new ArgumentNullException("input");
        if (length < 0)
            throw new ArgumentOutOfRangeException("length", "length cant be smaller than 0");

        // the length of ellipsis might not be larger than the desired length of the resulting string when inclusiveEllipsis is set
        if (ellipsis != null)
        {
            if (inclusiveEllipsis)
            {
                if (ellipsis.Length > length)
                    throw new ArgumentException("Ellipsis cant be larger than the desired length when inclusiveEllipsis is set", "ellipsis");
            }
        }

        string result = input;

        if (input.Length > length)
        {
            int checkLength = length;

            if (inclusiveEllipsis && !string.IsNullOrEmpty(ellipsis))
            {
                // ensure that we leave space for the ellipsis
                checkLength -= ellipsis.Length;
            }
            if (!string.IsNullOrEmpty(boundary))
            {
                int boundaryIndex = input.LastIndexOf(boundary, checkLength, checkLength, comparisonType);
                if (boundaryIndex != -1)
                {
                    int boundaryLength = boundaryIndex; // we want to stop right before the boundary starts so we can use the index of the boundary as the length.

                    result = input.Left(boundaryLength);
                }
                else
                {
                    if (emptyOnNoBoundary)
                    {
                        result = string.Empty;
                    }
                    else
                    {
                        result = input.Left(length);
                    }
                }
            }
            else
            {
                result = input.Left(checkLength);
            }

            if (!string.IsNullOrEmpty(ellipsis))
            {
                result += ellipsis;
            }
        }
        else
        {
            if (!inclusiveEllipsis)
            {
                if (ellipsis != null)
                {
                    result += ellipsis;
                }
            }
        }

        return result;
    }

    public static string Truncate(this String s, int maxLength, bool smartTrim = true, bool appendEllipsis = true)
    {
        const string ellipsis = "...";

        if (maxLength < 0)
            throw new ArgumentException($"{nameof(maxLength)} cannot be less than zero.");

        if (s.Length > maxLength)
        {
            if (appendEllipsis)
            {
                if (maxLength > ellipsis.Length)
                    maxLength -= ellipsis.Length;
                else
                    appendEllipsis = false;
            }

            int length = maxLength;

            if (smartTrim)
            {
                // Remove Comment

                //while (length > 0 && s.IsWordCharacter(length))
                //    length--;
                while (length > 0 && char.IsWhiteSpace(s[length - 1]))
                    length--;
                if (length == 0)
                    length = maxLength;
            }

            s = s.Substring(0, length);

            if (appendEllipsis)
                s += ellipsis;
        }

        return s;
    }

    public static string Truncate(this String s, int maxLength)
    {
        if (String.IsNullOrEmpty(s) || maxLength <= 0)
        {
            return String.Empty;
        }
        if (s.Length > maxLength)
        {
            return s.Substring(0, maxLength) + "...";
        }
        return s;
    }

    public static string Truncate2(this string? value, int maxLength, string? truncateSymbol = EllipsisAsciiSymbol)
    {
        if (maxLength < 1)
            throw new ArgumentOutOfRangeException(nameof(maxLength), string.Format(AT.Infrastructure.ExceptionMessages.ParamCannotBeLessThan_ParamName_MinValue_ActualValue, nameof(maxLength), 1, maxLength));

        truncateSymbol ??= string.Empty;
        if (truncateSymbol.Length > maxLength)
            throw new ArgumentException(string.Format(AT.Infrastructure.ExceptionMessages.ParamAStringLengthCannotBeGreaterThanValueOfParamB_ParamAStrLen_ParamBValue, nameof(truncateSymbol), nameof(maxLength)), nameof(truncateSymbol));

        if (value is null)
            return string.Empty;

        if (value.Length > maxLength)
            return value[..(maxLength - truncateSymbol.Length)] + truncateSymbol;

        return value;
    }

    public static string Truncate3(this String value, int maxLength, string suffix = "")
    {
        if (string.IsNullOrEmpty(value))
            return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength) + suffix;
    }

    public static string Union(this String s, IEnumerable<char> unionChars, bool ignoreCase = false)
    {
        int unionCharsCount = unionChars.Count();
        System.Text.StringBuilder builder = new(s.Length + unionCharsCount);
        HashSet<char> hashSet = new(s.Length + unionCharsCount, Comparison.Extensions.CharComparer.GetEqualityComparer(ignoreCase));

        foreach (char c in s)
        {
            if (hashSet.Add(c))
                builder.Append(c);
        }

        foreach (char c in unionChars)
        {
            if (hashSet.Add(c))
                builder.Append(c);
        }

        return builder.ToString();
    }

    public static string Union(this String s, string unionChars, bool ignoreCase = false)
    {
        return Union(s, (IEnumerable<char>)unionChars, ignoreCase);
    }

    public static string WithTrailingSlash(this String value)
    {
        var slash = value.EndsWith("/") ? "" : "/";
        return $"{value}{slash}";
    }

    public static string WrapInQuotes(this String str, string quote = Quote)
    {
        string r = str.StringWithPrefix(Quote).StringWithSuffix(Quote);
        return r;
    }
}