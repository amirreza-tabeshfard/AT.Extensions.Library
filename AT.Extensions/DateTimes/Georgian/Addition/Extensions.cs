using AT.Extensions.DateTimes.Georgian.Comparison;
using System;

namespace AT.Extensions.DateTimes.Georgian.Addition;
public static class Extensions : Object
{
    public static DateTime Add(this DateTime dateTime, Double value, Enums.DateTimeDifferenceFormat differenceFormat)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return differenceFormat switch
        {
            Enums.DateTimeDifferenceFormat.Milliseconds => dateTime.AddMilliseconds(value),
            Enums.DateTimeDifferenceFormat.Seconds => dateTime.AddSeconds(value),
            Enums.DateTimeDifferenceFormat.Minutes => dateTime.AddMinutes(value),
            Enums.DateTimeDifferenceFormat.Hours => dateTime.AddHours(value),
            Enums.DateTimeDifferenceFormat.Days => dateTime.AddDays(value),
            Enums.DateTimeDifferenceFormat.Weeks => dateTime.AddDays(value * 7),
            Enums.DateTimeDifferenceFormat.Months => dateTime.AddMonths(Convert.ToInt32(value)),
            Enums.DateTimeDifferenceFormat.Years => dateTime.AddYears(Convert.ToInt32(value)),
            _ => default,
        };
    }

    public static TimeSpan AddDays(this TimeSpan time, Int32 days)
    {
        if (time == default)
            throw new ArgumentNullException(nameof(time));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan additionalDays = new TimeSpan(days: days,
                                               hours: 0,
                                               minutes: 0,
                                               seconds: 0,
                                               milliseconds: 0);
        // ----------------------------------------------------------------------------------------------------
        return time.Add(additionalDays);
    }

    public static DateTime AddWorkDays(this DateTime dateTime, Int32 days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        while (dateTime.DayOfWeek.IsWeekday())
            dateTime = dateTime.AddDays(value: 1.0);

        for (Int32 i = 0; i < days; ++i)
        {
            dateTime = dateTime.AddDays(value: 1.0);

            while (dateTime.DayOfWeek.IsWeekday())
                dateTime = dateTime.AddDays(value: 1.0);
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime;
    }

    public static DateTime AddWeeks(this DateTime dateTime, Double value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(value * 7);
    }

    public static DateTime AddBusinessDays(this DateTime dateTime, UInt32 daysToAdd, IEnumerable<DateTime>? holidays)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else ArgumentNullException.ThrowIfNull(holidays);
        // ----------------------------------------------------------------------------------------------------
        List<DateTime>? holidaysList = holidays.ToList();
        for (Int32 i = 0; i < daysToAdd; i++)
            switch (dateTime.DayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        dateTime = dateTime.AddDays(3);

                        if (holidaysList.Contains(dateTime))
                            dateTime = dateTime.AddDays(1);
                    }
                    break;

                default:
                    {
                        dateTime = dateTime.AddDays(1);

                        if (holidaysList.Contains(dateTime))
                            dateTime = dateTime.AddDays(dateTime.DayOfWeek == DayOfWeek.Friday ? 3 : 1);
                    }
                    break;
            }
        // ----------------------------------------------------------------------------------------------------
        return dateTime;
    }

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