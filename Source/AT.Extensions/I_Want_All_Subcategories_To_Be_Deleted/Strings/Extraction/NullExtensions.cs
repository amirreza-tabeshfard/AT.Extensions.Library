using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class NullExtensions
{
    public static String? NullIfEmpty(this String value)
    {
        return value.IsNullOrEmpty() ? default : value;
    }

    public static String? NullIfEmptyOrWhiteSpace(this String value)
    {
        return value.IsNullOrWhiteSpace() ? default : value;
    }
}