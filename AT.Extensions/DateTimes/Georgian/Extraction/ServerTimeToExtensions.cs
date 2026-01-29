using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static  class ServerTimeToExtensions
{
    public static DateTime ServerTimeToLocalTime(this DateTime dateTime, String timeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeZone serverTimeZone = TimeZone.CurrentTimeZone;
        DateTime dateTimeInUtc = serverTimeZone.ToUniversalTime(dateTime);
        // ----------------------------------------------------------------------------------------------------
        return dateTimeInUtc.Local(timeZoneName);
    }

    public static DateTime ServerTimeToLocalTime(this DateTime dateTime, Infrastructure.SystemTimeZone localTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return ServerTimeToLocalTime(dateTime, localTimeZone.ToString());
    }

    public static String ServerTimeToLocalTime(this String serverTime, String timeZoneName, String formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (serverTime.IsNullOrEmpty() || serverTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(serverTime));
        else if (timeZoneName.IsNullOrEmpty() || timeZoneName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(timeZoneName));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(serverTime)
               .ServerTimeToLocalTime(timeZoneName)
               .ToString(formatToReturn);
    }

    public static String ServerTimeToLocalTime(this String serverTime, Infrastructure.SystemTimeZone localTimeZone, String formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (serverTime.IsNullOrEmpty() || serverTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(serverTime));
        else if (localTimeZone == default)
            throw new ArgumentNullException(nameof(localTimeZone));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(serverTime)
               .ServerTimeToLocalTime(localTimeZone)
               .ToString(formatToReturn);
    }
}