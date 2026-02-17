namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class AnagramExtensions
{
    public static String? Anagram(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray()).ToUpper();
    }
}