namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class AnyExtensions
{
    public static Boolean Any(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length != 0;
    }
}