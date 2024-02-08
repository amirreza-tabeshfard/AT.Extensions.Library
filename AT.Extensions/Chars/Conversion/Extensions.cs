namespace AT.Extensions.Chars.Conversion;
public static class Extensions : Object
{
    public static char ToChar(this String input)
    {
        if (string.IsNullOrEmpty(input))
            throw new ArgumentNullException(nameof(input));
        // ----------------------------------------------------------------------------------------------------
        if (char.TryParse(input, out char result))
            return result;
        // ----------------------------------------------------------------------------------------------------
        throw new InvalidCastException("The input could not be changed into a char.");
    }

    public static char ToLower(this char ch)
    {
        return char.ToLower(ch);
    }

    public static char ToLowerInvariant(this char ch)
    {
        return char.ToLowerInvariant(ch);
    }

    public static char ToUpper(this char ch)
    {
        return char.ToUpper(ch);
    }

    public static char ToUpperInvariant(this char ch)
    {
        return char.ToUpperInvariant(ch);
    }
}