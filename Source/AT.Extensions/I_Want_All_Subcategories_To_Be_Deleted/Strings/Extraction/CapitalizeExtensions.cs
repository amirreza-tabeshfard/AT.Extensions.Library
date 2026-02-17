using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Extraction;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class CapitalizeExtensions
{
    public static String Capitalize(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new(value);
        result[0] = Char.ToUpper(result[0]);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 1; i < result.Length; ++i)
        {
            if (Char.IsWhiteSpace(result[i - 1]) && !Char.IsWhiteSpace(result[i]))
                result[i] = Char.ToUpper(result[i]);
        }
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String CapitalizeAfterCharacter(this String value, Char separator, Boolean minimizeOtherChar = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return CapitalizeAfterCharacters(value, new[] { separator }, minimizeOtherChar);
    }

    public static String CapitalizeAfterCharacters(this String value, IEnumerable<Char> separators, Boolean minimizeOtherChar = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Char[] stringToCapitalize = value.ToCharArray();
        if (!separators.Any() || !stringToCapitalize.Intersect(separators).Any())
        {
            string firstChar = value.Substring(0, 1).ToUpper();
            string otherChars = value.Substring(1, value.Length - 1);
            if (minimizeOtherChar)
                otherChars = otherChars.ToLower();
            return firstChar + otherChars;
        }
        else
        {
            System.Text.StringBuilder stringCapitalized = new System.Text.StringBuilder();
            Char lastCharacter = separators.First();
            foreach (Char character in stringToCapitalize)
            {
                if (separators.Contains(lastCharacter))
                    stringCapitalized.Append(Char.ToUpper(character));
                else
                    stringCapitalized.Append(minimizeOtherChar ? Char.ToLower(character) : character);

                lastCharacter = character;
            }
            return stringCapitalized.ToString();
        }
    }

    public static String CapitalizeEachLine(this String value, Boolean minimizeOtherChar = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<string> sentencesTrimmed = value.Split('\n').Select(sentence => sentence.Trim());
        string textTrimed = String.Join('\n', sentencesTrimmed);
        // ----------------------------------------------------------------------------------------------------
        return textTrimed.CapitalizeAfterCharacter('\n', minimizeOtherChar);
    }

    public static String CapitalizeEachWord(this String value, Boolean minimizeOtherChar = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.CapitalizeAfterCharacters(new Char[] { ' ', '\t', '\n', '\r' }, minimizeOtherChar);
    }

    public static String CapitalizeFirstLetter(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.GetFirst().ToString().ToUpper() + value.Substring(1);
    }

    public static String CapitalizeFirstLetters(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String[] splittedMessage = value.Split(' ');
        System.Text.StringBuilder result = new();
        foreach (String word in splittedMessage)
        {
            String firstLetter = word[0].ToString().ToUpper();
            String otherLetters = word.Substring(1, word.Length - 1);
            result.Append(firstLetter + otherLetters + " ");
        }
        result.Remove(result.Length - 1, 1);
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }
}