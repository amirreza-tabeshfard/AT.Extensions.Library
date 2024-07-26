using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static  class LocalTimeToExtensions  : Object
{
    public static String LocalTimeToServerTime(this String localTime, String serverTimeZoneName, String formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (localTime.IsNullOrEmpty() || localTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(localTime));
        else if (serverTimeZoneName.IsNullOrEmpty() || serverTimeZoneName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(serverTimeZoneName));
        else if (formatToReturn.IsNullOrEmpty() || formatToReturn.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(formatToReturn));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(localTime)
               .LocalTimeToServerTime(serverTimeZoneName)
               .ToString(formatToReturn);
    }

    public static String LocalTimeToServerTime(this String localTime, Infrastructure.SystemTimeZone serverTimeZone, String formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (localTime.IsNullOrEmpty() || localTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(localTime));
        else if (serverTimeZone == default)
            throw new ArgumentNullException(nameof(serverTimeZone));
        else if (formatToReturn.IsNullOrEmpty() || formatToReturn.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(formatToReturn));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(localTime)
               .LocalTimeToServerTime(serverTimeZone)
               .ToString(formatToReturn);
    }
}