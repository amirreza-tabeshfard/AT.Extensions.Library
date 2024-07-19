namespace AT.Extensions.Strings.Extraction;
public static class LongExtensions : Object
{
    public static Int64 AsLong(this String value)
    {
        return value.AsLong(0);
    }

    public static Int64 AsLong(this String value, Int32 defaultValue)
    {
        Int64 result;
        if (!Int64.TryParse(value, out result))
            return defaultValue;
        return result;
    }

    public static Int64 Length(this String source)
    {
        if (source == default)
        {
            throw new ArgumentNullException("source");
        }
        return source.Length;
    }
}