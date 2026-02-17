namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class AllBetweenExtensions
{
    #region Method(s): Private

    private static IEnumerable<String> AllBetweenCore(this String input, Char firstEnclosureCharacter, Char secondEnclosureCharacter)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        // ----------------------------------------------------------------------------------------------------
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
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(firstEnclosure);
        ArgumentException.ThrowIfNullOrEmpty(secondEnclosure);
        // ----------------------------------------------------------------------------------------------------
        Int32 firstEnclosureIndex = input.IndexOf(firstEnclosure, comparisonType);
        while (firstEnclosureIndex != -1 && firstEnclosureIndex + firstEnclosure.Length < input.Length)
        {
            Int32 firstAdjustedIndex = firstEnclosureIndex + firstEnclosure.Length;
            Int32 secondEnclosureIndex = input.IndexOf(secondEnclosure, firstAdjustedIndex, comparisonType);
            
            if (secondEnclosureIndex == -1)
                break;
            else
            {
                Int32 length = secondEnclosureIndex - firstAdjustedIndex;

                String substring = input.Substring(firstAdjustedIndex, length);

                yield return substring;

                firstEnclosureIndex = input.IndexOf(firstEnclosure, secondEnclosureIndex + secondEnclosure.Length);
            }
        }
    }

    #endregion

    public static IEnumerable<String> AllBetween(this String input, Char enclosureCharacter)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        // ----------------------------------------------------------------------------------------------------
        return AllBetween(input, enclosureCharacter, enclosureCharacter);
    }

    public static IEnumerable<String> AllBetween(this String input, Char firstEnclosureCharacter, Char secondEnclosureCharacter)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        // ----------------------------------------------------------------------------------------------------
        return AllBetweenCore(input, firstEnclosureCharacter, secondEnclosureCharacter);
    }

    public static IEnumerable<String> AllBetween(this String input, String enclosure)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(enclosure);
        // ----------------------------------------------------------------------------------------------------
        return AllBetween(input, enclosure, StringComparison.Ordinal);
    }

    public static IEnumerable<String> AllBetween(this String input, String enclosure, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(enclosure);
        // ----------------------------------------------------------------------------------------------------
        return AllBetween(input, enclosure, enclosure, comparisonType);
    }

    public static IEnumerable<String> AllBetween(this String input, String firstEnclosure, String secondEnclosure, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(firstEnclosure);
        ArgumentException.ThrowIfNullOrEmpty(secondEnclosure);
        // ----------------------------------------------------------------------------------------------------
        return AllBetweenImpl(input, firstEnclosure, secondEnclosure, comparisonType);
    }
}