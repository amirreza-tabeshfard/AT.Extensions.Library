namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class ThisWeekExtensions : Object
{
    public static DateTime ThisWeekSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Sunday);
    }

    public static DateTime ThisWeekMonday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Monday);
    }

    public static DateTime ThisWeekTuesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Tuesday);
    }

    public static DateTime ThisWeekWednesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Wednesday);
    }

    public static DateTime ThisWeekThursday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Thursday);
    }

    public static DateTime ThisWeekFriday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Friday);
    }

    public static DateTime ThisWeekSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Saturday);
    }
}