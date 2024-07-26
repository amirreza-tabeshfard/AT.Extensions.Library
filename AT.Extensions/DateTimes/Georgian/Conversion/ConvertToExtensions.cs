namespace AT.Extensions.DateTimes.Georgian.Conversion;
public static class ConvertToExtensions : Object
{
    public static String ConvertTo24HourFormatWithSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static String ConvertToFormatDateOnly(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("yyy-MM-dd");
    }
}