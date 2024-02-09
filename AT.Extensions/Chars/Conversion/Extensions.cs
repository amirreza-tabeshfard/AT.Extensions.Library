using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Chars.Conversion;
public static class Extensions : Object
{
    public static char ToChar(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (char.TryParse(value, out char result))
            return result;
        // ----------------------------------------------------------------------------------------------------
        throw new InvalidCastException("The input could not be changed into a char.");
    }

    public static char ToLower(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.ToLower(ch);
    }

    public static char ToLowerInvariant(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.ToLowerInvariant(ch);
    }

    public static char ToUpper(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.ToUpper(ch);
    }

    public static char ToUpperInvariant(this char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return char.ToUpperInvariant(ch);
    }
}