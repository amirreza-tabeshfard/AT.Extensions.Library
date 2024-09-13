namespace AT.Extensions.Strings.Comparison;
public static class AsBoolExtensions : Object
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