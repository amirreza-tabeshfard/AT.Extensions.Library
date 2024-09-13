namespace AT.Extensions.Strings.Extraction;
public static class ReverseExtensions : Object
{
    public static String Reverse(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        ICollection<Char> reversedCharacters = new List<Char>();
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = value.Length - 1; i >= 0; i--)
            reversedCharacters.Add(value[i]);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(String.Empty, reversedCharacters);
    }

    public static String Reverse(this String value, Int32 startIndex, Int32 count)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (startIndex < 0 || startIndex >= value.Length)
            throw new ArgumentOutOfRangeException(nameof(startIndex), "startIndex should be between 0 and input.Length");

        if (count < 0 || count > value.Length - startIndex)
            throw new ArgumentOutOfRangeException(nameof(count), "count should be larger or equal to 0 and smaller than input.Length - startIndex");
        // ----------------------------------------------------------------------------------------------------
        String result = value;
        // ----------------------------------------------------------------------------------------------------
        if (count > 0)
        {
            Char[] characters = value.ToCharArray();
            // ----------------------------------------------------------------------------------------------------
            Array.Reverse(characters, startIndex, count);
            // ----------------------------------------------------------------------------------------------------
            result = new String(characters);
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String ReverseSlash(this String value, Int32 direction)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return direction switch
        {
            0 => value.Replace(@"/", @"\"),
            1 => value.Replace(@"\", @"/"),
            _ => value,
        };
    }

    public static String ReverseString(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String result = string.Empty;
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = value.Length - 1; i >= 0; i--)
            result += value[i];
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}