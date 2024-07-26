using AT.Extensions.DateTimes.Georgian.Addition;
using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Collections;

namespace AT.Extensions.DateTimes.Georgian.Comparison;
public static class IsExtensions : Object
{
    public static Boolean IsAfter(this DateTime current, DateTime other)
    {
        Boolean result = default;
        // ----------------------------------------------------------------------------------------------------
        if (current == default)
            throw new ArgumentNullException(nameof(current));
        else if (other == default)
            throw new ArgumentNullException(nameof(other));
        // ----------------------------------------------------------------------------------------------------
        if (current.CompareTo(other).Equals((Int32)Enums.DateComparison.IsFuture))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean IsBefore(this DateTime current, DateTime other)
    {
        Boolean result = default;
        // ----------------------------------------------------------------------------------------------------
        if (current == default)
            throw new ArgumentNullException(nameof(current));
        else if (other == default)
            throw new ArgumentNullException(nameof(other));
        // ----------------------------------------------------------------------------------------------------
        if (current.CompareTo(other).Equals((Int32)Enums.DateComparison.IsPast))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static Boolean IsBetween(this DateTime dateTime, DateTime startDate, DateTime endDate, Boolean compareTime = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        else if (endDate == default)
            throw new ArgumentNullException(nameof(endDate));
        // ----------------------------------------------------------------------------------------------------
        return compareTime 
               ? (dateTime >= startDate && dateTime <= endDate) 
               : (dateTime.Date >= startDate.Date && dateTime.Date <= endDate.Date);
    }

    public static Boolean IsBetween(this DateTime dateTime, DateTime rangeBegining, DateTime rangeEnd)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (rangeBegining == default)
            throw new ArgumentNullException(nameof(rangeBegining));
        else if (rangeEnd == default)
            throw new ArgumentNullException(nameof(rangeEnd));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Ticks >= rangeBegining.Ticks
               && dateTime.Ticks < rangeEnd.Ticks;
    }

    public static Boolean IsDatePM(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime >= dateTime.Noon())
               && (dateTime < dateTime.AddDays(1).Midnight());
    }

    public static Boolean IsDateTimeWithinXRangeOfAnotherDateTime(this DateTime dateTime, Int32 interval, Enums.DateTimeDifferenceFormat DateTimeFormat, DateTime comparisonDate)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (comparisonDate == default)
            throw new ArgumentNullException(nameof(comparisonDate));
        // ----------------------------------------------------------------------------------------------------
        DateTime _min = comparisonDate;
        DateTime _max = comparisonDate;
        // ----------------------------------------------------------------------------------------------------
        switch (DateTimeFormat)
        {
            case Enums.DateTimeDifferenceFormat.Milliseconds:
                {
                    _min = _min.AddMilliseconds(-interval);
                    _max = _max.AddMilliseconds(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Seconds:
                {
                    _min = _min.AddSeconds(-interval);
                    _max = _max.AddSeconds(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Minutes:
                {
                    _min = _min.AddMinutes(-interval);
                    _max = _max.AddMinutes(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Hours:
                {
                    _min = _min.AddHours(-interval);
                    _max = _max.AddHours(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Days:
                {
                    _min = _min.AddDays(-interval);
                    _max = _max.AddDays(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Weeks:
                {
                    _min = _min.AddWeeks(-interval);
                    _max = _max.AddWeeks(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Months:
                {
                    _min = _min.AddMonths(-interval);
                    _max = _max.AddMonths(interval);
                }
                break;

            case Enums.DateTimeDifferenceFormat.Years:
                {
                    _min = _min.AddYears(-interval);
                    _max = _max.AddYears(interval);
                }
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        return _min <= dateTime && dateTime <= _max;
    }

    public static Boolean IsEarlierThan(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(value) < 0;
    }

    public static Boolean IsEndOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    public static Boolean IsEndOfWeek(this DateTime dateTime, DayOfWeek weekEnd = DayOfWeek.Saturday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == weekEnd;
    }

    public static Boolean IsFriday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Friday;
    }

    public static Boolean IsHoliday(this DateTime dateTime)
    {
        IReadOnlyCollection<DateTime>? allHolidays = default;
        // ----------------------------------------------------------------------------------------------------
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        allHolidays = dateTime.Year.AllHolidays();

        if (allHolidays is not null)
            return allHolidays.Any(holiday => holiday.Date == dateTime.Date);
        // ----------------------------------------------------------------------------------------------------
        return false;
    }

    public static Boolean IsHolidayOrSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IsSunday() || dateTime.IsHoliday();
    }

    public static Boolean IsInRange(this DateTime dateTime, DateTime startDate, DateTime endDate)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        else if (endDate == default)
            throw new ArgumentNullException(nameof(endDate));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime >= startDate && dateTime <= endDate);
    }

    public static Boolean IsIntersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
    {
        if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        else if (endDate == default)
            throw new ArgumentNullException(nameof(endDate));
        else if (intersectingStartDate == default)
            throw new ArgumentNullException(nameof(intersectingStartDate));
        else if (intersectingEndDate == default)
            throw new ArgumentNullException(nameof(intersectingEndDate));
        // ----------------------------------------------------------------------------------------------------
        return (intersectingEndDate >= startDate && intersectingStartDate <= endDate);
    }

    public static Boolean IsLastDayOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
    }

    public static Boolean IsLaterThan(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(value) > 0;
    }

    public static Boolean IsLeapDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapDay(dateTime.Year, dateTime.Month, dateTime.Day, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Boolean IsLeapMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapMonth(dateTime.Year, dateTime.Month, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }
    
    public static Boolean IsLeapYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.IsLeapYear(dateTime.Year);
        //return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Boolean IsLeapYear(this Int32 year)
    {
        return DateTime.IsLeapYear(year);
    }

    public static Boolean IsMonday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Monday;
    }

    public static Boolean IsSameDate(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date == value.Date;
    }

    public static Boolean IsSameTime(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime - dateTime.Date == value - value.Date;
    }

    public static Boolean IsSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Saturday;
    }

    public static Boolean IsStartOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Day == 1;
    }

    public static Boolean IsSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Sunday;
    }

    public static Boolean IsThursday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Thursday;
    }

    public static Boolean IsTuesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Tuesday;
    }

    public static Boolean IsValidDate(this Int32 year, Int32 month, Int32 day)
    {
        return (year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year)
               && (month >= 1 && month <= 12)
               && (day >= 1 && DateTime.DaysInMonth(year, month) >= day);
    }

    public static Boolean IsValidDateTime(this Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second = default)
    {
        return IsValidDate(year, month, day)
               && IsValidTime(hour, minute, second);
    }

    public static Boolean IsValidTime(this Int32 hour, Int32 minute, Int32 second = default)
    {
        return (hour >= 0 && hour <= 23)
               && (minute >= 0 && minute <= 59)
               && (second >= 0 && second <= 59)
               ;
    }

    public static Boolean IsWednesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Wednesday;
    }

    public static Boolean IsWeekday(this DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Sunday or DayOfWeek.Saturday => false,
            _ => true,
        };
    }

    public static Boolean IsWeekend(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday);
    }

    public static Boolean IsWeekend(this DayOfWeek dayOfWeek)
    {
        return !dayOfWeek.IsWeekday();
    }
}