using AT.Extensions.DateTimes.Georgian.Addition;
using AT.Extensions.DateTimes.Georgian.Extraction;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class DaySpecificNamingExtensions : Object
{
    public static DateTime Dawn(this DateTime dateTime, Double Latitude, Double Longitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        Double m = 720 + (4 * (-Longitude - dateTime.HourAngleDawn(Latitude, Kind))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Dusk(this DateTime dateTime, Double Latitude, Double Longitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        Double m = 720 + (4 * (-Longitude - dateTime.HourAngleDusk(Latitude, Kind))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Midnight(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static DateTime Noon(this DateTime dateTime)
    {
        return dateTime.SetTime(12, 0, 0);
    }

    public static DateTime SolarNoon(this DateTime dateTime, Double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan _eqTime = dateTime.EquationOfTimeTotal();
        Double m = 720 + (-Longitude * 4) - _eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Sunrise(this DateTime dateTime, Double Latitude, Double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        Double m = 720 + (4 * (-Longitude - dateTime.HourAngleSunrise(Latitude))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Sunset(this DateTime dateTime, Double Latitude, Double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        Double m = 720 + (4 * (-Longitude - dateTime.HourAngleSunset(Latitude))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Tomorrow(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(1);
    }

    public static DateTime Yesterday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-1);
    }
}