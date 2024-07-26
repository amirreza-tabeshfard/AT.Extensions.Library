namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class Extensions : Object
{
    #region Field(s)

    private const Double AverageAngle = 0.985653;
    private const Double E360OverPi = 1.915169;
    private const Double MinutesPerDegree = 3.98892;
    private const Double MaxEarthTilt = 23.45;
    private const Double VAtVernalEquinox = 78.74611803;

    #endregion

    public static String? NullDateToString(this DateTime? dateTime, String format = "M/d/yyyy", String? nullResult = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        if (dateTime.HasValue)
            return dateTime.Value.ToString(format);
        // ----------------------------------------------------------------------------------------------------
        return nullResult;
    }

    public static Infrastructure.DateTimeRange Since(this DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime == default)
            throw new ArgumentNullException(nameof(startDateTime));
        else if (endDateTime == default)
            throw new ArgumentNullException(nameof(endDateTime));
        // ----------------------------------------------------------------------------------------------------
        return new Infrastructure.DateTimeRange(startDateTime, endDateTime);
    }

    public static Double SolarDeclination(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Double h = AverageAngle * (dateTime.DayOfYear - 2);
        Double v = h + (E360OverPi * Math.Sin(h.AsDegreesToRadians()));
        // ----------------------------------------------------------------------------------------------------
        return Math.Asin((MinutesPerDegree / 10) * Math.Sin((v - VAtVernalEquinox).AsDegreesToRadians()))
               .AsRadiansToDegrees();
    }

    public static TimeSpan TimeElapsed(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now - dateTime;
    }

    public static Infrastructure.DateTimeRange Until(this DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime == default)
            throw new ArgumentNullException(nameof(startDateTime));
        else if (endDateTime == default)
            throw new ArgumentNullException(nameof(endDateTime));
        // ----------------------------------------------------------------------------------------------------
        return new Infrastructure.DateTimeRange(startDateTime, endDateTime);
    }

    public static Double DaysLeft(this DateTime source, DateTime target)
    {
        return (target.Date - source.Date).TotalDays;
    }

    public static String DayName(this DateTime current)
    {
        return current.DayOfWeek.ToString();
    }
}