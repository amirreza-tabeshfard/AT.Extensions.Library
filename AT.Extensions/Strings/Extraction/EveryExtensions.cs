namespace AT.Extensions.Strings.Extraction;
public static  class EveryExtensions
{
    public static String EveryLetterLower(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(" ", value.Split(' ').Select(x => x.FirstLetterLower()));
    }

    public static String EveryLetterUpper(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(" ", value.Split(' ').Select(x => x.FirstLetterUpper()));
    }

    public static String EveryWordUpper(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(" ", value.Split(' ').Select(x => x.FirstWordUpper()));
    }
}