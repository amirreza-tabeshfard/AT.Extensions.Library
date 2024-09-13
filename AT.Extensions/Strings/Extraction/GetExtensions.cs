using AT.Extensions.Chars.Comparison;
using AT.Extensions.Chars.Extraction;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static partial class GetExtensions : object
{
    #region Method(s): Private

    [System.Text.RegularExpressions.GeneratedRegex("([^\\s]+)")]
    private static partial System.Text.RegularExpressions.Regex FirstWord();

    #endregion

    public static String GetArticle(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            return String.Empty;
        else if (value.ToLowerInvariant().Trim()[0].IsVowel() == true)
            return "an";
        // ----------------------------------------------------------------------------------------------------
        return "a";
    }

    public static String GetDefaultIfEmpty(this String value, String defaultValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(defaultValue);
        // ----------------------------------------------------------------------------------------------------
        value = value.Trim();
        // ----------------------------------------------------------------------------------------------------
        return value.Length > 0 ? value : defaultValue;
    }

    public static String GetDigits(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.Where(c => Char.IsDigit(c)).ToArray());
    }

    public static String GetEmptyStringIfNull(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value != null ? value.Trim() : "";
    }

    public static String GetExtension(this String value, Char Delimeter = '.')
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(value.LastIndexOf(Delimeter) + 1);
    }

    public static String GetFirstCharactersAfterSeparators(this String value, IEnumerable<Char> separators)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!value.ToCharArray().Except(separators).Any())
            return String.Empty;
        // ----------------------------------------------------------------------------------------------------
        if (!separators.Any())
            return value.GetFirst().ToString();
        // ----------------------------------------------------------------------------------------------------
        String[] words = value.Split(separators.ToArray(), StringSplitOptions.RemoveEmptyEntries);
        IEnumerable<Char> firstCharacters = words.Select(word => word[0]);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(String.Empty, firstCharacters);
    }

    public static String GetFirstLetterOfEachWord(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Char[] separators = new[] { ' ', '\t', '\n', '\r' };
        return value.GetFirstLettersAfterSeparators(separators);
    }

    public static String GetFirstLettersAfterSeparators(this String value, IEnumerable<Char> separators)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (!value.ToCharArray().Except(separators).Any())
            return String.Empty;

        if (!separators.Any())
            return value.FirstLetter()?.ToString() ?? String.Empty;

        IEnumerable<Char?> firstLetters = value.Split(separators.ToArray())
                                               .Select(word => word.FirstLetter());
        // ----------------------------------------------------------------------------------------------------
        return String.Join(String.Empty, firstLetters);
    }

    public static String GetFirstWord(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return FirstWord().Match(value).Value;
    }

    public static String GetLetterOrDigit(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.Where(c => Char.IsLetterOrDigit(c)).ToArray());
    }

    public static String GetLetters(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.Where(c => Char.IsLetter(c)).ToArray());
    }

    public static String GetLettersAndSpaces(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.Where(c => Char.IsLetter(c) || Char.IsWhiteSpace(c)).ToArray());
    }

    public static String? GetNextToken(this String value, Func<Char, bool> predicate, ref int pos)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        // Skip delimiters
        while (pos < value.Length && predicate(value[pos]))
            pos++;

        // Parse token
        int start = pos;
        while (pos < value.Length && !predicate(value[pos]))
            pos++;

        // Extract token
        if (pos > start)
            return value[start..pos];
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static String? GetNextToken(this String value, String delimiterChars, ref int pos, bool ignoreCase = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(delimiterChars);
        // ----------------------------------------------------------------------------------------------------
        HashSet<Char> hashSet = new(delimiterChars, StartsWithExtensions.CharComparer.GetEqualityComparer(ignoreCase));
        // ----------------------------------------------------------------------------------------------------
        return value.GetNextToken(hashSet.Contains, ref pos);
    }

    public static String? GetNullIfEmptyString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        value = value.Trim();
        if (value.Length > 0)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static String GetNumbers(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value?.Where(c => Char.IsDigit(c)).ToArray());
    }

    public static String GetSafeString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (String.IsNullOrWhiteSpace(value)) 
            return String.Empty;

        Byte[] bytes = System.Text.Encoding.Default.GetBytes(value);
        value = System.Text.Encoding.UTF8.GetString(bytes);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.Where(c => Char.IsLetterOrDigit(c) || Char.IsWhiteSpace(c) || Char.IsPunctuation(c) || Char.IsControl(c)).ToArray());
    }
}