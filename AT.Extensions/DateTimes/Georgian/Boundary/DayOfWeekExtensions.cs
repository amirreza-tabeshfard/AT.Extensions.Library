namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class DayOfWeekExtensions
{
    public static DateTime DayOfWeekAfter(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 _valueDayOfWeek = (Int32)dateTime.DayOfWeek;
        Int32 _targetDayOfWeek = (Int32)DayOfWeek;
        Int32 _difference = _targetDayOfWeek - _valueDayOfWeek;

        if (_difference <= 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(_difference);
    }

    public static DateTime DayOfWeekAfter(this DateTime dateTime, Int32 weeks)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 days = weeks * 7;
        return dateTime.AddDays(days);
    }

    public static DateTime DayOfWeekBefore(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 _valueDayOfWeek = (Int32)dateTime.DayOfWeek;
        Int32 _targetDayOfWeek = (Int32)DayOfWeek;
        Int32 _difference = _valueDayOfWeek - _targetDayOfWeek;

        if (_difference <= 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-_difference);
    }

    public static DateTime DayOfWeekBefore(this DateTime dateTime, Int32 weeks)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 days = weeks * 7;
        return dateTime.AddDays(-days);
    }

    public static DateTime DayOfWeekOnOrAfter(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 _valueDayOfWeek = (Int32)dateTime.DayOfWeek;
        Int32 _targetDayOfWeek = (Int32)DayOfWeek;
        Int32 _difference = _targetDayOfWeek - _valueDayOfWeek;

        if (_difference < 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(_difference);
    }

    public static DateTime DayOfWeekOnOrBefore(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 _valueDayOfWeek = (Int32)dateTime.DayOfWeek;
        Int32 _targetDayOfWeek = (Int32)DayOfWeek;
        Int32 _difference = _valueDayOfWeek - _targetDayOfWeek;

        if (_difference < 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-_difference);
    }
}