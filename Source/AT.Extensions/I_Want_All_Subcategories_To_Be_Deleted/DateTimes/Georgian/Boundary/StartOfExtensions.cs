namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Boundary;
public static class StartOfExtensions
{
    public static DateTime StartOfSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, millisecond: 0);
    }

    public static DateTime StartOfMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, millisecond: 0);
    }

    public static DateTime StartOfHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, millisecond: 0);
    }

    public static DateTime StartOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public static DateTime StartOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, 1, 1);
    }

    public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek firstWeekDay = DayOfWeek.Sunday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 dayDiff = (Int32)firstWeekDay - (Int32)dateTime.DayOfWeek;

        if (dayDiff > 0)
            dayDiff -= 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(dayDiff);
    }

    public static DateTime StartOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 quarterStartMonth = dateTime.Month - (dateTime.Month % 3) + 1;
        return new DateTime(dateTime.Year, quarterStartMonth, 1);
    }
}