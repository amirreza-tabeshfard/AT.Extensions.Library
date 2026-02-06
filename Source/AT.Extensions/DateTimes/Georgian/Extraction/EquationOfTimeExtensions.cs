namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class EquationOfTimeExtensions
{
    #region Field(s)

    private const Double AverageAngle = 0.985653;
    private const Double E360OverPi = 1.915169;
    private const Double MinutesPerDegree = 3.98892;
    private const Double MaxEarthTilt = 23.45;

    #endregion

    public static TimeSpan EquationOfTimeEccentricEffect(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Double h = AverageAngle * (dateTime.DayOfYear - 2);
        Double v = h + (E360OverPi * Math.Sin(h.AsDegreesToRadians()));
        Double m = ((h - v) * MinutesPerDegree);
        Double s = (m - (Int32)m) * 60;
        Double l = (s - (Int32)s) * 1000;
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(0, 0, (Int32)m, (Int32)s, (Int32)l);
    }

    public static TimeSpan EquationOfTimeTiltEffect(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Double e = AverageAngle * (dateTime.DayOfYear - 80);
        e = (e >= 270 ? e - 360 : (e >= 90 ? e - 180 : e));
        Double b = Math.Atan(Math.Cos(MaxEarthTilt.AsDegreesToRadians()) * Math.Tan(e.AsDegreesToRadians())).AsRadiansToDegrees();
        Double m = ((e - b) * MinutesPerDegree);
        Double s = (m - (Int32)m) * 60;
        Double l = (s - (Int32)s) * 1000;
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(0, 0, (Int32)m, (Int32)s, (Int32)l);
    }

    public static TimeSpan EquationOfTimeTotal(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.EquationOfTimeEccentricEffect() + dateTime.EquationOfTimeTiltEffect();
    }
}