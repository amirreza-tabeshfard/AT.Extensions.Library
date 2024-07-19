using AT.Extensions.Chars.Comparison;
using AT.Extensions.Chars.Conversion;
using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.Strings.Conversion;
public static partial class Extensions : object
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

    private static String FromHungarianNotation(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Replace('_', ' ').EveryWordUpper().Replace(" ", "");
    }

    private static String FromCamelCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return $"{value.FirstOrDefault().ToUpper()}{value.Substring(1)}";
    }

    private static String FromKebabCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.Replace('-', ' ').EveryWordUpper().Replace(" ", "");
    }

    internal static String DetectAndNormalize(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (value.IsHungarianNotation())
            return value.FromHungarianNotation();

        if (value.IsCamelCase())
            return value.FromCamelCase();

        if (value.IsKebabCase())
            return value.FromKebabCase();
        // ----------------------------------------------------------------------------------------------------
        return value.EveryLetterUpper().Replace(" ", "");
    }

    private static Int32 FindFirstLetterIndex(String str)
    {
        Int32 index = notFoundIndexValue;
        for (Int32 i = 0; i < str.Length; i++)
        {
            if (str.Substring(i, 1) != whiteSpace)
            {
                index = i;
                break;
            }
        }

        return index;
    }

    #endregion

    #region Method(s): Private Regex Attributes (It doesn't work in .NET 6 version)

    [System.Text.RegularExpressions.GeneratedRegex("\\w+", System.Text.RegularExpressions.RegexOptions.IgnoreCase, "en-US")]
    private static partial System.Text.RegularExpressions.Regex AlphaNumeric();

    #endregion

    #region DataType(s)

    public static Byte ToByte(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsByte() ? Byte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Byte'.");
    }

    public static Byte ToByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsByte(style, provider) ? Byte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Byte'.");
    }

    public static void ToByte(this String value, out Byte result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsByte() ? result = Byte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Byte'.");
    }

    public static void ToByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Byte result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsByte(style, provider) ? result = Byte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Byte'.");
    }

    public static SByte ToSByte(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsSByte() ? SByte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'SByte'.");
    }

    public static SByte ToSByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsSByte(style, provider) ? SByte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'SByte'.");
    }

    public static void ToSByte(this String value, out SByte result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsSByte() ? result = SByte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'SByte'.");
    }

    public static void ToSByte(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out SByte result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsSByte(style, provider) ? result = SByte.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'SByte'.");
    }

    public static Int16 ToShort(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsShort() ? Int16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int16'.");
    }

    public static Int16 ToShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsShort(style, provider) ? Int16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int16'.");
    }

    public static void ToShort(this String value, out Int16 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsShort() ? result = Int16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int16'.");
    }

    public static void ToShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Int16 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsShort(style, provider) ? result = Int16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int16'.");
    }

    public static UInt16 ToUShort(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsUShort() ? UInt16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt16'.");
    }

    public static UInt16 ToUShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsUShort(style, provider) ? UInt16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt16'.");
    }

    public static void ToUShort(this String value, out UInt16 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsUShort() ? result = UInt16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt16'.");
    }

    public static void ToUShort(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out UInt16 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsUShort(style, provider) ? result = UInt16.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt16'.");
    }

    public static Int32 ToInt(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsInt() ? Int32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int32'.");
    }

    public static Int32 ToInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsInt(style, provider) ? Int32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int32'.");
    }

    public static void ToInt(this String value, out Int32 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsInt() ? result = Int32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int32'.");
    }

    public static void ToInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Int32 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsInt(style, provider) ? result = Int32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int32'.");
    }

    public static UInt32 ToUInt(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsUInt() ? UInt32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt32'.");
    }

    public static UInt32 ToUInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsUInt(style, provider) ? UInt32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt32'.");
    }

    public static void ToUInt(this String value, out UInt32 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsUInt() ? result = UInt32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt32'.");
    }

    public static void ToUInt(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out UInt32 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsUInt(style, provider) ? result = UInt32.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt32'.");
    }

    public static Int64 ToLong(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsLong() ? Int64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int64'.");
    }

    public static Int64 ToLong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsLong(style, provider) ? Int64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int64'.");
    }

    public static void ToLong(this String value, out Int64 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsLong() ? result = Int64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int64'.");
    }

    public static void ToLong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Int64 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsLong(style, provider) ? result = Int64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Int64'.");
    }

    public static UInt64 ToULong(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsULong() ? UInt64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt64'.");
    }

    public static UInt64 ToULong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsULong(style, provider) ? UInt64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt64'.");
    }

    public static void ToULong(this String value, out UInt64 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsULong() ? result = UInt64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt64'.");
    }

    public static void ToULong(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out UInt64 result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsULong(style, provider) ? result = UInt64.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'UInt64'.");
    }

    public static Single ToFloat(this String value)
    {
        return value.ToSingle();
    }

    public static Single ToFloat(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        return value.ToSingle(style, provider);
    }

    public static void ToFloat(this String value, out Single result)
    {
        value.ToSingle(out result);
    }

    public static void ToFloat(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Single result)
    {
        value.ToSingle(style, provider, out result);
    }

    public static Single ToSingle(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsSingle() ? Single.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Single'.");
    }

    public static Single ToSingle(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsSingle(style, provider) ? Single.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Single'.");
    }

    public static void ToSingle(this String value, out Single result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsSingle() ? result = Single.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Single'.");
    }

    public static void ToSingle(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Single result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsSingle(style, provider) ? result = Single.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Single'.");
    }

    public static Double ToDouble(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsDouble() ? Double.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Double'.");
    }

    public static Double ToDouble(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsDouble(style, provider) ? Double.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Double'.");
    }

    public static void ToDouble(this String value, out Double result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsDouble() ? result = Double.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Double'.");
    }

    public static void ToDouble(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Double result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsDouble(style, provider) ? result = Double.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Double'.");
    }

    public static Decimal ToDecimal(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsDecimal() ? Decimal.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Decimal'.");
    }

    public static Decimal ToDecimal(this String value, System.Globalization.NumberStyles style, IFormatProvider provider)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsDecimal(style, provider) ? Decimal.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Decimal'.");
    }

    public static void ToDecimal(this String value, out Decimal result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsDecimal() ? result = Decimal.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Decimal'.");
    }

    public static void ToDecimal(this String value, System.Globalization.NumberStyles style, IFormatProvider provider, out Decimal result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsDecimal(style, provider) ? result = Decimal.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Decimal'.");
    }

    public static Char ToChar(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsChar() ? Char.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Char'.");
    }

    public static void ToChar(this String value, out Char result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsChar() ? result = Char.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Char'.");
    }

    public static Boolean ToBool(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.IsBool() ? Boolean.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Boolean'.");
    }

    public static void ToBool(this String value, out Boolean result)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        _ = value.IsBool() ? result = Boolean.Parse(value) : throw new InvalidCastException("The input could not be changed into a 'Boolean'.");
    }

    #endregion

    #region Return: String

    public static String ToAcronym(this String value, Boolean onlyPrincipalWords = true)
    {
        if (value.IsNullOrEmpty())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.ToAcronym(System.Globalization.CultureInfo.CurrentCulture, onlyPrincipalWords);
    }

    public static String ToAcronym(this String value, System.Globalization.CultureInfo culture, Boolean onlyPrincipalWords = true)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        else if (culture == default)
            throw new ArgumentNullException(nameof(culture));
        // ----------------------------------------------------------------------------------------------------
        onlyPrincipalWords = onlyPrincipalWords && AT.Infrastructure.CultureInfoData.InitializeCultureData(culture);
        return AlphaNumeric().Replace(value.ToAlphabetic(), new System.Text.RegularExpressions.MatchEvaluator(WordEvaluator)).RemoveWhitespace();
        String WordEvaluator(System.Text.RegularExpressions.Match word)
        {
            String lower = word.Value.ToLower();

            return String.IsNullOrWhiteSpace(word.Value)
                    || onlyPrincipalWords && (AT.Infrastructure.CultureInfoData.InfoData.data?.Articles?.Contains(lower) ?? false)
                    || (AT.Infrastructure.CultureInfoData.InfoData.data?.Conjunctions?.Contains(lower) ?? false)
                    || (AT.Infrastructure.CultureInfoData.InfoData.data?.Prepositions?.Contains(lower) ?? false)
                    ? String.Empty : word.Value.Substring(0, 1).ToUpper();
        }
    }

    public static String ToAlphabetic(this String value, Boolean preserveWhitespace = true)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();
        value.Where(current => current.IsLetter() || (current.IsWhiteSpace() && preserveWhitespace)).ToList().ForEach(search => result.Append(search));
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String ToAlphanumeric(this String value, Boolean preserveWhitespace = true)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();
        value.Where(current => current.IsLetterOrDigit() || (current.IsWhiteSpace() && preserveWhitespace)).ToList().ForEach(search => result.Append(search));
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String ToBase64String(this String value, System.Text.Encoding? encoding = default)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        encoding ??= System.Text.Encoding.UTF8;
        return Convert.ToBase64String(encoding.GetBytes(value));
    }

    public static String ToBase64String(this byte[] values)
    {
        if (values is null || values == default)
            throw new ArgumentNullException(nameof(values));
        // ----------------------------------------------------------------------------------------------------
        return Convert.ToBase64String(values);
    }

    public static String ToBase64UrlString(this byte[]? values, Boolean removePadding = true)
    {
        if (values is null || values == default)
            throw new ArgumentNullException(nameof(values));
        // ----------------------------------------------------------------------------------------------------
        String base64Uri = Convert.ToBase64String(values).Replace("+", "-").Replace("/", "_");
        return removePadding
               ? base64Uri.TrimEnd('=')
               : base64Uri.Replace("=", "%3D");
    }

    public static String ToCamelCase(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return AlphaNumeric().Replace(value, new System.Text.RegularExpressions.MatchEvaluator(WordEvaluator)).ToAlphanumeric(false);
        static String WordEvaluator(System.Text.RegularExpressions.Match word)
        {
            return word.Value.ToSentenceCase();
        }
    }

    public static String ToCamelCase2(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.DetectAndNormalize().FirstLetterLower();
    }

    public static String ToCommaSeparated(this List<String> values)
    {
        if (values is null || values == default)
            throw new ArgumentNullException(nameof(values));
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
        if (values is null || values == default)
            throw new ArgumentNullException(nameof(values));
        // ----------------------------------------------------------------------------------------------------
        String value = String.Empty;
        if (values is not null)
            if (values.Any())
                value = String.Join(separator, values);
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String ToEndTrimmedString(this object input)
    {
        return input.ToString().TrimEnd();
    }

    public static String ToFormat(this String stringFormat, params object[] args)
    {
        return String.Format(stringFormat, args);
    }

    public static String ToFullPath(this String path)
    {
        return Path.GetFullPath(path);
    }

    public static String ToGmtFormattedDate(this DateTime date)
    {
        return date.ToString("yyyy'-'MM'-'dd hh':'mm':'ss tt 'GMT'");
    }

    public static String ToHash(this String text)
    {
        var parts = System.Security.Cryptography.MD5.Create().ComputeHash(System.Text.Encoding.UTF8.GetBytes(text)).Select(b => b.ToString("x2"));
        return String.Join("", parts);
    }

    public static String ToHexString(this byte[]? value, Boolean upperCase = true)
    {
        if (value is null)
            return String.Empty;

        var mapping = upperCase ? NibbleToStringMappingUpperCase : NibbleToStringMappingLowerCase;
        var hexString = new Char[value.Length * 2];

        var charIndex = 0;
        for (var i = 0; i < value.Length; i += 1, charIndex += 2)
        {
            hexString[charIndex] = mapping[value[i] >> 4];
            hexString[charIndex + 1] = mapping[value[i] & 0x0F];
        }

        return new String(hexString);
    }

    public static String ToHumanReadableFileSize(this Int64 input, Boolean binary = true)
    {
        Boolean negative = false;
        if (input < 0) { input *= -1; negative = true; }

        Double value = input;
        Double divisor = binary ? 1024d : 1000d;
        Int32 idx = 0;
        Int32 maxIdx = binary
            ? BinaryMeasurementSuffixes.Length
            : DecimalMeasurementSuffixes.Length;

        while (value >= divisor && (idx + 1) < maxIdx)
        {
            value /= divisor;
            idx++;
        }

        return (negative ? "-" : String.Empty)
            + value.ToString(idx > 0 ? "0.00" : "0") + " " +
            (
                binary
                ? BinaryMeasurementSuffixes[idx]
                : DecimalMeasurementSuffixes[idx]
            );
    }

    public static String ToHungarianNotation(this String str)
    {
        return String.Join("", str
            .DetectAndNormalize()
            .Select(x => x.IsUpper() ? $"_{x.ToLower()}" : x.ToString()))
            .Trim('_');
    }

    public static String ToInsecureString(this System.Security.SecureString secureString)
    {
        Enable.Common.Argument.IsNotNull(secureString, nameof(secureString));

        IntPtr ptr = IntPtr.Zero;

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
        return date.ToString(format, System.Globalization.CultureInfo.InvariantCulture);
    }

    public static String ToInvariantString(this DateTime? date, String format = "dd/MM/yyyy")
    {
        if (date == default)
            return String.Empty;
        else
            return date.Value.ToInvariantString(format);
    }

    public static String ToKebabCase(this String str)
    {
        return String.Join("", str
            .DetectAndNormalize()
            .Select(x => x.IsUpper() ? $"-{x.ToLower()}" : x.ToString()))
            .Trim('-');
    }

    public static String ToMorseCode(this String str, Boolean translateSpace = true)
    {
        System.Text.StringBuilder morseStringBuilder = new System.Text.StringBuilder();

        Dictionary<Char, String> morseCharacters = new Dictionary<Char, String>()
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
        foreach (Char c in str.ToLower())
        {
            //if there is a corresponding character-Key in dictionary
            if (morseCharacters.TryGetValue(c, out tempMorseStr))
            {
                if (c != ' ')
                {
                    morseStringBuilder.Append(tempMorseStr);
                }
                else
                {
                    if (translateSpace)
                    {
                        morseStringBuilder.Append(tempMorseStr);
                    }
                    else
                    {
                        morseStringBuilder.Append(' ');
                    }
                }
            }
            // if no corresponding key then concat unchanged input character
            else
            {
                morseStringBuilder.Append(c);
            }
        }
        return morseStringBuilder.ToString();
    }

    public static String ToPascal(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        String upperLetter = String.Empty;
        Int32 firstLetterIndex = FindFirstLetterIndex(value);

        if (firstLetterIndex == notFoundIndexValue)
            return value;

        upperLetter = value.Substring(0, firstLetterIndex + 1).ToUpper();
        String unchangedStringPart = String.Empty;

        if (value.Length > firstLetterIndex + 1)
            unchangedStringPart = value.Substring(firstLetterIndex + 1, value.Length - firstLetterIndex - 1);
        // ----------------------------------------------------------------------------------------------------
        return upperLetter + unchangedStringPart;
    }

    public static String ToPascalCase(this String str)
    {
        return str.DetectAndNormalize();
    }

    public static String ToPriceFormat(this object value)
    {
        return Convert.ToDecimal(value).ToString("N0");
    }

    public static String ToSearchableString(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        value = value.ToLower();

        foreach (var rep in ReplaceDictionary.Keys)
            value = value.Replace(rep, ReplaceDictionary[rep]);

        value = value.TrimEveryThing();
        // ----------------------------------------------------------------------------------------------------
        return value;
    }

    public static String ToSentenceCase(this String text, Boolean cleanWhitespace = true)
    {
        if (String.IsNullOrWhiteSpace(text))
            return text;

        if (cleanWhitespace)
            text = text.CleanWhiteSpace1();

        return text.Length > 1 ? text.Substring(0, 1).ToUpper() + text.Substring(1).ToLower() : text.ToUpper();
    }

    public static String ToSentenceCase(this String input)
    {
        try
        {
            String sentence = input.ToLower().TrimStart();
            if (sentence.Length <= 1) return sentence.ToUpper();
            return sentence.Remove(1).ToUpper() + sentence.Substring(1);
        }
        catch (Exception ex)
        {
            Exception except = new Exception(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static String ToSnakeCase(this String text)
    {
        if (String.IsNullOrWhiteSpace(text))
            return text;

        var result = text.ToAlphanumeric();

        return result.ReplaceWhitespace("_");
    }

    public static String ToStartTrimmedString(this object input)
    {
        return input.ToString().TrimStart();
    }

    public static String ToTitleCase(this String text, System.Globalization.CultureInfo culture)
    {
        if (String.IsNullOrWhiteSpace(text))
            return text;

        if (!AT.Infrastructure.CultureInfoData.InitializeCultureData(culture))
            return culture.TextInfo.ToTitleCase(text);
        else
        {
            String result = culture.TextInfo.ToTitleCase(text);

            return System.Text.RegularExpressions.Regex.Replace(result, @"\w+", new System.Text.RegularExpressions.MatchEvaluator(WordEvaluator), System.Text.RegularExpressions.RegexOptions.IgnoreCase);
        }

        String WordEvaluator(System.Text.RegularExpressions.Match word)
        {
            String lower = word.Value.ToLower();

            Boolean lastWord = !word.NextMatch().Success;

            return word.Index > 0 && !lastWord && word.Value.Length <= 3
                    && (AT.Infrastructure.CultureInfoData.InfoData.data?.Articles?.Contains(lower) ?? false)
                    || (AT.Infrastructure.CultureInfoData.InfoData.data?.Conjunctions?.Contains(lower) ?? false)
                    || (AT.Infrastructure.CultureInfoData.InfoData.data?.Prepositions?.Contains(lower) ?? false)
                    ? lower : word.Value;
        }
    }

    public static String ToTitleCase(this String text)
    {
        return ToTitleCase(text, System.Globalization.CultureInfo.CurrentCulture);
    }

    public static String ToTitleCase2(this String source)
    {
        return ToTitleCase2(source, new System.Globalization.CultureInfo("en-US"));
    }

    public static String ToTitleCase2(this String source, System.Globalization.CultureInfo cultureInfo)
    {
        return cultureInfo.TextInfo.ToTitleCase(source.ToLower(cultureInfo));
    }

    public static String ToTitleCase3(this String input)
    {
        try
        {
            input = input.ToLower();
            return Thread.CurrentThread.CurrentCulture.TextInfo.ToTitleCase(input);
        }
        catch (Exception ex)
        {
            Exception except = new Exception(String.Format("Exception throw in {0}.{1} -- Inner Exception:\n  {2}", System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName, System.Reflection.MethodBase.GetCurrentMethod().Name, ex.Message), ex);
            throw except;
        }
    }

    public static String ToTrimmedString(this object input)
    {
        return input.ToString().Trim();
    }

    public static String ToValidFilename(this String s)
    {
        Char[] invalid = Path.GetInvalidFileNameChars();
        String invalidString = new String(invalid);
        Char[] titleChars = s.ToCharArray();
        Char[] output = new Char[s.Length];
        Int32 outputLength = 0;
        foreach (Char input in titleChars)
        {
            if (invalidString.IndexOf(input) == -1)
            {
                output[outputLength] = input;
                outputLength++;
            }
        }
        String rawR = new String(output, 0, outputLength);
        String r = rawR.Trim();
        if (r == "")
        {
            r = "NonAlphaName";
        }
        return r;
    }

    public static String ToWords(this String source)
    {
        return System.Text.RegularExpressions.Regex.Replace(source, @"((?<=\p{Ll})\p{Lu})|((?!\A)\p{Lu}(?>\p{Ll}))", " $0");
    }

    public static String? ToXmlFormat(this String instance)
    {
        if (instance.IsNullOrEmpty() || instance.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(instance));
        // ----------------------------------------------------------------------------------------------------
        if (instance.TrimStart().StartsWith("<"))
            return System.Xml.Linq.XDocument.Parse(instance).ToString();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    #endregion

    public static dynamic JsonToExpanderObject(this String json)
    {
        Newtonsoft.Json.Converters.ExpandoObjectConverter converter = new();
        return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Dynamic.ExpandoObject>(json, converter);
    }

    public static System.Text.Encoding ToEncoding(this String str)
    {
        return str.ToUpper(System.Globalization.CultureInfo.InvariantCulture) switch
        {
            "ASCII" => System.Text.Encoding.ASCII,
            "UTF8" => System.Text.Encoding.UTF8,
            "UTF32" => System.Text.Encoding.UTF32,
            "UNICODE" => System.Text.Encoding.Unicode,
            _ => throw new ArgumentException("Argument not supported", nameof(str)),
        };
    }

    public static System.Security.SecureString ToSecureString(this String plainText)
    {
        Enable.Common.Argument.IsNotNull(plainText, nameof(plainText));

        System.Security.SecureString secureString = new();

        foreach (Char c in plainText)
            secureString.AppendChar(c);

        secureString.MakeReadOnly();

        return secureString;
    }
}