namespace AT.Extensions.Strings.Collections;
public static class UniqueExtensions : Object
{
    public static IEnumerable<String> UniqueWords(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();
        // ----------------------------------------------------------------------------------------------------
        return value
               .Words()?
               .GroupBy(word => word)
               .Select(wordGroup => wordGroup.Key)
               ?? defaultValue;
    }
}