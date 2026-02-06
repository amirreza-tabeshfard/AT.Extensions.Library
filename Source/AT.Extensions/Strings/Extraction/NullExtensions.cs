using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.Strings.Extraction;
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