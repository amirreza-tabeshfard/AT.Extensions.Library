namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class TimeStampExtensions : Object
{
    #region Field(s)

    private static readonly Int64 _epochTicks;

    #endregion

    #region Constructor

    static TimeStampExtensions()
    {
        _epochTicks = 621355968000000000L;
    }

    #endregion

    public static TimeSpan AsMinutesToTimeSpan(this Double value)
    {
        Double h = Math.Floor(value / 60);
        value -= h * 60;
        Double s = (value - (Int32)value) * 60;
        Double l = (s - (Int32)s) * 1000;

        return new TimeSpan(0, (Int32)h, (Int32)value, (Int32)s, (Int32)l);
    }

    public static Double DateTimeToUnixTimeStamp(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime.ToUniversalTime().Ticks - _epochTicks) / TimeSpan.TicksPerSecond;
    }
}