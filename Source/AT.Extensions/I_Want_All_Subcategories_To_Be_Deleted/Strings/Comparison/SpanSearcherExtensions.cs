using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
public static class SpanSearcherExtensions
{
    public static Boolean SpanSearcherContains(ReadOnlySpan<Char> stringToSearch, String searchFor, Int32 startAt = 0, Int32 endAt = -1)
    {
        ArgumentException.ThrowIfNullOrEmpty(searchFor);
        // ----------------------------------------------------------------------------------------------------
        if (startAt < 0)
            throw new ArgumentException("Starting Index must be positive number");

        if (endAt < -1)
            throw new ArgumentException("Ending Index must be positive number");

        if (endAt < startAt)
            throw new ArgumentException("Ending index cannot be less than the starting index");
        // ----------------------------------------------------------------------------------------------------
        if (stringToSearch == default)
            return false;
        
        if (stringToSearch.IsEmpty)
            return false;

        if (startAt > stringToSearch.Length)
            return false;
        // ----------------------------------------------------------------------------------------------------
        if (endAt == -1)
            endAt = stringToSearch.Length;

        if (endAt > stringToSearch.Length)
            endAt = stringToSearch.Length;
        // ----------------------------------------------------------------------------------------------------
        ReadOnlySpan<Char> lookingFor = searchFor;
        // ----------------------------------------------------------------------------------------------------
        for (Int32 i = startAt; i <= endAt - lookingFor.Length; i++)
            if (stringToSearch.Slice(i, lookingFor.Length).SequenceEqual(lookingFor))
                return true;
        // ----------------------------------------------------------------------------------------------------
        return false;
    }
}