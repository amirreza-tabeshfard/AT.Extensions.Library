namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Boundary;
public static class NextExtensions
{
    public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
    {
        if (from == default)
            throw new ArgumentNullException(nameof(from));
        // ----------------------------------------------------------------------------------------------------
        Int32 start = (Int32)from.DayOfWeek;
        Int32 target = (Int32)dayOfWeek;

        if (target <= start)
            target += 7;
        // ----------------------------------------------------------------------------------------------------
        return from.AddDays(target - start);
    }

    public static DateTime NextAnniversary(this DateTime dateTime, Int32 eventMonth, Int32 eventDay, Boolean preserveMonth = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime calcDate;

        if (eventDay > 31 || eventDay < 1 || eventMonth > 12 || eventMonth < 1 ||
           ((eventMonth == 4 || eventMonth == 6 || eventMonth == 9 || eventMonth == 11) && eventDay > 30) ||
           (eventMonth == 2 && eventDay > 29))
            throw new Exception("Invalid combination of Event Year and Event Month.");

        calcDate = new DateTime(dateTime.Year + (dateTime.Month < eventMonth || dateTime.Month == eventMonth && dateTime.Day < eventDay ? 0 : 1), eventMonth, 1, 0, 0, 0, dateTime.Kind).AddDays(eventDay - 1);
        // ----------------------------------------------------------------------------------------------------
        if (eventMonth == calcDate.Month || !preserveMonth)
            return calcDate;
        // ----------------------------------------------------------------------------------------------------
        return calcDate.AddYears(dateTime.Month == 2 && dateTime.Day == 28 ? 1 : 0).AddDays(-1);
    }

    public static DateTime NextAnniversary(this DateTime dateTime, DateTime eventDate, Boolean preserveMonth = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime calcDate;

        if (dateTime.Date < eventDate.Date)
            return new DateTime(eventDate.Year, eventDate.Month, eventDate.Day, 0, 0, 0, dateTime.Kind);

        calcDate = new DateTime(dateTime.Year + (dateTime.Month < eventDate.Month || dateTime.Month == eventDate.Month && dateTime.Day < eventDate.Day ? 0 : 1), eventDate.Month, 1, 0, 0, 0, dateTime.Kind).AddDays(eventDate.Day - 1);
        // ----------------------------------------------------------------------------------------------------
        if (eventDate.Month == calcDate.Month || !preserveMonth)
            return calcDate;
        // ----------------------------------------------------------------------------------------------------
        return calcDate.AddYears(dateTime.Month == 2 && dateTime.Day == 28 ? 1 : 0).AddDays(-1);
    }

    public static DateTime NextSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddSeconds(1);
    }

    public static DateTime NextMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMinutes(1);
    }

    public static DateTime NextHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddHours(1);
    }

    public static DateTime NextDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(1);
    }

    public static DateTime NextDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeTemp = new System.Globalization.GregorianCalendar().AddDays(dateTime, -((Int32)dateTime.DayOfWeek) + (Int32)dayOfWeek);
        return (dateTimeTemp.Day < dateTime.Day)
               ? dateTimeTemp.AddDays(7)
               : dateTimeTemp;
    }

    public static DateTime NextMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMonths(1);
    }

    public static DateTime NextSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().AddDays(dateTime, -((Int32)dateTime.DayOfWeek) + 7);
    }

    public static DateTime NextWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(7);
    }

    public static DateTime NextYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddYears(1);
    }
}