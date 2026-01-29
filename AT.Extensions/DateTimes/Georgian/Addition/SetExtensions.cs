using AT.Extensions.DateTimes.Georgian.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Addition;
public static class SetExtensions
{
    public static DateTime SetTime(this DateTime dateTime, Int32 hour, Int32 minute, Int32 second, Int32 millisecond = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (!hour.IsValidTime(minute, second))
            throw new ArgumentOutOfRangeException("Time values are not within ccepted range");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second, millisecond);
    }

    public static DateTime SetDay(this DateTime dateTime, Int32 day)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (!dateTime.Year.IsValidDate(dateTime.Month, day))
            throw new ArgumentOutOfRangeException(nameof(day));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-1 * dateTime.Day).AddDays(day);
    }

    public static DateTime SetYear(this DateTime dateTime, Int32 year)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (!year.IsValidDate(dateTime.Month, dateTime.Day))
            throw new ArgumentOutOfRangeException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
    }
}