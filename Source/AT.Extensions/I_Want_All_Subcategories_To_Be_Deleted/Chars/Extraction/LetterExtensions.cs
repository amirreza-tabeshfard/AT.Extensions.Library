using AT.Extensions.Strings.Comparison;
using AT.Extensions.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Extraction;
public static class LetterExtensions
{
    public static Char? FirstLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        String letters = value.KeepLettersOnly();
        if (!letters.IsNullOrEmpty() || !letters.IsNullOrWhiteSpace())
            return letters.GetFirst();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    public static Char? LastLetter(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        String letters = value.KeepLettersOnly();
        if (!letters.IsNullOrEmpty() || !letters.IsNullOrWhiteSpace())
            return letters.GetLast();
        // ----------------------------------------------------------------------------------------------------
        return default;
    }
}