using AT.Extensions.DateTimes.Georgian.Addition;
using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Collections;

namespace AT.Extensions.DateTimes.Georgian.Comparison;
public static class Extensions : Object
{
    public static bool IsAfter(this DateTime current, DateTime other)
    {
        bool result = default;
        // ----------------------------------------------------------------------------------------------------
        if (current == default)
            throw new ArgumentNullException(nameof(current));
        else if (other == default)
            throw new ArgumentNullException(nameof(other));
        // ----------------------------------------------------------------------------------------------------
        if (current.CompareTo(other).Equals((int)AT.Enums.DateComparison.IsFuture))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsBefore(this DateTime current, DateTime other)
    {
        bool result = default;
        // ----------------------------------------------------------------------------------------------------
        if (current == default)
            throw new ArgumentNullException(nameof(current));
        else if (other == default)
            throw new ArgumentNullException(nameof(other));
        // ----------------------------------------------------------------------------------------------------
        if (current.CompareTo(other).Equals((int)AT.Enums.DateComparison.IsPast))
            result = true;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static bool IsBetween(this DateTime dateTime, DateTime startDate, DateTime endDate, Boolean compareTime = default)
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

    public static bool IsBetween(this DateTime dateTime, DateTime rangeBegining, DateTime rangeEnd)
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

    public static bool IsDatePM(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime >= dateTime.Noon())
               && (dateTime < dateTime.AddDays(1).Midnight());
    }

    public static bool IsDateTimeWithinXRangeOfAnotherDateTime(this DateTime dateTime, int interval, AT.Enums.DateTimeDifferenceFormat DateTimeFormat, DateTime comparisonDate)
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
            case AT.Enums.DateTimeDifferenceFormat.Milliseconds:
                {
                    _min = _min.AddMilliseconds(-interval);
                    _max = _max.AddMilliseconds(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Seconds:
                {
                    _min = _min.AddSeconds(-interval);
                    _max = _max.AddSeconds(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Minutes:
                {
                    _min = _min.AddMinutes(-interval);
                    _max = _max.AddMinutes(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Hours:
                {
                    _min = _min.AddHours(-interval);
                    _max = _max.AddHours(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Days:
                {
                    _min = _min.AddDays(-interval);
                    _max = _max.AddDays(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Weeks:
                {
                    _min = _min.AddWeeks(-interval);
                    _max = _max.AddWeeks(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Months:
                {
                    _min = _min.AddMonths(-interval);
                    _max = _max.AddMonths(interval);
                }
                break;

            case AT.Enums.DateTimeDifferenceFormat.Years:
                {
                    _min = _min.AddYears(-interval);
                    _max = _max.AddYears(interval);
                }
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        return _min <= dateTime && dateTime <= _max;
    }

    public static bool IsEarlierThan(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(value) < 0;
    }

    public static bool IsEndOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Day == DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    public static bool IsEndOfWeek(this DateTime dateTime, DayOfWeek weekEnd = DayOfWeek.Saturday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == weekEnd;
    }

    public static bool IsFriday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Friday;
    }

    public static bool IsHoliday(this DateTime dateTime)
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

    public static bool IsHolidayOrSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IsSunday() || dateTime.IsHoliday();
    }

    public static bool IsInRange(this DateTime dateTime, DateTime startDate, DateTime endDate)
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

    public static bool IsIntersects(this DateTime startDate, DateTime endDate, DateTime intersectingStartDate, DateTime intersectingEndDate)
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

    public static bool IsLastDayOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime == new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
    }

    public static bool IsLaterThan(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(value) > 0;
    }

    public static bool IsLeapDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapDay(dateTime.Year, dateTime.Month, dateTime.Day, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static bool IsLeapMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapMonth(dateTime.Year, dateTime.Month, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }
    
    public static bool IsLeapYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.IsLeapYear(dateTime.Year);
        //return System.Globalization.CultureInfo.InvariantCulture.Calendar.IsLeapYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static bool IsLeapYear(this int year)
    {
        return DateTime.IsLeapYear(year);
    }

    public static bool IsMonday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Monday;
    }

    public static bool IsSameDate(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date == value.Date;
    }

    public static bool IsSameTime(this DateTime dateTime, DateTime value)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        return dateTime - dateTime.Date == value - value.Date;
    }

    public static bool IsSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Saturday;
    }

    public static bool IsStartOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Day == 1;
    }

    public static bool IsSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Sunday;
    }

    public static bool IsThursday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Thursday;
    }

    public static bool IsTuesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Tuesday;
    }

    public static bool IsValidDate(this int year, int month, int day)
    {
        return (year >= DateTime.MinValue.Year && year <= DateTime.MaxValue.Year)
               && (month >= 1 && month <= 12)
               && (day >= 1 && DateTime.DaysInMonth(year, month) >= day);
    }

    public static bool IsValidDateTime(this int year, int month, int day, int hour, int minute, int second = default)
    {
        return IsValidDate(year, month, day)
               && IsValidTime(hour, minute, second);
    }

    public static bool IsValidTime(this int hour, int minute, int second = default)
    {
        return (hour >= 0 && hour <= 23)
               && (minute >= 0 && minute <= 59)
               && (second >= 0 && second <= 59)
               ;
    }

    public static bool IsWednesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.DayOfWeek == DayOfWeek.Wednesday;
    }

    public static bool IsWeekday(this DayOfWeek dayOfWeek)
    {
        return dayOfWeek switch
        {
            DayOfWeek.Sunday or DayOfWeek.Saturday => false,
            _ => true,
        };
    }

    public static bool IsWeekend(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime.DayOfWeek == DayOfWeek.Sunday || dateTime.DayOfWeek == DayOfWeek.Saturday);
    }

    public static bool IsWeekend(this DayOfWeek dayOfWeek)
    {
        return !dayOfWeek.IsWeekday();
    }

    public static bool WillChangeDate(this DateTime dateTime, double value, AT.Enums.DateTimeDifferenceFormat differenceFormat = AT.Enums.DateTimeDifferenceFormat.Hours)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date != dateTime.Add(value, differenceFormat).Date;
    }

    public static bool WillChangeMonth(this DateTime dateTime, double value, AT.Enums.DateTimeDifferenceFormat differenceFormat = AT.Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Month != dateTime.Add(value, differenceFormat).Month;
    }

    public static bool WillChangeYear(this DateTime dateTime, double value, AT.Enums.DateTimeDifferenceFormat differenceFormat = AT.Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Year != dateTime.Add(value, differenceFormat).Year;
    }
}