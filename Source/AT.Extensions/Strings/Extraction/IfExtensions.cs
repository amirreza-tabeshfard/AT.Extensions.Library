namespace AT.Extensions.Strings.Extraction;
public static class IfExtensions
{
    public static void IfNotNull(this String? target, Action<String> continuation)
    {
        ArgumentException.ThrowIfNullOrEmpty(target);
        // ----------------------------------------------------------------------------------------------------
        continuation(target);
    }
}