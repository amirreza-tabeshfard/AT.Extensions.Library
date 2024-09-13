namespace AT.Extensions.Strings.Collections;
public static class RangeExtensions : Object
{
    public static IEnumerable<String> Range(this String value, Int32 start, Int32 end, params Char[] split)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value
               .Split(split)
               .Skip(start)
               .Take(end - start + 1);
    }

    public static IEnumerable<String> Range(this String value, Int32 start, Int32 end, params String[] split)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return value
               .Split(split, StringSplitOptions.RemoveEmptyEntries)
               .Skip(start)
               .Take(end - start);
    }
}