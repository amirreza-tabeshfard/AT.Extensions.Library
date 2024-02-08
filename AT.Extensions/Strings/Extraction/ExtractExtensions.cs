using AT.Extensions.Strings.Conversion;

namespace AT.Extensions.Strings.Extraction;
public static class ExtractExtensions : Object
{
    public static int? ExtractFirstInt(this string? value)
    {
        if (value == null)
            return null;
        var firstInt = System.Text.RegularExpressions.Regex.Match(value, @"-?( )?\d+").Value.Replace(" ", "");
        return firstInt.ToInt();
    }

    public static decimal? ExtractDecimal(this string? value)
    {
        var valueCleaned = System.Text.RegularExpressions.Regex.Replace(value, "[^0-9.,]", "").Replace(".", ",");
        decimal result;
        if (decimal.TryParse(valueCleaned, out result))
            return result;
        else
            return null;
    }
}