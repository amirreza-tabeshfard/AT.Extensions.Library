using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
public static class PrefixExtensions
{
    public static Int32 PrefixCount(this String value, String prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(prefix);
        // ----------------------------------------------------------------------------------------------------
        Int32 result = default;
        // ----------------------------------------------------------------------------------------------------
        while (value.StartsWithInvariant(prefix))
        {
            value = value.Substring(prefix.Length);
            result++;
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}