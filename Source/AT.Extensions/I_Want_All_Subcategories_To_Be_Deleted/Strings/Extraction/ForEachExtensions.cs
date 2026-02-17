namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class ForEachExtensions
{
    public static void ForEach(this String value, Action<Char> action)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        foreach (Char character in value)
            action(character);
    }
}