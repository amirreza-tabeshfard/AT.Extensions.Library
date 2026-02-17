namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class SafeExtensions
{
    public static Int32 SafeIndexOf(this String value, String substring, StringComparison comparison)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(substring);
        // ----------------------------------------------------------------------------------------------------
        return value.IndexOf(substring, comparison);
    }

    public static Int32 SafeLength(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length;
    }
}