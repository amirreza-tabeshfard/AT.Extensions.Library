namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
public static class TimeOfExtensions
{
    public static TimeSpan? TimeOfDay(this TimeSpan? value)
    {
        TimeSpan? result = default;
        // ----------------------------------------------------------------------------------------------------
        if (value.HasValue)
            result = new TimeSpan(hours: value.Value.Hours,
                                  minutes: value.Value.Minutes,
                                  seconds: value.Value.Seconds);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}