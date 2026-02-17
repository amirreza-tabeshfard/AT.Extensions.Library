using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Addition;
public static class AppendExtensions
{
    public static String Append(this String self, IEnumerable<String> lines, Boolean asAppendLine = false, Boolean appendWithWhiteSpace = false)
    {
        System.Text.StringBuilder builder = new();

        if (asAppendLine)
            builder.AppendLine(self);
        else
            builder.Append(self);

        String leadWith = String.Empty;
        if (appendWithWhiteSpace)
            leadWith = " ";

        String[] theLines = (String[])lines;
        String lastLine = theLines[theLines.Length - 1];

        foreach (String line in theLines)
        {
            if (asAppendLine && line == lastLine)
            {
                builder.Append(line);
                continue;
            }

            if (asAppendLine)
            {
                builder.AppendLine(line);
                continue;
            }

            builder.Append($"{leadWith}{line}");
        }
        return builder.ToString();
    }

    public static String AppendIf(this String str, Boolean condition, String appendMe)
    {
        if (condition)
            return str + appendMe;

        return str;
    }

    public static String AppendPath(this String path, params String[] parts)
    {
        return Path.Combine(parts);
    }

    public static String AppendPrefixIfMissing(this String value, String prefix, Boolean ignoreCase = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.StartsWithIgnoreCase(prefix) : value.StartsWith(prefix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return prefix + value;
    }

    public static String AppendSuffixIfMissing(this String value, String suffix, Boolean ignoreCase = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (ignoreCase ? value.EndsWithIgnoreCase(suffix) : value.EndsWith(suffix))
            return value;
        // ----------------------------------------------------------------------------------------------------
        return value + suffix;
    }

    public static String AppendSymbolIfMissing(this String? input, String symbol)
    {
        ArgumentException.ThrowIfNullOrEmpty(input);
        ArgumentException.ThrowIfNullOrEmpty(symbol);
        // ----------------------------------------------------------------------------------------------------
        input ??= String.Empty;
        return input.EndsWith(symbol)
               ? input
               : input + symbol;
    }

    public static String AppendUrl(this String url, String part)
    {
        String composite = (url ?? String.Empty).TrimEnd('/') + "/" + part.TrimStart('/');

        return composite.TrimEnd('/');
    }
}