namespace AT.Extensions.Strings.Collections;
public static class CountWordExtensions : Object
{
    public static IOrderedEnumerable<(Int32 Length, Int32 Count)> CountWordLengths(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IOrderedEnumerable<(int Length, int Count)> defaultValue = Enumerable
                                                                   .Empty<(Int32 Length, Int32 Count)>()
                                                                   .OrderBy(wordLength => wordLength.Length);

        IEnumerable<IGrouping<int, string>>? lengths = value
                                                       .Words()?
                                                       .GroupBy(word => word.Length);

        IOrderedEnumerable<(int Length, int Count)> result = lengths
                                                             ?.Select(lengthGroup => (Length: lengthGroup.Key, Count: lengthGroup.Count()))
                                                             .OrderBy(wordLength => wordLength.Length) 
                                                             ?? defaultValue;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}