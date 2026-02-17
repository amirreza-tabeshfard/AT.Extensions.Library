namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class AsBoolExtensions
{
    public static Boolean AsBool(this String value, Boolean defaultValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        Boolean result;
        // ----------------------------------------------------------------------------------------------------
        if (!Boolean.TryParse(value, out result))
            return defaultValue;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean AsBool(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.AsBool(false);
    }
}