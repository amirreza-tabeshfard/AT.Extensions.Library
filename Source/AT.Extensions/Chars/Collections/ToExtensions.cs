using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Chars.Collections;
public static class ToExtensions
{
    public static Char[] ToArray(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.ToCharArray();
    }

    public static List<Char> ToList(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return new List<Char>(value.ToCharArray());
    }
}