namespace AT.Extensions.Strings.Collections;
public static class FromCommaSeparatedExtensions : Object
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