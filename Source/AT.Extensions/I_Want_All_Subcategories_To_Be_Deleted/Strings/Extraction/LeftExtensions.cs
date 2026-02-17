namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class LeftExtensions
{
    public static String Left(this String value, Int32 length)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (length <= 0)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be greater than 0.");

        if (length > value.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "The length must be smaller or equal to the length of the input String.");

        if (length < 0 || length > value.Length)
            throw new ArgumentOutOfRangeException(nameof(length), "length cannot be higher than the amount of available characters in the input or lower than 0");

        if (value.Length <= length)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(0, length);
    }

    public static String LeftOf(this String value, Char character)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return LeftOf(value, character, 0);
    }

    public static String LeftOf(this String value, Char character, Int32 skip)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (skip < 0)
            throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result;
        // ----------------------------------------------------------------------------------------------------
        if (value.Length == 0)
            result = value;
        else
        {
            Int32 characterPosition = 0;
            Int32 charactersFound = -1;

            while (charactersFound < skip)
            {
                characterPosition = value.IndexOf(character, characterPosition + 1);
                if (characterPosition == -1)
                    break;
                else
                    charactersFound++;
            }

            if (characterPosition == -1)
                result = value;
            else
                result = value.Substring(0, characterPosition);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String LeftOf(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return LeftOf(input, value, StringComparison.Ordinal);
    }

    public static String LeftOf(this String input, String value, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return LeftOf(input, value, 0, comparisonType);
    }

    public static String LeftOf(this String input, String value, Int32 skip, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (skip < 0)
            throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result;
        // ----------------------------------------------------------------------------------------------------
        if (input.Length <= skip)
            result = input;
        else
        {
            Int32 valuePosition = 0;
            Int32 valuesFound = -1;
            // ----------------------------------------------------------------------------------------------------
            while (valuesFound < skip)
            {
                valuePosition = input.IndexOf(value, valuePosition + 1, comparisonType);
                if (valuePosition == -1)
                    break;
                else
                    valuesFound++;
            }
            // ----------------------------------------------------------------------------------------------------
            if (valuePosition == -1)
                result = input;
            else
                result = input.Substring(0, valuePosition);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String LeftOfLast(this String value, Char character)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String result;
        Int32 lastCharacterPosition = value.LastIndexOf(character);
        // ----------------------------------------------------------------------------------------------------
        if (lastCharacterPosition == -1)
            result = value;
        else
            result = value.Substring(0, lastCharacterPosition);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String LeftOfLast(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return LeftOfLast(input, value, StringComparison.Ordinal);
    }

    public static String LeftOfLast(this String input, String value, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String result;
        Int32 lastValuePosition = input.LastIndexOf(value, comparisonType);
        // ----------------------------------------------------------------------------------------------------
        if (lastValuePosition == -1)
            result = input;
        else
            result = input.Substring(0, lastValuePosition);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}