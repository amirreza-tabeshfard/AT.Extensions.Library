using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Chars.Conversion;
public static class ToExtensions
{
    public static Char ToChar(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        if (Char.TryParse(value, out Char result))
            return result;
        // ----------------------------------------------------------------------------------------------------
        throw new InvalidCastException("The input could not be changed into a Char.");
    }

    public static Char ToLower(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.ToLower(ch);
    }

    public static Char ToLowerInvariant(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.ToLowerInvariant(ch);
    }

    public static Char ToUpper(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.ToUpper(ch);
    }

    public static Char ToUpperInvariant(this Char ch)
    {
        if (ch == default)
            throw new ArgumentNullException(nameof(ch));
        // ----------------------------------------------------------------------------------------------------
        return Char.ToUpperInvariant(ch);
    }
}