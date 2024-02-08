using AT.Extensions.DateTimes.Georgian.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Addition;
public static class Extensions : Object
{
    public static DateTime Add(this DateTime dateTime, double value, AT.Enums.DateTimeDifferenceFormat differenceFormat)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return differenceFormat switch
        {
            AT.Enums.DateTimeDifferenceFormat.Milliseconds => dateTime.AddMilliseconds(value),
            AT.Enums.DateTimeDifferenceFormat.Seconds => dateTime.AddSeconds(value),
            AT.Enums.DateTimeDifferenceFormat.Minutes => dateTime.AddMinutes(value),
            AT.Enums.DateTimeDifferenceFormat.Hours => dateTime.AddHours(value),
            AT.Enums.DateTimeDifferenceFormat.Days => dateTime.AddDays(value),
            AT.Enums.DateTimeDifferenceFormat.Weeks => dateTime.AddDays(value * 7),
            AT.Enums.DateTimeDifferenceFormat.Months => dateTime.AddMonths(Convert.ToInt32(value)),
            AT.Enums.DateTimeDifferenceFormat.Years => dateTime.AddYears(Convert.ToInt32(value)),
            _ => default,
        };
    }

    public static TimeSpan AddDays(this TimeSpan time, int days)
    {
        if (time == default)
            throw new ArgumentNullException($"time is '{default(TimeSpan)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan additionalDays = new TimeSpan(days: days,
                                               hours: 0,
                                               minutes: 0,
                                               seconds: 0,
                                               milliseconds: 0);
        // ----------------------------------------------------------------------------------------------------
        return time.Add(additionalDays);
    }

    public static DateTime AddWorkDays(this DateTime dateTime, int days)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        while (dateTime.DayOfWeek.IsWeekday())
            dateTime = dateTime.AddDays(value: 1.0);

        for (int i = 0; i < days; ++i)
        {
            dateTime = dateTime.AddDays(value: 1.0);

            while (dateTime.DayOfWeek.IsWeekday())
                dateTime = dateTime.AddDays(value: 1.0);
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime;
    }

    public static DateTime AddWeeks(this DateTime dateTime, double value)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(value * 7);
    }

    public static DateTime AddBusinessDays(this DateTime dateTime, uint daysToAdd, IEnumerable<DateTime>? holidays)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");

        if (holidays == default)
            throw new ArgumentNullException($"holidays is {default(DateTime)}");
        // ----------------------------------------------------------------------------------------------------
        List<DateTime>? holidaysList = holidays.ToList();
        for (int i = 0; i < daysToAdd; i++)
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

    public static DateTime SetTime(this DateTime dateTime, int hour, int minute, int second, int millisecond = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (!hour.IsValidTime(minute, second))
            throw new ArgumentOutOfRangeException("Time values are not within ccepted range");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, hour, minute, second, millisecond);
    }

    public static DateTime SetDay(this DateTime dateTime, int day)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (!dateTime.Year.IsValidDate(dateTime.Month, day))
            throw new ArgumentOutOfRangeException(nameof(day));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-1 * dateTime.Day).AddDays(day);
    }

    public static DateTime SetYear(this DateTime dateTime, int year)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (!year.IsValidDate(dateTime.Month, dateTime.Day))
            throw new ArgumentOutOfRangeException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, dateTime.Millisecond);
    }
}