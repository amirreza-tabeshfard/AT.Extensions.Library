namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class FormatClientExtensions
{
    #region Private: Method(s)

    private static String ToClientFormat(DateTime dateTime, String format)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (dateTime == DateTime.MinValue)
            return String.Empty;
        // ----------------------------------------------------------------------------------------------------
        TimeSpan offsetSpan = Infrastructure.LocalTimeZoneConfig.TimeZone.GetUtcOffset(dateTime);
        DateTimeOffset offset = new DateTimeOffset(dateTime.Ticks, offsetSpan);
        // ----------------------------------------------------------------------------------------------------
        return offset.ToString(format);
    }

    #endregion

    public static String FormatClientDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd");
    }

    public static String FormatClientTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "HH:mm");
    }
}