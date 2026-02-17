using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Comparison;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Conversion;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Conversion;
public static class ToExtensions
{
    #region Field(s)

    private static readonly Char[] NibbleToStringMappingUpperCase = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F' };
    private static readonly Char[] NibbleToStringMappingLowerCase = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };

    static String whiteSpace = " ";
    static Int32 notFoundIndexValue = -1;

    private static readonly String[] DecimalMeasurementSuffixes = new[]
                                                                  {
                                                                      "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB"
                                                                  };

    private static readonly String[] BinaryMeasurementSuffixes = new[]
                                                                 {
                                                                     "B", "KiB", "MiB", "GiB", "TiB", "PiB", "EiB", "ZiB", "YiB"
                                                                 };

    private static readonly Dictionary<String, String> ReplaceDictionary = new Dictionary<String, String>()
                                                                            {
                                                                                {"ı","i"}, //türkçeler
                                                                                {"ü","u"},
                                                                                {"ö","o"},
                                                                                {"ş","s"},
                                                                                {"ğ","g"},
                                                                                {"ç","c"},
                                                                                {"ä","a"},//a şapkalılar 
                                                                                {"ã","a"},
                                                                                {"à","a"},
                                                                                {"á","a"},
                                                                                {"â","a"},
                                                                                {"å","a"},
                                                                                {"ë","e"}, //e şapkalılar
                                                                                {"è","e"},
                                                                                {"é","e"},
                                                                                {"ê","e"},
                                                                                {"ï","i"}, //ı şapkalılar
                                                                                {"ì","i"},
                                                                                {"í","i"},
                                                                                {"î","i"},
                                                                                {"õ","o"}, //o şapkalılar
                                                                                {"ò","o"},
                                                                                {"ó","o"},
                                                                                {"ô","o"},
                                                                                {"ù","u"}, //u şapkalılar
                                                                                {"ú","u"},
                                                                                {"û","u"},
                                                                                {"ß","ss"},
                                                                                {"ñ", "n" }, //ispanya
                                                                                {"ý", "y" },
                                                                                {"ø", "o" }, //kuzey harfleri
                                                                                {"æ", "ae" },
                                                                                {"œ", "oe" },
                                                                                {"ð", "d" },
                                                                                {"þ", "th" },
                                                                                {"ƒ", "f" },
                                                                                {"_"," "}, //tireler
                                                                                {"-",""},
                                                                                {"*","" }, //dört işlem
                                                                                {"+"," "},
                                                                                {"="," "},
                                                                                {"\\",""},
                                                                                {"/",""},
                                                                                {"'",""},  //tırnaklar
                                                                                {"‘",""},
                                                                                {"’",""},
                                                                                {"\""," "},
                                                                                {"”"," "},
                                                                                {"“"," "},
                                                                                {"("," "},  //parantezler
                                                                                {")"," "},
                                                                                {"["," "},
                                                                                {"]"," "},
                                                                                {"{"," "},
                                                                                {"}"," "},
                                                                                {"."," "}, //nokta virgül, soru işareti
                                                                                {","," "},
                                                                                {"&"," "},
                                                                                {"?","" },
                                                                                {"!","" },
                                                                                {"¿", ""},
                                                                                {"¡","" },
                                                                                {"@"," " }, //heşteg vs
                                                                                {"©"," " },
                                                                                {"#"," " },
                                                                                {"~"," " }, //tilda milda
                                                                                {"¨"," " },
                                                                                {"´"," " },
                                                                                {"`"," " },
                                                                                {"^"," " },
                                                                            };

    #endregion

    #region Method(s): Private

    private static Int32 FindFirstLetterIndex(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Int32 index = notFoundIndexValue;
        for (Int32 i = 0; i < value.Length; i++)
            if (value.Substring(i, 1) != whiteSpace)
            {
                index = i;
                break;
            }
        // ----------------------------------------------------------------------------------------------------
        return index;
    }

    private static String FromHungarianNotation(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Replace('_', ' ').EveryWordUpper().Replace(" ", "");
    }

    private static String FromCamelCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return $"{value.FirstOrDefault().ToUpper()}{value.Substring(1)}";
    }

    private static String FromKebabCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Replace('-', ' ').EveryWordUpper().Replace(" ", "");
    }

    private static String DetectAndNormalize(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.IsHungarianNotation())
            return value.FromHungarianNotation();

        if (value.IsCamelCase())
            return value.FromCamelCase();

        if (value.IsKebabCase())
            return value.FromKebabCase();
        // ----------------------------------------------------------------------------------------------------
        return value.EveryLetterUpper().Replace(" ", string.Empty);
    }

    #endregion

    public static String ToAlphabetic(this String value, Boolean preserveWhitespace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();
        value.Where(current => current.IsLetter() || (current.IsWhiteSpace() && preserveWhitespace)).ToList().ForEach(search => result.Append(search));
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String ToAlphanumeric(this String value, Boolean preserveWhitespace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();
        value.Where(current => current.IsLetterOrDigit() || (current.IsWhiteSpace() && preserveWhitespace)).ToList().ForEach(search => result.Append(search));
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String ToBase64String(this String value, System.Text.Encoding? encoding = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        encoding ??= System.Text.Encoding.UTF8;
        return Convert.ToBase64String(encoding.GetBytes(value));
    }

    public static String ToBase64String(this byte[] values)
    {
        ArgumentNullException.ThrowIfNull(values);
        // ----------------------------------------------------------------------------------------------------
        return Convert.ToBase64String(values);
    }

    public static String ToBase64UrlString(this byte[]? values, Boolean removePadding = true)
    {
        ArgumentNullException.ThrowIfNull(values);
        // ----------------------------------------------------------------------------------------------------
        String base64Uri = Convert.ToBase64String(values).Replace("+", "-").Replace("/", "_");
        return removePadding
               ? base64Uri.TrimEnd('=')
               : base64Uri.Replace("=", "%3D");
    }

    public static String ToCamelCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.DetectAndNormalize().FirstLetterLower();
    }

    public static String ToCommaSeparated(this List<String> values)
    {
        ArgumentNullException.ThrowIfNull(values);
        // ----------------------------------------------------------------------------------------------------
        String value = String.Empty;
        if (values is not null)
            if (values.Any())
                value = String.Join(',', values);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String ToCommaSeparated(this List<String> values, Char separator)
    {
        ArgumentNullException.ThrowIfNull(values);
        // ----------------------------------------------------------------------------------------------------
        String value = String.Empty;
        if (values is not null)
            if (values.Any())
                value = String.Join(separator, values);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static System.Text.Encoding ToEncoding(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.ToUpper(System.Globalization.CultureInfo.InvariantCulture) switch
        {
            "ASCII" => System.Text.Encoding.ASCII,
            "UTF8" => System.Text.Encoding.UTF8,
            "UTF32" => System.Text.Encoding.UTF32,
            "UNICODE" => System.Text.Encoding.Unicode,
            _ => throw new ArgumentException("Argument not supported", nameof(value)),
        };
    }

    public static String ToEndTrimmedString(this object input)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(Convert.ToString(input));
        // ----------------------------------------------------------------------------------------------------
        return Convert.ToString(input).TrimEnd();
    }

    public static String ToFormat(this String stringFormat, params object[] args)
    {
        ArgumentException.ThrowIfNullOrEmpty(stringFormat);
        // ----------------------------------------------------------------------------------------------------
        return String.Format(stringFormat, args);
    }

    public static String ToFullPath(this String path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        // ----------------------------------------------------------------------------------------------------
        return Path.GetFullPath(path);
    }

    public static String ToGmtFormattedDate(this DateTime date)
    {
        ArgumentNullException.ThrowIfNull(date);
        // ----------------------------------------------------------------------------------------------------
        return date.ToString("yyyy'-'MM'-'dd hh':'mm':'ss tt 'GMT'");
    }

    public static String ToHash(this String text)
    {
        ArgumentException.ThrowIfNullOrEmpty(text);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<string> parts = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(text)).Select(b => b.ToString("x2"));
        // ----------------------------------------------------------------------------------------------------
        return String.Join("", parts);
    }

    public static String ToHexString(this byte[]? value, Boolean upperCase = true)
    {
        ArgumentNullException.ThrowIfNull(value);
        // ----------------------------------------------------------------------------------------------------
        char[] mapping = upperCase ? NibbleToStringMappingUpperCase : NibbleToStringMappingLowerCase;
        char[] hexString = new Char[value.Length * 2];
        int charIndex = 0;
        // ----------------------------------------------------------------------------------------------------
        for (var i = 0; i < value.Length; i += 1, charIndex += 2)
        {
            hexString[charIndex] = mapping[value[i] >> 4];
            hexString[charIndex + 1] = mapping[value[i] & 0x0F];
        }
        // ----------------------------------------------------------------------------------------------------
        return new String(hexString);
    }

    public static String ToHumanReadableFileSize(this Int64 input, Boolean binary = true)
    {
        Boolean negative = default;
        // ----------------------------------------------------------------------------------------------------
        if (input < 0)
        {
            input *= -1;
            negative = true;
        }
        // ----------------------------------------------------------------------------------------------------
        Double value = input;
        Double divisor = binary ? 1024d : 1000d;
        Int32 idx = 0;
        Int32 maxIdx = binary
                       ? BinaryMeasurementSuffixes.Length
                       : DecimalMeasurementSuffixes.Length;
        // ----------------------------------------------------------------------------------------------------
        while (value >= divisor && (idx + 1) < maxIdx)
        {
            value /= divisor;
            idx++;
        }
        // ----------------------------------------------------------------------------------------------------
        return (negative ? "-" : String.Empty)
            + value.ToString(idx > 0 ? "0.00" : "0") + " " +
            (
                binary
                ? BinaryMeasurementSuffixes[idx]
                : DecimalMeasurementSuffixes[idx]
            );
    }

    public static String ToHungarianNotation(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Join("", value
                               .DetectAndNormalize()
                               .Select(x => x.IsUpper() ? $"_{x.ToLower()}" : x.ToString()))
                               .Trim('_');
    }

    public static String ToInsecureString(this System.Security.SecureString secureString)
    {
        ArgumentNullException.ThrowIfNull(secureString);
        // ----------------------------------------------------------------------------------------------------
        IntPtr ptr = IntPtr.Zero;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            ptr = System.Runtime.InteropServices.Marshal.SecureStringToGlobalAllocUnicode(secureString);
            return System.Runtime.InteropServices.Marshal.PtrToStringUni(ptr);
        }
        finally
        {
            System.Runtime.InteropServices.Marshal.ZeroFreeGlobalAllocUnicode(ptr);
        }
    }

    public static String ToInvariantString(this DateTime date, String format = "dd/MM/yyyy")
    {
        ArgumentNullException.ThrowIfNull(date);
        ArgumentException.ThrowIfNullOrEmpty(format);
        // ----------------------------------------------------------------------------------------------------
        return date.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public static String ToKebabCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Join("", value
                               .DetectAndNormalize()
                               .Select(x => x.IsUpper() ? $"-{x.ToLower()}" : x.ToString()))
                               .Trim('-');
    }

    public static String ToMorseCode(this String value, Boolean translateSpace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder morseStringBuilder = new();
        Dictionary<Char, String> morseCharacters = new()
                                                    {
                                                        {'a', ".-"},
                                                        {'b', "-..."},
                                                        {'c', "-.-."},
                                                        {'d', "-.."},
                                                        {'e', "."},
                                                        {'f', "..-."},
                                                        {'g', "--."},
                                                        {'h', "...."},
                                                        {'i', ".."},
                                                        {'j', ".---"},
                                                        {'k', "-.-"},
                                                        {'l', ".-.."},
                                                        {'m', "--"},
                                                        {'n', "-."},
                                                        {'o', "---"},
                                                        {'p', ".--."},
                                                        {'q', "--.-"},
                                                        {'r', ".-."},
                                                        {'s', "..."},
                                                        {'t', "-"},
                                                        {'u', "..-"},
                                                        {'v', "...-"},
                                                        {'w', ".--"},
                                                        {'x', "-..-"},
                                                        {'y', "-.--"},
                                                        {'z', "--.."},
                                                        {'0', "-----"},
                                                        {'1', ".----"},
                                                        {'2', "..---"},
                                                        {'3', "...--"},
                                                        {'4', "....-"},
                                                        {'5', "....."},
                                                        {'6', "-...."},
                                                        {'7', "--..."},
                                                        {'8', "---.."},
                                                        {'9', "----."},
                                                        {'.',".-.-.-"},
                                                        {',',"--.--"},
                                                        {'?',"..--.."},
                                                        {':',"---..."},
                                                        {';',"-.-.-."},
                                                        {' ',"-...-"},
                                                        {'/',"-..-."},
                                                        {'"',".-..-."}
                                                    };
        String tempMorseStr = String.Empty;
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value.ToLower())
        {
            if (morseCharacters.TryGetValue(c, out tempMorseStr))
            {
                if (c != ' ')
                    morseStringBuilder.Append(tempMorseStr);
                else
                {
                    if (translateSpace)
                        morseStringBuilder.Append(tempMorseStr);
                    else
                        morseStringBuilder.Append(' ');
                }
            }
            else
                morseStringBuilder.Append(c);
        }
        // ----------------------------------------------------------------------------------------------------
        return morseStringBuilder.ToString();
    }

    public static String ToPascal(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String upperLetter = String.Empty;
        Int32 firstLetterIndex = FindFirstLetterIndex(value);
        // ----------------------------------------------------------------------------------------------------
        if (firstLetterIndex == notFoundIndexValue)
            return value;
        // ----------------------------------------------------------------------------------------------------
        upperLetter = value.Substring(0, firstLetterIndex + 1).ToUpper();
        String unchangedStringPart = String.Empty;
        // ----------------------------------------------------------------------------------------------------
        if (value.Length > firstLetterIndex + 1)
            unchangedStringPart = value.Substring(firstLetterIndex + 1, value.Length - firstLetterIndex - 1);
        // ----------------------------------------------------------------------------------------------------
        return upperLetter + unchangedStringPart;
    }

    public static String ToPascalCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.DetectAndNormalize();
    }

    public static String ToPriceFormat(this object value)
    {
        ArgumentNullException.ThrowIfNull(value);
        // ----------------------------------------------------------------------------------------------------
        return Convert.ToDecimal(value).ToString("N0");
    }

    public static String ToSearchableString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        value = value.ToLower();
        // ----------------------------------------------------------------------------------------------------
        foreach (string rep in ReplaceDictionary.Keys)
            value = value.Replace(rep, ReplaceDictionary[rep]);
        // ----------------------------------------------------------------------------------------------------
        value = value.TrimEveryThing();
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String ToSentenceCase(this String value, Boolean cleanWhitespace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (cleanWhitespace)
            value = value.CleanWhiteSpace();
        // ----------------------------------------------------------------------------------------------------
        return value.Length > 1 
               ? value.Substring(0, 1).ToUpper() + value.Substring(1).ToLower() 
               : value.ToUpper();
    }

    public static String ToSentenceCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String sentence = value.ToLower().TrimStart();
            // ----------------------------------------------------------------------------------------------------
            if (sentence.Length <= 1) 
                return sentence.ToUpper();
            // ----------------------------------------------------------------------------------------------------
            return sentence.Remove(1).ToUpper() + sentence.Substring(1);
        }
        catch (Exception ex)
        {
            throw new(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
        }
    }

    public static System.Security.SecureString ToSecureString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Security.SecureString secureString = new();
        // ----------------------------------------------------------------------------------------------------
        foreach (Char c in value)
            secureString.AppendChar(c);
        // ----------------------------------------------------------------------------------------------------
        secureString.MakeReadOnly();
        // ----------------------------------------------------------------------------------------------------
        return secureString;
    }

    public static String ToSnakeCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        string result = value.ToAlphanumeric();
        // ----------------------------------------------------------------------------------------------------
        return result.ReplaceWhiteSpace("_");
    }

    public static String ToStartTrimmedString(this object input)
    {
        ArgumentNullException.ThrowIfNull(input);
        ArgumentException.ThrowIfNullOrEmpty(Convert.ToString(input));
        // ----------------------------------------------------------------------------------------------------
        return input.ToString().TrimStart();
    }

    public static String ToTrimmedString(this object input)
    {
        ArgumentNullException.ThrowIfNull(input);
        // ----------------------------------------------------------------------------------------------------
        return input.ToString().Trim();
    }

    public static String ToValidFilename(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Char[] invalid = Path.GetInvalidFileNameChars();
        String invalidString = new String(invalid);
        Char[] titleChars = value.ToCharArray();
        Char[] output = new Char[value.Length];
        Int32 outputLength = 0;
        // ----------------------------------------------------------------------------------------------------
        foreach (Char input in titleChars)
            if (invalidString.IndexOf(input) == -1)
            {
                output[outputLength] = input;
                outputLength++;
            }
        // ----------------------------------------------------------------------------------------------------
        String rawR = new String(output, 0, outputLength);
        String result = rawR.Trim();
        // ----------------------------------------------------------------------------------------------------
        if (result == "")
            result = "NonAlphaName";
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String ToWords(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.RegularExpressions.Regex.Replace(value, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
    }

    public static String? ToXmlFormat(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.TrimStart().StartsWith("<"))
            return System.Xml.Linq.XDocument.Parse(value).ToString();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}