using AT.Extensions.Strings.Collections;

namespace AT.Extensions.Strings.Extraction;
public static class CamelCaseExtensions : Object
{
    public static String CamelCaseEveryChar(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();
        Int32 counter = default;
        // ----------------------------------------------------------------------------------------------------
        foreach (Char character in value)
        {
            result.Append(counter % 2 == 0 ? character.ToString().ToUpper() : character.ToString().ToLower());
            counter++;
        }
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String CamelCaseEveryLetter(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String[] splittedMessage = value.Split(' ');
        System.Text.StringBuilder result = new();
        // ----------------------------------------------------------------------------------------------------
        foreach (String word in splittedMessage)
        {
            for (Int32 i = 0; i < word.Length; i++)
                result.Append(i % 2 == 0 ? word[i].ToString().ToUpper() : word[i].ToString().ToLower());

            result.Append(' ');
        }
        // ----------------------------------------------------------------------------------------------------
        result.Remove(result.Length - 1, 1);
        // ----------------------------------------------------------------------------------------------------
        return result.ToString();
    }

    public static String CamelCaseToHumanCase(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<String> words = value.SplitCamelCase();
        String humanCased = String.Join(" ", words);
        // ----------------------------------------------------------------------------------------------------
        return humanCased;
    }
}