namespace AT.Extensions.Strings.Extraction;
public static class LongExtensions : Object
{
    public static long AsLong(this String value)
    {
        return value.AsLong(0);
    }

    public static long AsLong(this String value, int defaultValue)
    {
        long result;
        if (!long.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static long Length(this String source)
    {
        if (source == null)
        {
            throw new ArgumentNullException("source");
        }
        return source.Length;
    }
}