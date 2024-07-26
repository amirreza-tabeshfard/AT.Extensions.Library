using AT.Extensions.DateTimes.Georgian.Extraction;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class BeginningOfExtensions : Object
{
    public static DateTime BeginningOfTheDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
    }

    public static DateTime BeginningOfTheWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return BeginningOfTheDay(dateTime.AddDays(1 - (Int32)(dateTime.DayOfWeek)));
    }

    public static DateTime BeginningOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
    }

    public static DateTime BeginningOfTheYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, 1, 1, 0, 0, 0);
    }

    public static DateTime BeginningOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.QuarterOfYear() switch
        {
            1 => new DateTime(dateTime.Year, 1, 1, 0, 0, 0, millisecond: 0),
            2 => new DateTime(dateTime.Year, 4, 1, 0, 0, 0, millisecond: 0),
            3 => new DateTime(dateTime.Year, 7, 1, 0, 0, 0, millisecond: 0),
            _ => new DateTime(dateTime.Year, 10, 1, 0, 0, 0, millisecond: 0),
        };
    }
}