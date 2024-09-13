namespace AT.Extensions.Strings.Comparison;
public static class EqualsExtensions : Object
{
    public static Boolean EqualsIgnoreCase(this String valueFirst, String valueSecond)
    {
        ArgumentException.ThrowIfNullOrEmpty(valueFirst);
        ArgumentException.ThrowIfNullOrEmpty(valueSecond);
        // ----------------------------------------------------------------------------------------------------
        return valueFirst.Equals(valueSecond, StringComparison.CurrentCultureIgnoreCase);
    }
}