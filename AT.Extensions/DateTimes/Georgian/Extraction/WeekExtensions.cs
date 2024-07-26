namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static  class WeekExtensions  : Object
{
    public static Int32 MaxWeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return MaxWeekNumber(dateTime.Year, weekRule, weekStart);
    }

    public static Int32 MaxWeekNumber(Int32 year, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (year == default)
            throw new ArgumentNullException(nameof(year));
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().GetWeekOfYear(new DateTime(year, 12, 31), weekRule, weekStart);
    }

    public static Int32 WeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().GetWeekOfYear(dateTime, weekRule, weekStart);
    }
}