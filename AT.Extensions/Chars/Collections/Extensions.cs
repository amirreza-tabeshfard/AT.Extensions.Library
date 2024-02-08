using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Chars.Collections;
public static class Extensions : Object
{
    public static char[] ToArray(this String source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return source.ToCharArray();
    }

    public static List<char> ToList(this String value)
    {
        if (value.IsNullOrEmpty() || value.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return new List<char>(value.ToCharArray());
    }
}