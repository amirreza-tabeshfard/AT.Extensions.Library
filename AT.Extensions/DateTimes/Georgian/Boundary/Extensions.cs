using AT.Extensions.DateTimes.Georgian.Addition;
using AT.Extensions.DateTimes.Georgian.Conversion;
using AT.Extensions.DateTimes.Georgian.Extraction;
using System;

namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class Extensions : Object
{
    #region Private: Method(s)

    private static DateTime WorkMethod(DateTime dateTime, long returnType, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        long interval1 = (long)timeInterval;
        long ticksFromFloor = 0L;
        int intervalFloor = 0;
        int floorOffset = 0;
        int intervalLength = 0;
        DateTime floorDate;
        DateTime ceilingDate;

        if (interval1 > 132L)
        {
            floorDate = new DateTime(dateTime.Ticks - (dateTime.Ticks % interval1), dateTime.Kind);
            if (returnType != 0L)
                ticksFromFloor = interval1 / returnType;
        }
        else if (interval1 < 8L)
        {
            intervalFloor = (int)(interval1) - 1;
            floorOffset = (int)dateTime.DayOfWeek * -1;
            floorDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind).AddDays(-(intervalFloor > floorOffset ? floorOffset + 7 - intervalFloor : floorOffset - intervalFloor));
            if (returnType != 0L)
                ticksFromFloor = TimeSpan.TicksPerDay * 7L / returnType;
        }
        else
        {
            intervalLength = interval1 >= 130L ? 12 : (int)(interval1 / 10L);
            intervalFloor = (int)(interval1 % intervalLength);
            floorOffset = (dateTime.Month - 1) % intervalLength;
            floorDate = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind).AddMonths(-(intervalFloor > floorOffset ? floorOffset + intervalLength - intervalFloor : floorOffset - intervalFloor));
            if (returnType != 0L)
            {
                ceilingDate = floorDate.AddMonths(intervalLength);
                ticksFromFloor = (long)ceilingDate.Subtract(floorDate).Ticks / returnType;
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return floorDate.AddTicks(ticksFromFloor);
    }

    #endregion

    #region BeginningOf

    public static DateTime BeginningOfTheDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, 0);
    }

    public static DateTime BeginningOfTheWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return BeginningOfTheDay(dateTime.AddDays(1 - (int)(dateTime.DayOfWeek)));
    }

    public static DateTime BeginningOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0);
    }

    public static DateTime BeginningOfTheYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, 1, 1, 0, 0, 0);
    }

    public static DateTime BeginningOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.QuarterOfYear() switch
        {
            1 => new DateTime(dateTime.Year, 1, 1, 0, 0, 0, millisecond: 0),
            2 => new DateTime(dateTime.Year, 4, 1, 0, 0, 0, millisecond: 0),
            3 => new DateTime(dateTime.Year, 7, 1, 0, 0, 0, millisecond: 0),
            _ => new DateTime(dateTime.Year, 10, 1, 0, 0, 0, millisecond: 0),
        };
    }

    #endregion

    public static DateTime CurrentDateTimeIn(this DateTime dateTime, string timeZoneById)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, TimeZoneInfo.FindSystemTimeZoneById(timeZoneById));
    }

    #region DateTime

    public static DateTime DateTimeCeiling(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 1L, timeInterval);
    }

    public static DateTime DateTimeCeilingUnbounded(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 1L, timeInterval).AddTicks(-1);
    }

    public static DateTime DateTimeFloor(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 0L, timeInterval);
    }

    public static DateTime DateTimeMidpoint(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 2L, timeInterval);
    }

    public static DateTime DateTimeRound(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (dateTime >= WorkMethod(dateTime, 2L, timeInterval))
            return WorkMethod(dateTime, 1L, timeInterval);
        else
            return WorkMethod(dateTime, 0L, timeInterval);
    }

    #endregion

    #region DayOfWeek

    public static DateTime DayOfWeekAfter(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int _valueDayOfWeek = (int)dateTime.DayOfWeek;
        int _targetDayOfWeek = (int)DayOfWeek;
        int _difference = _targetDayOfWeek - _valueDayOfWeek;

        if (_difference <= 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(_difference);
    }

    public static DateTime DayOfWeekAfter(this DateTime dateTime, int weeks)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int days = weeks * 7;
        return dateTime.AddDays(days);
    }

    public static DateTime DayOfWeekBefore(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int _valueDayOfWeek = (int)dateTime.DayOfWeek;
        int _targetDayOfWeek = (int)DayOfWeek;
        int _difference = _valueDayOfWeek - _targetDayOfWeek;

        if (_difference <= 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-_difference);
    }

    public static DateTime DayOfWeekBefore(this DateTime dateTime, int weeks)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int days = weeks * 7;
        return dateTime.AddDays(-days);
    }

    public static DateTime DayOfWeekOnOrAfter(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int _valueDayOfWeek = (int)dateTime.DayOfWeek;
        int _targetDayOfWeek = (int)DayOfWeek;
        int _difference = _targetDayOfWeek - _valueDayOfWeek;

        if (_difference < 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(_difference);
    }

    public static DateTime DayOfWeekOnOrBefore(this DateTime dateTime, DayOfWeek DayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int _valueDayOfWeek = (int)dateTime.DayOfWeek;
        int _targetDayOfWeek = (int)DayOfWeek;
        int _difference = _valueDayOfWeek - _targetDayOfWeek;

        if (_difference < 0)
            _difference += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-_difference);
    }

    #endregion

    #region EndOf

    public static DateTime EndOfSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfSecond().AddSeconds(1).AddTicks(-1);
    }

    public static DateTime EndOfMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfMinute().AddMinutes(1).AddTicks(-1);
    }

    public static DateTime EndOfHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddHours(1).AddTicks(-1);
    }

    public static DateTime EndOfTheDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddDays(1).AddTicks(-1);
    }

    public static DateTime EndOfTheWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return EndOfTheDay(dateTime.AddDays(7 - (int)(dateTime.DayOfWeek)));
    }

    public static DateTime EndOfTheWeek(this DateTime dateTime, DayOfWeek firstWeekDay = DayOfWeek.Sunday)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int dayDiff = (int)firstWeekDay - (int)dateTime.DayOfWeek;

        if (dayDiff <= 0)
            dayDiff += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(dayDiff);
    }

    public static DateTime EndOfTheMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1)
               .AddMonths(1)
               .AddDays(-1);
    }

    public static DateTime EndOfTheYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.StartOfHour().AddYears(1).AddTicks(-1);
    }

    public static DateTime EndOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.QuarterOfYear() switch
        {
            1 => new DateTime(dateTime.Year, 3, 31, 23, 59, 49),
            2 => new DateTime(dateTime.Year, 6, 30, 23, 59, 49),
            3 => new DateTime(dateTime.Year, 9, 30, 23, 59, 49),
            _ => new DateTime(dateTime.Year, 12, 31, 23, 59, 49),
        };
    }

    #endregion

    public static DateTime FromUnixTime(this double unixTimestamp)
    {
        return new DateTime(1970, 1, 1).Add(unixTimestamp, AT.Enums.DateTimeDifferenceFormat.Seconds);
    }

    #region Get

    public static DateTime GetCyclic(this DateTime dateTime, IEnumerable<DateTime> cycle)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime result = dateTime;

        if (cycle?.Any() ?? false)
        {
            DateTime from = cycle.Min();
            DateTime to = cycle.Max();

            int duration = to.GetAbsDuration(from).Days + 1;

            if (duration < 2)
                result = from;
            else if (dateTime < from)
            {
                int distance = from.GetAbsDuration(dateTime).Days - 1;
                result = to.AddDays((distance % duration) * -1);
            }
            else if (dateTime > to)
            {
                int distance = dateTime.GetAbsDuration(to).Days - 1;
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
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int iCurr = (int)dateTime.DayOfWeek;
        int iTarg = (int)targetDayOfWeek;
        int nTarg;

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
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(7);
    }

    public static DateTime GetFirstDayOfWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        System.Globalization.CultureInfo culture = Thread.CurrentThread.CurrentCulture;
        int diff = dateTime.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        // ----------------------------------------------------------------------------------------------------
        if (diff < 0)
            diff += 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-diff).Date;
    }

    public static DateTime GetLastDayOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1).AddMonths(1).AddDays(-1);
    }

    public static DateTime GetLastDayOfPreviousWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(-1);
    }

    public static DateTime GetLastDayOfWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetFirstDayOfWeek(dateTime).AddDays(6);
    }

    public static DateTime GetLastWeekdayOfMonth(this DayOfWeek day, int year, int month)
    {
        DateTime lastDayOfTheMonth = new DateTime(year, month, 1)
                                            .AddMonths(1)
                                            .AddDays(-1);

        int searchDay = (int)day;
        int lastDay = (int)lastDayOfTheMonth.DayOfWeek;

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
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int daysToAdd = ((int)day - (int)dateTime.DayOfWeek + 7) % 7;
        return dateTime.AddDays(daysToAdd);
    }

    public static DateTime GetPrevious(this DateTime dateTime, DayOfWeek day)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int daysToAdd = ((int)day - (int)dateTime.DayOfWeek - 7) % 7;
        return dateTime.AddDays(daysToAdd);
    }

    public static DateTime GetSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        return new System.Globalization.GregorianCalendar().AddDays(result, -((int)result.DayOfWeek) + 6);
    }

    public static DateTime GetShifted(this DateTime dateTime, int shift)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(shift);
    }

    public static DateTime GetSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime result = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second);
        return new System.Globalization.GregorianCalendar().AddDays(result, -((int)result.DayOfWeek));
    }

    #endregion

    public static DateTime In(this DateTime dateTime, TimeZoneInfo timeZoneInfo)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTime(dateTime, timeZoneInfo);
    }

    #region Ignore

    public static DateTime IgnoreTimeSpan(this DateTime dateTime, TimeSpan timeSpan)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (timeSpan == TimeSpan.Zero)
            return dateTime;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
    }

    public static DateTime IgnoreMilliseconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromMilliseconds(1000));
    }

    public static DateTime IgnoreSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromSeconds(60));
    }

    public static DateTime IgnoreMinutes(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromMinutes(60));
    }

    public static DateTime IgnoreHours(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromHours(24));
    }

    #endregion

    #region Last
        
    public static DateTime LastSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddSeconds(-1);
    }

    public static DateTime LastMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMinutes(-1);
    }

    public static DateTime LastHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddHours(-1);
    }

    public static DateTime LastDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-1);
    }

    public static DateTime LastWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-7);
    }

    public static DateTime LastMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMonths(-1);
    }

    public static DateTime LastYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddYears(-1);
    }

    #endregion

    #region Local

    public static DateTime Local(this DateTime utcDate, string timeZoneName)
    {
        if (utcDate == default)
            throw new ArgumentNullException($"utcDate is '{default(DateTime)}'");
        else if (string.IsNullOrEmpty(timeZoneName))
            throw new ArgumentNullException($"timeZoneName is '{default(string)}'");
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeFromUtc(utcDate, TimeZoneConverter.TZConvert.GetTimeZoneInfo(timeZoneName));
    }

    public static DateTime Local(this DateTime utcDate, AT.Infrastructure.SystemTimeZone systemTimeZone)
    {
        if (utcDate == default)
            throw new ArgumentNullException($"utcDate is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return Local(utcDate, systemTimeZone.ToString());
    }

    public static DateTime LocalTimeToServerTime(this DateTime localTime, string timeZoneName)
    {
        if (localTime == default)
            throw new ArgumentNullException($"localTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return TimeZone.CurrentTimeZone.ToLocalTime(localTime.ToUniversalTime(timeZoneName));
    }

    public static DateTime LocalTimeToServerTime(this DateTime localTime, AT.Infrastructure.SystemTimeZone serverTimeZone)
    {
        if (localTime == default)
            throw new ArgumentNullException($"localTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return localTime.LocalTimeToServerTime(serverTimeZone.ToString());
    }

    #endregion

    #region Next

    public static DateTime Next(this DateTime from, DayOfWeek dayOfWeek)
    {
        if (from == default)
            throw new ArgumentNullException($"from is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int start = (int)from.DayOfWeek;
        int target = (int)dayOfWeek;

        if (target <= start)
            target += 7;
        // ----------------------------------------------------------------------------------------------------
        return from.AddDays(target - start);
    }

    public static DateTime NextAnniversary(this DateTime dateTime, int eventMonth, int eventDay, bool preserveMonth = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
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

    public static DateTime NextAnniversary(this DateTime dateTime, DateTime eventDate, bool preserveMonth = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddSeconds(1);
    }

    public static DateTime NextMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMinutes(1);
    }
        
    public static DateTime NextHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddHours(1);
    }

    public static DateTime NextDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(1);
    }

    public static DateTime NextDayOfWeek(this DateTime dateTime, DayOfWeek dayOfWeek)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeTemp = new System.Globalization.GregorianCalendar().AddDays(dateTime, -((int)dateTime.DayOfWeek) + (int)dayOfWeek);
        return (dateTimeTemp.Day < dateTime.Day)
               ? dateTimeTemp.AddDays(7)
               : dateTimeTemp;
    }

    public static DateTime NextMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMonths(1);
    }

    public static DateTime NextSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().AddDays(dateTime, -((int)dateTime.DayOfWeek) + 7);
    }

    public static DateTime NextWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(7);
    }

    public static DateTime NextYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddYears(1);
    }

    #endregion

    #region Previous

    public static DateTime PreviousDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-1);
    }

    public static DateTime PreviousMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMonths(-1);
    }

    public static DateTime PreviousWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-7);
    }

    public static DateTime PreviousYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddYears(-1);
    }

    #endregion

    #region Start

    public static DateTime StartOfSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, millisecond: 0);
    }

    public static DateTime StartOfMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, millisecond: 0);
    }

    public static DateTime StartOfHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, millisecond: 0);
    }

    public static DateTime StartOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1);
    }

    public static DateTime StartOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, 1, 1);
    }

    public static DateTime StartOfWeek(this DateTime dateTime, DayOfWeek firstWeekDay = DayOfWeek.Sunday)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int dayDiff = (int)firstWeekDay - (int)dateTime.DayOfWeek;

        if (dayDiff > 0)
            dayDiff -= 7;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(dayDiff);
    }

    public static DateTime StartOfQuarter(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int quarterStartMonth = dateTime.Month - (dateTime.Month % 3) + 1;
        return new DateTime(dateTime.Year, quarterStartMonth, 1);
    }

    #endregion

    #region ThisWeek

    public static DateTime ThisWeekSunday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Sunday);
    }

    public static DateTime ThisWeekMonday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Monday);
    }

    public static DateTime ThisWeekTuesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Tuesday);
    }

    public static DateTime ThisWeekWednesday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Wednesday);
    }

    public static DateTime ThisWeekThursday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Thursday);
    }

    public static DateTime ThisWeekFriday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Friday);
    }

    public static DateTime ThisWeekSaturday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime today = DateTime.Now;
        return new System.Globalization.GregorianCalendar().AddDays(today, -((byte)today.DayOfWeek) + (byte)DayOfWeek.Saturday);
    }

    #endregion

    #region Working

    public static DateTime WorkingDayAfter(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return source.AddDays(1).WorkingDayOnOrAfter(publicHolidays);
    }

    public static DateTime WorkingDayBefore(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return source.AddDays(-1).WorkingDayOnOrBefore(publicHolidays);
    }

    public static DateTime WorkingDayOnOrAfter(this DateTime source, DayOfWeek publicHoliday1 = DayOfWeek.Sunday, DayOfWeek publicHoliday2 = DayOfWeek.Saturday)
    {
        if (source == default)
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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
            throw new ArgumentNullException($"source is '{default(DateTime)}'");
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

    #endregion

    #region Types of day-specific naming

    public static DateTime Dawn(this DateTime dateTime, double Latitude, double Longitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        double m = 720 + (4 * (-Longitude - dateTime.HourAngleDawn(Latitude, Kind))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Dusk(this DateTime dateTime, double Latitude, double Longitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        double m = 720 + (4 * (-Longitude - dateTime.HourAngleDusk(Latitude, Kind))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Midnight(this DateTime dateTime)
    {
        return dateTime.Date;
    }

    public static DateTime Noon(this DateTime dateTime)
    {
        return dateTime.SetTime(12, 0, 0);
    }

    public static DateTime SolarNoon(this DateTime dateTime, double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan _eqTime = dateTime.EquationOfTimeTotal();
        double m = 720 + (-Longitude * 4) - _eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Sunrise(this DateTime dateTime, double Latitude, double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        double m = 720 + (4 * (-Longitude - dateTime.HourAngleSunrise(Latitude))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Sunset(this DateTime dateTime, double Latitude, double Longitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan eqTime = dateTime.EquationOfTimeTotal();
        double m = 720 + (4 * (-Longitude - dateTime.HourAngleSunset(Latitude))) - eqTime.TotalMinutes;
        TimeSpan val = m.AsMinutesToTimeSpan();
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, val.Hours, val.Minutes, val.Seconds, val.Milliseconds, DateTimeKind.Utc);
    }

    public static DateTime Tomorrow(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(1);
    }

    public static DateTime Yesterday(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.AddDays(-1);
    }

    #endregion

    #region Ago Or Before

    public static DateTime DaysAgo(this DateTime source, int days)
    {
        return source.AddDays(-days);
    }

    public static DateTime WeeksAgo(this DateTime source, int weeks)
    {
        var days = weeks * 7;
        return source.AddDays(-days);
    }

    public static DateTime MonthsAgo(this DateTime source, int months)
    {
        return source.AddMonths(-months);
    }

    public static DateTime YearsAgo(this DateTime source, int years)
    {
        return source.AddYears(-years);
    }

    public static DateTime DaysAfter(this DateTime source, int days)
    {
        return source.AddDays(days);
    }

    public static DateTime WeeksAfter(this DateTime source, int weeks)
    {
        var days = weeks * 7;
        return source.AddDays(days);
    }

    public static DateTime MonthsAfter(this DateTime source, int months)
    {
        return source.AddMonths(months);
    }

    public static DateTime YearsAfter(this DateTime source, int years)
    {
        return source.AddYears(years);
    }

    #endregion
}