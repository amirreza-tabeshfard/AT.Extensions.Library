using AT.Extensions.DateTimes.Georgian.Extraction;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class EndOfExtensions
{
    public static DateTime EndOfSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfSecond().AddSeconds(1).AddTicks(-1);
    }

    public static DateTime EndOfMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfMinute().AddMinutes(1).AddTicks(-1);
    }

    public static DateTime EndOfHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddHours(1).AddTicks(-1);
    }

    public static DateTime EndOfTheDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddDays(1).AddTicks(-1);
    }

    public static DateTime EndOfTheWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return EndOfTheDay(dateTime.AddDays(7 - (Int32)(dateTime.DayOfWeek)));
    }

    public static DateTime EndOfTheWeek(this DateTime dateTime, DayOfWeek firstWeekDay = DayOfWeek.Sunday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 dayDiff = (Int32)firstWeekDay - (Int32)dateTime.DayOfWeek;

        if (dayDiff <= 0)
            dayDiff += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(dayDiff);
    }

    public static DateTime EndOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1)
               .AddMonths(1)
               .AddDays(-1);
    }

    public static DateTime EndOfTheYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddYears(1).AddTicks(-1);
    }

    public static DateTime EndOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.QuarterOfYear() switch
        {
            1 => new DateTime(dateTime.Year, 3, 31, 23, 59, 49),
            2 => new DateTime(dateTime.Year, 6, 30, 23, 59, 49),
            3 => new DateTime(dateTime.Year, 9, 30, 23, 59, 49),
            _ => new DateTime(dateTime.Year, 12, 31, 23, 59, 49),
        };
    }
}