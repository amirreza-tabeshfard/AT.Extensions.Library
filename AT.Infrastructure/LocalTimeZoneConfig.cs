namespace AT.Infrastructure;
public static class LocalTimeZoneConfig : Object
{
    #region Field(s)

    private static TimeZoneInfo? _tz;

    #endregion

    public static TimeZoneInfo TimeZone
    {
        get
        {
            if (_tz == default)
                throw new InvalidTimeZoneException("TimeZoneConfig has not been initialized. Call Init.");

            return _tz;
        }
    }

    public static TimeZoneInfo Init(String timeZoneId)
    {
        _tz = TimeZoneConverter.TZConvert.GetTimeZoneInfo(timeZoneId);
        return _tz;
    }

    public static TimeZoneInfo Create(String timeZoneId)
    {
        _tz = TimeZoneConverter.TZConvert.GetTimeZoneInfo(timeZoneId);
        return _tz;
    }

    public static void Reset()
    {
        _tz = default;
    }
}