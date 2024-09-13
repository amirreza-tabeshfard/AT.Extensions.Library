namespace AT.Extensions.Strings.Comparison;
public static class SequenceExtensions : Object
{
    public static Boolean SequenceEqual(this String first, String second)
    {
        ArgumentException.ThrowIfNullOrEmpty(first);
        ArgumentException.ThrowIfNullOrEmpty(second);
        // ----------------------------------------------------------------------------------------------------
        return first == second;
    }

    public static Boolean SequenceEqual(this String first, String second, StringComparison comparison)
    {
        ArgumentException.ThrowIfNullOrEmpty(first);
        ArgumentException.ThrowIfNullOrEmpty(second);
        // ----------------------------------------------------------------------------------------------------
        return String.Equals(first, second, comparison);
    }
}