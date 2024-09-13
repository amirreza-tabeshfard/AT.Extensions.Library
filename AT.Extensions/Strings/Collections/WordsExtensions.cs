﻿namespace AT.Extensions.Strings.Collections;
public static class WordsExtensions : Object
{
    public static IEnumerable<String> Words(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<String> defaultValue = Enumerable.Empty<String>();
        IEnumerable<System.Text.RegularExpressions.Match> matches = System.Text.RegularExpressions.Regex.Matches(value, @"\w+") as IEnumerable<System.Text.RegularExpressions.Match>;
        // ----------------------------------------------------------------------------------------------------
        return matches
               .Where(match => match.Success && !String.IsNullOrWhiteSpace(match.Value))
               .Select(match => match.Value);
    }
}