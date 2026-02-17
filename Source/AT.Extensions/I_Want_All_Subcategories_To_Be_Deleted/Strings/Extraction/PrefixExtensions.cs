using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
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