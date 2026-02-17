using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Boundary;
public static class GetExtensions
{
    public static DateTime GetCyclic(this DateTime dateTime, IEnumerable<DateTime> cycle)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime result = dateTime;

        if (cycle?.Any() ?? false)
        {
            DateTime from = cycle.Min();
            DateTime to = cycle.Max();

            Int32 duration = to.GetAbsDuration(from).Days + 1;

            if (duration < 2)
                result = from;
            else if (dateTime < from)
            {
                Int32 distance = from.GetAbsDuration(dateTime).Days - 1;
                result = to.AddDays((distance % duration) * -1);
            }
            else if (dateTime > to)
            {
                Int32 distance = dateTime.GetAbsDuration(to).Days - 1;
                result = from.AddDays(distance % duration);
            }
            else
                result = dateTime;
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static DateTime GetDateOfTarget(this DateTime dateTime, DayOfWeek targetDayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 iCurr = (Int32)dateTime.DayOfWeek;
        Int32 iTarg = (Int32)targetDayOfWeek;
        Int32 nTarg;

        if (iCurr < iTarg)
            nTarg = iTarg - iCurr;
        else
            nTarg = 7 - (iCurr - iTarg);
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(nTarg);
    }

    public static DateTime GetFirstDayOfNextWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(7);
    }

    public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        Int32 diff = dateTime.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        // ----------------------------------------------------------------------------------------------------
        if (diff < 0)
            diff += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-diff).Date;
    }

    public static DateTime GetLastDayOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
    }

    public static DateTime GetLastDayOfPreviousWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(-1);
    }

    public static DateTime GetLastDayOfWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(6);
    }

    public static DateTime GetLastWeekdayOfMonth(this DayOfWeek day, Int32 year, Int32 month)
    {
        DateTime lastDayOfTheMonth = new DateTime(year, month, 1)
                                            .AddMonths(1)
                                            .AddDays(-1);

        Int32 searchDay = (Int32)day;
        Int32 lastDay = (Int32)lastDayOfTheMonth.DayOfWeek;

        return lastDayOfTheMonth.AddDays(lastDay >= searchDay
                                         ? searchDay - lastDay
                                         : searchDay - lastDay - 7);
    }

    public static DateTime GetMaxDate()
    {
        return DateTime.MaxValue;
    }

    public static DateTime GetMinDate()
    {
        return DateTime.MinValue;
    }

    public static DateTime GetNext(this DateTime dateTime, DayOfWeek day)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 daysToAdd = ((Int32)day - (Int32)dateTime.DayOfWeek + 7) % 7;
        return dateTime.AddDays(daysToAdd);
    }

    public static DateTime GetPrevious(this DateTime dateTime, DayOfWeek day)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 daysToAdd = ((Int32)day - (Int32)dateTime.DayOfWeek - 7) % 7;
        return dateTime.AddDays(daysToAdd);
    }

    public static DateTime GetSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        return new System.Globalization.GregorianCalendar().AddDays(result, -((Int32)result.DayOfWeek) + 6);
    }

    public static DateTime GetShifted(this DateTime dateTime, Int32 shift)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(shift);
    }

    public static DateTime GetSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        return new System.Globalization.GregorianCalendar().AddDays(result, -((Int32)result.DayOfWeek));
    }
}