namespace AT.Extensions.Strings.Extraction;
public static class EmptyExtensions
{
    public static String EmptyIfNull(this String? value)
    {
        return value ?? String.Empty;
    }

    public static String EmptyIfNullOrWhiteSpace(this String? value)
    {
        return String.IsNullOrWhiteSpace(value) ? String.Empty : value;
    }
}