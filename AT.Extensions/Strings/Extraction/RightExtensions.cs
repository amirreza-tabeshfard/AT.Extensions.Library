namespace AT.Extensions.Strings.Extraction;
public static class RightExtensions : Object
{
    public static String Right(this String value, Int32 length)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (length >= value.Length)
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(value.Length - length);
    }

    public static String? Right(this String value, Int32 length, Boolean withEllipsis = false)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= length)
            return value;
        // ----------------------------------------------------------------------------------------------------
        if (withEllipsis)
            return string.Concat(".", value.AsSpan(value.Length - length + 1));
        // ----------------------------------------------------------------------------------------------------
        return value.Substring(value.Length - length);
    }

    public static String RightOf(this String value, Char character)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return RightOf(value, character, 0);
    }

    public static String RightOf(this String value, Char character, Int32 skip)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (skip < 0)
            throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result;
        // ----------------------------------------------------------------------------------------------------
        if (value.Length <= skip)
            result = value;
        else
        {
            Int32 characterPosition = value.Length;
            Int32 foundCharacters = -1;
            // ----------------------------------------------------------------------------------------------------
            while (foundCharacters < skip)
            {
                characterPosition = value.LastIndexOf(character, characterPosition - 1);
                if (characterPosition == -1)
                    break;
                else
                {
                    foundCharacters++;

                    if (characterPosition == 0)
                        break;
                }
            }
            // ----------------------------------------------------------------------------------------------------
            if (characterPosition == -1)
                result = value;
            else
                result = value.Substring(characterPosition + 1);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RightOf(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return RightOf(input, value, StringComparison.Ordinal);
    }

    public static String RightOf(this String input, String value, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return RightOf(input, value, 0, comparisonType);
    }

    public static String RightOf(this String input, String value, Int32 skip, StringComparison comparisonType)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (skip < 0)
            throw new ArgumentOutOfRangeException(nameof(skip), "skip should be larger or equal to 0");
        // ----------------------------------------------------------------------------------------------------
        String result;
        if (input.Length <= skip)
            result = input;
        else
        {
            Int32 valuePosition = -1;
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
                result = input.Substring(valuePosition + value.Length);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RightOfLast(this String value, Char character)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String result;
        Int32 lastCharacterPosition = value.LastIndexOf(character);
        // ----------------------------------------------------------------------------------------------------
        if (lastCharacterPosition == -1)
            result = value;
        else
            result = value.Substring(lastCharacterPosition + 1);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String RightOfLast(this String input, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return RightOfLast(input, value, StringComparison.Ordinal);
    }

    public static String RightOfLast(this String input, String value, StringComparison comparisonType)
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
            result = input.Substring(lastValuePosition + value.Length);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}