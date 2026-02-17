namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class FromCommaSeparatedExtensions
{
    public static List<String> FromCommaSeparatedToList(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        List<String> values = new();
        foreach (String item in value.Split(','))
            values.Add(item);
        // ----------------------------------------------------------------------------------------------------
        return values;
    }
}