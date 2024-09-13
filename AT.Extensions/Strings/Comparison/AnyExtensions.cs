namespace AT.Extensions.Strings.Comparison;
public static class AnyExtensions : Object
{
    public static Boolean Any(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Length != 0;
    }
}