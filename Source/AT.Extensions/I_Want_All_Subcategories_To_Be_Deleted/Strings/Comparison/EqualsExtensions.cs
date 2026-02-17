namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class EqualsExtensions
{
    public static Boolean EqualsIgnoreCase(this String valueFirst, String valueSecond)
    {
        ArgumentException.ThrowIfNullOrEmpty(valueFirst);
        ArgumentException.ThrowIfNullOrEmpty(valueSecond);
        // ----------------------------------------------------------------------------------------------------
        return valueFirst.Equals(valueSecond, StringComparison.CurrentCultureIgnoreCase);
    }
}