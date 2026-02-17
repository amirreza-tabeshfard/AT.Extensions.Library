namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Addition;
public static class InsertExtensions
{
    public static String InsertCamelCaseSpaces(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value.Length * 2);
        Boolean lastIsUpper = false;
        Boolean lastIsWhitespace = false;
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = 0; i < value.Length; i++)
        {
            Char c = value[i];
            Boolean isUpper = Char.IsUpper(c);
            Boolean nextIsLower = i + 1 < value.Length && Char.IsLower(value[i + 1]);

            if (isUpper && builder.Length > 0 && (!lastIsUpper || nextIsLower) && !lastIsWhitespace)
                builder.Append(' ');

            builder.Append(c);
            lastIsUpper = isUpper;
            lastIsWhitespace = Char.IsWhiteSpace(c);
        }
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    public static String InsertIntoEachLine(this String input, Int32 startIndex, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        String[] delims = { Environment.NewLine };
        String[] parts = input.Split(delims, StringSplitOptions.None);
        List<String> results = new();
        // ----------------------------------------------------------------------------------------------------
        foreach (String i in parts)
            results.Add(i.PadRight(startIndex)
                         .Insert(startIndex, value));
        // ----------------------------------------------------------------------------------------------------
        return String.Join(Environment.NewLine, results);
    }
}