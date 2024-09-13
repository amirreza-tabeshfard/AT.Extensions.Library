namespace AT.Extensions.Strings.Collections.Generic;
public static class SplitToExtensions : Object
{
    public static IEnumerable<T> SplitTo<T>(this String value, params Char[] separator) 
        where T : IConvertible
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Split(separator, StringSplitOptions.None).Select(s => (T)Convert.ChangeType(s, typeof(T)));
    }

    public static IEnumerable<T> SplitTo<T>(this String value, StringSplitOptions options, params Char[] separator)
        where T : IConvertible
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value.Split(separator, options).Select(s => (T)Convert.ChangeType(s, typeof(T)));
    }
}