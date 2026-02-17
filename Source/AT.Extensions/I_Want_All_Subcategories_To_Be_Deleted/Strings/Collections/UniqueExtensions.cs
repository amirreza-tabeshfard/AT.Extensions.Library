namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class UniqueExtensions
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