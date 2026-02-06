namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class GetExtensions
{
    public static TimeSpan GetAbsDuration(this TimeSpan from, TimeSpan to)
    {
        if (from == default)
            throw new ArgumentNullException(nameof(from));
        else if (to == default)
            throw new ArgumentNullException(nameof(to));
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(Math.Abs(to.Subtract(from).Ticks));
    }

    public static TimeSpan GetAbsDuration(this DateTime from, DateTime to)
    {
        if (from == default)
            throw new ArgumentNullException(nameof(from));
        else if (to == default)
            throw new ArgumentNullException(nameof(to));
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(Math.Abs(to.Subtract(from).Ticks));
    }

    public static Int32 GetDayOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(dateTime);
    }

    public static Int32 GetDaysInMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        //return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(dateTime.Year, dateTime.Month, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Int32 GetDaysInYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDaysInYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Int32 GetEra(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime);
    }

    public static Int32 GetHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetHour(dateTime);
    }

    public static Int32 GetLeapMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetLeapMonth(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Double GetMilliseconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMilliseconds(dateTime);
    }

    public static DateTime GetMaximum(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException(nameof(dateTimeFirst));
        else if (dateTimeSecond == default)
            throw new ArgumentNullException(nameof(dateTimeSecond));
        // ----------------------------------------------------------------------------------------------------
        if (dateTimeFirst >= dateTimeSecond)
            return dateTimeFirst;
        // ----------------------------------------------------------------------------------------------------
        return dateTimeSecond;
    }

    public static DateTime GetMinimum(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException(nameof(dateTimeFirst));
        else if (dateTimeSecond == default)
            throw new ArgumentNullException(nameof(dateTimeSecond));
        // ----------------------------------------------------------------------------------------------------
        if (dateTimeFirst <= dateTimeSecond)
            return dateTimeFirst;
        // ----------------------------------------------------------------------------------------------------
        return dateTimeSecond;
    }

    public static Int32 GetMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMinute(dateTime);
    }

    public static Int32 GetMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMonth(dateTime);
    }

    public static Int32 GetMonthDiff(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException(nameof(dateTimeFirst));
        else if (dateTimeSecond == default)
            throw new ArgumentNullException(nameof(dateTimeSecond));
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeLeft = (dateTimeFirst < dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        DateTime dateTimeRigth = (dateTimeFirst >= dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        // ----------------------------------------------------------------------------------------------------
        return (dateTimeLeft.Day == dateTimeRigth.Day ? default : dateTimeLeft.Day > dateTimeRigth.Day ? default : 1)
               + (dateTimeLeft.Month == dateTimeRigth.Month ? default : dateTimeRigth.Month - dateTimeLeft.Month)
               + (dateTimeLeft.Year == dateTimeRigth.Year ? default : (dateTimeRigth.Year - dateTimeLeft.Year) * 12);
    }

    public static Int32 GetMonthsInYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMonthsInYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static Int32 GetSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetSecond(dateTime);
    }

    public static Double GetTotalMonthDiff(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException(nameof(dateTimeFirst));
        else if (dateTimeSecond == default)
            throw new ArgumentNullException(nameof(dateTimeSecond));
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeLeft = (dateTimeFirst < dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        DateTime dateTimeRigth = (dateTimeFirst >= dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        Int32 dateTimeLeftDfM = DateTime.DaysInMonth(dateTimeLeft.Year, dateTimeLeft.Month);
        Int32 dateTimeRigthDfM = DateTime.DaysInMonth(dateTimeRigth.Year, dateTimeRigth.Month);
        // ----------------------------------------------------------------------------------------------------
        Double dayFixOne = dateTimeLeft.Day == dateTimeRigth.Day
                           ? 0d
                           : dateTimeLeft.Day > dateTimeRigth.Day
                           ? dateTimeRigth.Day * 1d / dateTimeRigthDfM - dateTimeLeft.Day * 1d / dateTimeLeftDfM
                           : ((dateTimeRigth.Day - dateTimeLeft.Day) * 1d / dateTimeRigthDfM);
        // ----------------------------------------------------------------------------------------------------
        return dayFixOne
               + (dateTimeLeft.Month == dateTimeRigth.Month ? 0 : dateTimeRigth.Month - dateTimeLeft.Month)
               + (dateTimeLeft.Year == dateTimeRigth.Year ? 0 : (dateTimeRigth.Year - dateTimeLeft.Year) * 12);
    }

    public static TimeSpan GetUtcOffset(this DateTime dateTime, String dateTimeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneConverter.TZConvert.GetTimeZoneInfo(dateTimeZoneName)
               .GetUtcOffset(dateTime);
    }

    public static TimeSpan GetUtcOffset(this DateTime dateTime, Infrastructure.SystemTimeZone dateTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffset(dateTime, dateTimeZone.ToString());
    }

    public static Int32 GetUtcOffsetInteger(this DateTime dateTime, String dateTimeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffset(dateTime, dateTimeZoneName)
               .Hours;
    }

    public static Int32 GetUtcOffsetInteger(this DateTime dateTime, Infrastructure.SystemTimeZone dateTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffsetInteger(dateTime, dateTimeZone.ToString());
    }

    public static Int32 GetWeekOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dateTime, System.Globalization.DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
    }

    public static Int32 GetYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetYear(dateTime);
    }
}