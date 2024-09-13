namespace AT.Extensions.Strings.Extraction;
public static class MoveUpExtensions : Object
{
    public static String? MoveUp(this String relativeUrl)
    {
        ArgumentException.ThrowIfNullOrEmpty(relativeUrl);
        // ----------------------------------------------------------------------------------------------------
        string[] segments = relativeUrl.Split('/');
        // ----------------------------------------------------------------------------------------------------
        if (segments.Length == 1)
            return default;
        // ----------------------------------------------------------------------------------------------------
        return segments.Skip(1).Join("/");
    }
}