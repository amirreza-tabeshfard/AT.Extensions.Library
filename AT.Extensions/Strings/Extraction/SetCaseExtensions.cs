namespace AT.Extensions.Strings.Extraction;
public static class SetCaseExtensions
{
    #region Method(s): Private

    private static String SetCapitalizeFirstCharacter(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value);
        // ----------------------------------------------------------------------------------------------------
        if (builder.Length > 0)
            builder[0] = Char.ToUpper(builder[0]);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    private static void EachChar(System.Text.StringBuilder builder, Int32 start, Int32 end, Func<Char, Char> action)
    {
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= builder.Length);

        for (Int32 i = start; i < end; i++)
            builder[i] = action(builder[i]);
    }

    private static Boolean IncludesLowerCase(String value, Int32 start, Int32 end)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= value.Length);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = start; i < end; i++)
            if (Char.IsLower(value[i]))
                return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    private static Boolean IncludesLowerCase(System.Text.StringBuilder builder, Int32 start, Int32 end)
    {
        System.Diagnostics.Debug.Assert(start <= end);
        System.Diagnostics.Debug.Assert(end <= builder.Length);
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = start; i < end; i++)
            if (Char.IsLower(builder[i]))
                return true;
        // ----------------------------------------------------------------------------------------------------
        return default;
    }

    private static void SetWordSentenceCase(System.Text.StringBuilder builder, Int32 wordStart, Int32 wordEnd, ref Boolean inSentence)
    {
        System.Diagnostics.Debug.Assert(wordStart != -1);
        System.Diagnostics.Debug.Assert(wordStart <= wordEnd);
        System.Diagnostics.Debug.Assert(wordEnd <= builder.Length);
        // ----------------------------------------------------------------------------------------------------
        if (IncludesLowerCase(builder, wordStart, wordEnd))
            EachChar(builder, wordStart, wordEnd, c => Char.ToLower(c));
        // ----------------------------------------------------------------------------------------------------
        if (!inSentence)
        {
            builder[wordStart] = Char.ToUpper(builder[wordStart]);
            inSentence = true;
        }
        // ----------------------------------------------------------------------------------------------------
        if (inSentence && wordEnd < builder.Length)
            inSentence = default;
    }

    private static String SetSentenceCase(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value);
        // ----------------------------------------------------------------------------------------------------
        Boolean inSentence = false;
        Int32 wordStart = -1;
        Int32 i = default;
        // ----------------------------------------------------------------------------------------------------
        if (wordStart != -1)
            SetWordSentenceCase(builder, wordStart, i, ref inSentence);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    private static void SetWordTitleCase(System.Text.StringBuilder builder, Int32 wordStart, Int32 wordEnd, ref Boolean inSentence)
    {
        System.Diagnostics.Debug.Assert(wordStart != -1);
        System.Diagnostics.Debug.Assert(wordStart <= wordEnd);
        System.Diagnostics.Debug.Assert(wordEnd <= builder.Length);
        // ----------------------------------------------------------------------------------------------------
        if (IncludesLowerCase(builder, wordStart, wordEnd))
            EachChar(builder, wordStart, wordEnd, c => Char.ToLower(c));
        // ----------------------------------------------------------------------------------------------------
        if (!inSentence || !UncapitalizedTitleWords.Contains(builder.ToString(wordStart, wordEnd - wordStart)))
        {
            builder[wordStart] = Char.ToUpper(builder[wordStart]);
            inSentence = true;
        }
        // ----------------------------------------------------------------------------------------------------
        if (inSentence && wordEnd < builder.Length)
            inSentence = false;
    }

    private static String SetTitleCase(String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder builder = new(value);
        // ----------------------------------------------------------------------------------------------------
        Boolean inSentence = false;
        Int32 wordStart = -1;
        Int32 i = default;
        // ----------------------------------------------------------------------------------------------------
        if (wordStart != -1)
            SetWordTitleCase(builder, wordStart, i, ref inSentence);
        // ----------------------------------------------------------------------------------------------------
        return builder.ToString();
    }

    #endregion

    #region Peoperties

    private static HashSet<String> UncapitalizedTitleWords { get; set; } = 
        new(StringComparer.OrdinalIgnoreCase)
        {
            "a",
            "about",
            "after",
            "an",
            "and",
            "are",
            "around",
            "as",
            "at",
            "be",
            "before",
            "but",
            "by",
            "else",
            "for",
            "from",
            "how",
            "if",
            "in",
            "is",
            "into",
            "nor",
            "of",
            "on",
            "or",
            "over",
            "than",
            "that",
            "the",
            "then",
            "this",
            "through",
            "to",
            "under",
            "when",
            "where",
            "why",
            "with"
        };

    #endregion

    public static String SetCase(this String value, Enums.CaseType caseType)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return caseType switch
        {
            Enums.CaseType.Lower => value.ToLower(),
            Enums.CaseType.Upper => value.ToUpper(),
            Enums.CaseType.CapitalizeFirstCharacter => SetCapitalizeFirstCharacter(value),
            Enums.CaseType.Sentence => SetSentenceCase(value),
            Enums.CaseType.Title => SetTitleCase(value),
            _ => value,
        };
    }
}