namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class WorkingDayExtensions : Object
{
    public static DateTime WorkingDayAfter(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.WorkingDayAfter(new[]
        {
            source.DayOfWeekOnOrAfter(publicHoliday1),
            source.DayOfWeekOnOrAfter(publicHoliday2)
        });
    }

    public static DateTime WorkingDayAfter(this DateTime source, IEnumerable<DateTime> publicHolidays)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.AddDays(1).WorkingDayOnOrAfter(publicHolidays);
    }

    public static DateTime WorkingDayBefore(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.WorkingDayBefore(new[]
        {
            source.DayOfWeekOnOrBefore(publicHoliday1),
            source.DayOfWeekOnOrBefore(publicHoliday2)
        });
    }

    public static DateTime WorkingDayBefore(this DateTime source, IEnumerable<DateTime> publicHolidays)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.AddDays(-1).WorkingDayOnOrBefore(publicHolidays);
    }

    public static DateTime WorkingDayOnOrAfter(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.WorkingDayOnOrAfter(new[]
        {
            source.DayOfWeekOnOrAfter(publicHoliday1),
            source.DayOfWeekOnOrAfter(publicHoliday2)
        });
    }

    public static DateTime WorkingDayOnOrAfter(this DateTime source, IEnumerable<DateTime> publicHolidays)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<DateTime> nonWorkingDays = publicHolidays.Union(new[]
        {
            source.DayOfWeekOnOrAfter(DayOfWeek.Saturday),
            source.DayOfWeekOnOrAfter(DayOfWeek.Sunday)
        });

        DateTime value = source.Date;

        while (nonWorkingDays.Any(x => x.Date == value))
            value = value.AddDays(1);
        // ----------------------------------------------------------------------------------------------------
        return value.Date;
    }

    public static DateTime WorkingDayOnOrBefore(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        return source.WorkingDayOnOrBefore(new[]
        {
            source.DayOfWeekOnOrBefore(publicHoliday1),
            source.DayOfWeekOnOrBefore(publicHoliday2)
        });
    }

    public static DateTime WorkingDayOnOrBefore(this DateTime source, IEnumerable<DateTime> publicHolidays)
    {
        if (source == default)
            throw new ArgumentNullException(nameof(source));
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<DateTime> nonWorkingDays = publicHolidays.Union(new[]
        {
            source.DayOfWeekOnOrBefore(DayOfWeek.Saturday),
            source.DayOfWeekOnOrBefore(DayOfWeek.Sunday)
        });
        DateTime value = source.Date;

        while (nonWorkingDays.Any(x => x.Date == value))
            value = value.AddDays(-1);
        // ----------------------------------------------------------------------------------------------------
        return value.Date;
    }
}