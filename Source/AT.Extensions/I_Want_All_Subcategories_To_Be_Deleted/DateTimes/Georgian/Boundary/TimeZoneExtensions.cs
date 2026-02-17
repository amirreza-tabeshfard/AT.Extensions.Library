namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Boundary;
public static class TimeZoneExtensions
{
    public static DateTime CurrentDateTimeIn(this DateTime dateTime, String timeZoneById)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timeZoneById));
    }

    public static DateTime In(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
    }
}