using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class SentencesExtensions
{
    public static IEnumerable<String> Sentences(this String value, Boolean cleanNewLine = true, Boolean cleanWhitepace = true)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();
        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(value, @"((\s[^\.\!\?]\.)+|([^\.\!\?]\.)+|[^\.\!\?]+)+[\.\!\?]+(\s|$)") as IEnumerable<System.Text.RegularExpressions.Match>;
        IEnumerable<String> result = matches.Where(match => match.Success && !String.IsNullOrWhiteSpace(match.Value)).Select(match => match.Value);
        // ----------------------------------------------------------------------------------------------------
        if (cleanNewLine)
            result = result.Select(sentence => sentence.Replace(Environment.NewLine, String.Empty));
        // ----------------------------------------------------------------------------------------------------
        if (cleanWhitepace)
            result = result.Select(sentence => sentence.CleanWhiteSpace());
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}