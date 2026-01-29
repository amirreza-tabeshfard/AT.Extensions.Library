namespace AT.Extensions.Strings.Extraction;
public static partial class SlugExtensions
{
    #region Method(s): Private

    [System.Text.RegularExpressions.GeneratedRegex("[^a-z0-9\\s-]")]
    private static partial System.Text.RegularExpressions.Regex RemoveNonAlphanumericAndHyphens();

    [System.Text.RegularExpressions.GeneratedRegex("\\s+")]
    private static partial System.Text.RegularExpressions.Regex CondenseWhitespace();

    [System.Text.RegularExpressions.GeneratedRegex("\\s")]
    private static partial System.Text.RegularExpressions.Regex SubstituteSpaces();

    #endregion

    public static string Slug(this string value, int maxLength)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        if (maxLength > 0)
        {
            string slug = value.RemoveDiacritics().ToLower();
            // ----------------------------------------------------------------------------------------------------
            slug = RemoveNonAlphanumericAndHyphens().Replace(slug, "");
            slug = CondenseWhitespace().Replace(slug, " ").Trim();
            slug = slug.Substring(0, slug.Length <= maxLength ? slug.Length : maxLength).Trim();
            slug = SubstituteSpaces().Replace(slug, "-");
            // ----------------------------------------------------------------------------------------------------
            return slug;
        }
        // ----------------------------------------------------------------------------------------------------
        throw new ArgumentOutOfRangeException(nameof(maxLength));
    }
}