namespace AT.Extensions.Strings.Extraction;
public static class AnagramExtensions : Object
{
    public static String? Anagram(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return new String(value.ToCharArray().OrderBy(x => Guid.NewGuid()).ToArray()).ToUpper();
    }
}