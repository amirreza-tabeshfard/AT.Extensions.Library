using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Chars.Collections;
public static class Extensions : Object
{
    public static char[] ToArray(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return value.ToCharArray();
    }

    public static List<char> ToList(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return new List<char>(value.ToCharArray());
    }
}