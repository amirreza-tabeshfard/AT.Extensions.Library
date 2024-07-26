using AT.Extensions.DateTimes.Georgian.Conversion;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class LocalExtensions : Object
{
    public static DateTime Local(this DateTime utcDate, String timeZoneName)
    {
        if (utcDate == default)
            throw new ArgumentNullException(nameof(utcDate));
        else if (timeZoneName.IsNullOrEmpty() || timeZoneName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(timeZoneName));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneConverter.TZConvert.GetTimeZoneInfo(timeZoneName));
    }

    public static DateTime Local(this DateTime utcDate, AT.Infrastructure.SystemTimeZone systemTimeZone)
    {
        if (utcDate == default)
            throw new ArgumentNullException(nameof(utcDate));
        // ----------------------------------------------------------------------------------------------------
        return Local(utcDate, systemTimeZone.ToString());
    }

    public static DateTime LocalTimeToServerTime(this DateTime localTime, String timeZoneName)
    {
        if (localTime == default)
            throw new ArgumentNullException(nameof(localTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZone.CurrentTimeZone.ToLocalTime(localTime.ToUniversalTime(timeZoneName));
    }

    public static DateTime LocalTimeToServerTime(this DateTime localTime, AT.Infrastructure.SystemTimeZone serverTimeZone)
    {
        if (localTime == default)
            throw new ArgumentNullException(nameof(localTime));
        // ----------------------------------------------------------------------------------------------------
        return localTime.LocalTimeToServerTime(serverTimeZone.ToString());
    }
}