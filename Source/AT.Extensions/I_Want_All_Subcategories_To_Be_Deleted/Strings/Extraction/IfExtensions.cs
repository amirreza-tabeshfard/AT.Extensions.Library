namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Extraction;
public static class IfExtensions
{
    public static void IfNotNull(this String? target, Action<String> continuation)
    {
        ArgumentException.ThrowIfNullOrEmpty(target);
        // ----------------------------------------------------------------------------------------------------
        continuation(target);
    }
}