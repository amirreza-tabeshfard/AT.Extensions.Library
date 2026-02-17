namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class JoinExtensions
{
    public static String Join(this String[] values, String separator)
    {
        ArgumentException.ThrowIfNullOrEmpty(separator);
        // ----------------------------------------------------------------------------------------------------
        return String.Join(separator, values);
    }

    public static String Join(this IEnumerable<String> values, String separator)
    {
        ArgumentException.ThrowIfNullOrEmpty(separator);
        // ----------------------------------------------------------------------------------------------------
        return Join(values.ToArray(), separator);
    }

    public static String JoinUpLast(this String source, String stringToJoin)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(stringToJoin);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder newString = new(source);
        newString.Append(stringToJoin);
        // ----------------------------------------------------------------------------------------------------
        return newString.ToString();
    }

    public static String JoinUpStart(this String source, String stringToJoin)
    {
        ArgumentException.ThrowIfNullOrEmpty(source);
        ArgumentException.ThrowIfNullOrEmpty(stringToJoin);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder newString = new(stringToJoin);
        newString.Append(source);
        // ----------------------------------------------------------------------------------------------------
        return newString.ToString();
    }
}