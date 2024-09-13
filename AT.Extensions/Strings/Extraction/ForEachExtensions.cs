namespace AT.Extensions.Strings.Extraction;
public static class ForEachExtensions : Object
{
    public static void ForEach(this String value, Action<Char> action)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        foreach (Char character in value)
            action(character);
    }
}