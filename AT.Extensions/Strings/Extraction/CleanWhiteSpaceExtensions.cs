namespace AT.Extensions.Strings.Extraction;
public static class CleanWhiteSpaceExtensions
{
    public static String CleanWhiteSpace(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        string stringWithSpaces = new(value.Trim().ToCharArray()
                                  .Select(c => Char.IsWhiteSpace(c) ? ' ' : c)
                                  .ToArray());
        // ----------------------------------------------------------------------------------------------------
        while (stringWithSpaces.Contains("  "))
            stringWithSpaces = stringWithSpaces.Replace("  ", " ");
        // ----------------------------------------------------------------------------------------------------
        return stringWithSpaces;
    }
}