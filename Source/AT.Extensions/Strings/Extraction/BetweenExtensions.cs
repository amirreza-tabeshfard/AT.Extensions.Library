namespace AT.Extensions.Strings.Extraction;
public static class BetweenExtensions
{
    public static String? Between(this String value, Char enclosureCharacter)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return Between(value, enclosureCharacter, enclosureCharacter);
    }

    public static String? Between(this String value, Char firstEnclosureCharacter, Char secondEnclosureCharacter)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String? result;
        // ----------------------------------------------------------------------------------------------------
        Int32 firstEnclosureCharacterIndex = value.IndexOf(firstEnclosureCharacter);
        if (firstEnclosureCharacterIndex == -1 || firstEnclosureCharacterIndex == value.Length - 1)
            result = default;
        else
        {
            Int32 firstAdjustedIndex = firstEnclosureCharacterIndex + 1;
            Int32 secondEnclosureCharacterIndex = value.IndexOf(secondEnclosureCharacter, firstAdjustedIndex);
            if (secondEnclosureCharacterIndex == -1)
                result = default;
            else
            {
                Int32 length = secondEnclosureCharacterIndex - firstAdjustedIndex;
                result = value.Substring(firstAdjustedIndex, length);
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String? Between(this String value, String enclosure)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(enclosure);
        // ----------------------------------------------------------------------------------------------------
        return Between(value, enclosure, StringComparison.Ordinal);
    }

    public static String? Between(this String input, String enclosure, StringComparison comparisonType)
    {
        return Between(input, enclosure, enclosure, comparisonType);
    }

    public static String? Between(this String value, String firstEnclosure, String secondEnclosure, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(firstEnclosure);
        ArgumentException.ThrowIfNullOrEmpty(secondEnclosure);
        // ----------------------------------------------------------------------------------------------------
        String? result;
        // ----------------------------------------------------------------------------------------------------
        Int32 firstEnclosureIndex = value.IndexOf(firstEnclosure, comparisonType);
        if (firstEnclosureIndex == -1 || firstEnclosureIndex + firstEnclosure.Length == value.Length)
            result = default;
        else
        {
            Int32 firstAdjustedIndex = firstEnclosureIndex + firstEnclosure.Length;
            Int32 secondEnclosureIndex = value.IndexOf(secondEnclosure, firstAdjustedIndex, comparisonType);
            // ----------------------------------------------------------------------------------------------------
            if (secondEnclosureIndex == -1)
                result = default;
            else
            {
                Int32 length = secondEnclosureIndex - firstAdjustedIndex;
                result = value.Substring(firstAdjustedIndex, length);
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}