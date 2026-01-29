using AT.Extensions.DateTimes.Georgian.Addition;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class TimeZoneExtensions
{
    public static DateTime CurrentDateTimeIn(this DateTime dateTime, String timeZoneById)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timeZoneById));
    }

    public static DateTime FromUnixTime(this Double unixTimestamp)
    {
        return new DateTime(1970, 1, 1).Add(unixTimestamp, AT.Enums.DateTimeDifferenceFormat.Seconds);
    }

    public static DateTime In(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
    }
}