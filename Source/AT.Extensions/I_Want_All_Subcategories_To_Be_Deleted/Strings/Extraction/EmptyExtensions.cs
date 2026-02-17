namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
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