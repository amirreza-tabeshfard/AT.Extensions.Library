﻿using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class Extensions : Object
{
    #region Field(s)

    private const double AverageAngle = 0.985653;
    private const double E360OverPi = 1.915169;
    private const double MinutesPerDegree = 3.98892;
    private const double MaxEarthTilt = 23.45;
    private const double VAtVernalEquinox = 78.74611803;

    private static readonly DateTime _epochDateTime;
    private static readonly long _epochTicks;

    #endregion

    #region Constructor

    static Extensions()
    {
        _epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        _epochTicks = 621355968000000000L;
    }

    #endregion

    #region Private: Method(s)

    private static string ToClientFormat(DateTime dateTime, string format)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (dateTime == DateTime.MinValue)
            return string.Empty;
        // ----------------------------------------------------------------------------------------------------
        TimeSpan offsetSpan = AT.Infrastructure.LocalTimeZoneConfig.TimeZone.GetUtcOffset(dateTime);
        DateTimeOffset offset = new DateTimeOffset(dateTime.Ticks, offsetSpan);
        // ----------------------------------------------------------------------------------------------------
        return offset.ToString(format);
    }

    #endregion

    public static decimal AgeExactYears(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException($"referenceDate is '{default(TimeSpan)}'");
        // ----------------------------------------------------------------------------------------------------
        if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return Math.Round(((decimal)(today.Year * 12 + today.Month) - (referenceDate.Year * 12 + referenceDate.Month)) / 12, 2);
    }

    public static double AsDegreesToRadians(this double value)
    {
        return (Math.PI / 180) * value;
    }

    public static TimeSpan AsMinutesToTimeSpan(this double value)
    {
        double h = Math.Floor(value / 60);
        value -= h * 60;
        double s = (value - (int)value) * 60;
        double l = (s - (int)s) * 1000;

        return new TimeSpan(0, (int)h, (int)value, (int)s, (int)l);
    }

    public static double AsRadiansToDegrees(this double value)
    {
        return (180 / Math.PI) * value;
    }

    public static double CompareTo(this DateTime dateTime, DateTime value, AT.Enums.DateTimeDifferenceFormat differenceFormat = AT.Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (value == default)
            throw new ArgumentNullException($"value is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan result = dateTime - value;
        return differenceFormat switch
        {
            AT.Enums.DateTimeDifferenceFormat.Days => result.TotalDays,
            AT.Enums.DateTimeDifferenceFormat.Hours => result.TotalHours,
            AT.Enums.DateTimeDifferenceFormat.Milliseconds => result.TotalMilliseconds,
            AT.Enums.DateTimeDifferenceFormat.Minutes => result.TotalMinutes,
            AT.Enums.DateTimeDifferenceFormat.Months => result.TotalDays / 30,
            AT.Enums.DateTimeDifferenceFormat.Seconds => result.TotalSeconds,
            AT.Enums.DateTimeDifferenceFormat.Weeks => result.TotalDays / 7,
            AT.Enums.DateTimeDifferenceFormat.Years => result.TotalDays / 365,
            _ => default,
        };
    }

    public static int CompareWithoutMinutes(this DateTime dateTime, DateTime toDateTimeCompare)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (toDateTimeCompare == default)
            throw new ArgumentNullException($"toDateTimeCompare is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, millisecond: 0);
        toDateTimeCompare = new DateTime(toDateTimeCompare.Year, toDateTimeCompare.Month, toDateTimeCompare.Day, toDateTimeCompare.Hour, 0, 0, millisecond: 0);
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(toDateTimeCompare);
    }

    public static long DateDiff(this DateTime startDate, System.String datePart, DateTime endDate)
    {
        if (startDate == default)
            throw new ArgumentNullException($"startDate is '{default(DateTime)}'");
        else if (string.IsNullOrEmpty(datePart))
            throw new ArgumentNullException($"datePart is '{default}'");
        else if (endDate == default)
            throw new ArgumentNullException($"endDate is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        Int64 DateDiffVal = 0;
        System.Globalization.Calendar calendar = Thread.CurrentThread.CurrentCulture.Calendar;
        TimeSpan timeSpan = new TimeSpan(endDate.Ticks - startDate.Ticks);
        // ----------------------------------------------------------------------------------------------------
        switch (datePart.ToLower().Trim())
        {
            #region year
            case "year":
            case "yy":
            case "yyyy":
                {
                    DateDiffVal = (Int64)(calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate));
                }
                break;
            #endregion

            #region quarter
            case "quarter":
            case "qq":
            case "q":
                {
                    DateDiffVal = (Int64)((((calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate)) * 4)
                                  + ((calendar.GetMonth(endDate) - 1) / 3))
                                  - ((calendar.GetMonth(startDate) - 1) / 3));
                }
                break;
            #endregion

            #region month
            case "month":
            case "mm":
            case "m":
                {
                    DateDiffVal = (Int64)(((calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate)) * 12
                                  + calendar.GetMonth(endDate))
                                  - calendar.GetMonth(startDate));
                }
                break;
            #endregion

            #region day
            case "day":
            case "d":
            case "dd":
                {
                    DateDiffVal = (Int64)timeSpan.TotalDays;
                }
                break;
            #endregion

            #region week
            case "week":
            case "wk":
            case "ww":
                {
                    DateDiffVal = (Int64)(timeSpan.TotalDays / 7);
                }
                break;
            #endregion

            #region hour
            case "hour":
            case "hh":
                {
                    DateDiffVal = (Int64)timeSpan.TotalHours;
                }
                break;
            #endregion

            #region minute
            case "minute":
            case "mi":
            case "n":
                {
                    DateDiffVal = (Int64)timeSpan.TotalMinutes;
                }
                break;
            #endregion

            #region second
            case "second":
            case "ss":
            case "s":
                {
                    DateDiffVal = (Int64)timeSpan.TotalSeconds;
                }
                break;
            #endregion

            #region millisecond
            case "millisecond":
            case "ms":
                {
                    DateDiffVal = (Int64)timeSpan.TotalMilliseconds;
                }
                break;
            #endregion

            default:
                throw new Exception(System.String.Format("DatePart \"{0}\" is unknown", datePart));
        }
        // ----------------------------------------------------------------------------------------------------
        return DateDiffVal;
    }

    public static double DateTimeToUnixTimeStamp(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return (dateTime.ToUniversalTime().Ticks - _epochTicks) / TimeSpan.TicksPerSecond;
    }

    public static TimeSpan Elapsed(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now.Subtract(dateTime);
    }

    public static double ElapsedSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now.Subtract(dateTime).TotalSeconds;
    }

    public static TimeSpan EquationOfTimeEccentricEffect(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        double h = AverageAngle * (dateTime.DayOfYear - 2);
        double v = h + (E360OverPi * Math.Sin(h.AsDegreesToRadians()));
        double m = ((h - v) * MinutesPerDegree);
        double s = (m - (int)m) * 60;
        double l = (s - (int)s) * 1000;
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(0, 0, (int)m, (int)s, (int)l);
    }

    public static TimeSpan EquationOfTimeTiltEffect(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        double e = AverageAngle * (dateTime.DayOfYear - 80);
        e = (e >= 270 ? e - 360 : (e >= 90 ? e - 180 : e));
        double b = Math.Atan(Math.Cos(MaxEarthTilt.AsDegreesToRadians()) * Math.Tan(e.AsDegreesToRadians())).AsRadiansToDegrees();
        double m = ((e - b) * MinutesPerDegree);
        double s = (m - (int)m) * 60;
        double l = (s - (int)s) * 1000;
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(0, 0, (int)m, (int)s, (int)l);
    }

    public static TimeSpan EquationOfTimeTotal(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.EquationOfTimeEccentricEffect() + dateTime.EquationOfTimeTiltEffect();
    }

    public static string FormatClientDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd");
    }

    public static string FormatClientTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "HH:mm");
    }

    #region Get

    public static TimeSpan GetAbsDuration(this TimeSpan from, TimeSpan to)
    {
        if (from == default)
            throw new ArgumentNullException($"from is '{default(TimeSpan)}'");
        else if (to == default)
            throw new ArgumentNullException($"to is '{default(TimeSpan)}'");
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(Math.Abs(to.Subtract(from).Ticks));
    }

    public static TimeSpan GetAbsDuration(this DateTime from, DateTime to)
    {
        if (from == default)
            throw new ArgumentNullException($"from is '{default(TimeSpan)}'");
        else if (to == default)
            throw new ArgumentNullException($"to is '{default(TimeSpan)}'");
        // ----------------------------------------------------------------------------------------------------
        return new TimeSpan(Math.Abs(to.Subtract(from).Ticks));
    }

    public static int GetDayOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDayOfMonth(dateTime);
    }

    public static int GetDaysInMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
        //return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDaysInMonth(dateTime.Year, dateTime.Month, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static int GetDaysInYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetDaysInYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static int GetEra(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime);
    }

    public static int GetHour(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetHour(dateTime);
    }

    public static int GetLeapMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetLeapMonth(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static double GetMilliseconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMilliseconds(dateTime);
    }

    public static DateTime GetMaximum(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException($"dateTimeFirst is '{default(DateTime)}'");
        else if (dateTimeSecond == default)
            throw new ArgumentNullException($"dateTimeSecond is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (dateTimeFirst >= dateTimeSecond)
            return dateTimeFirst;
        // ----------------------------------------------------------------------------------------------------
        return dateTimeSecond;
    }

    public static DateTime GetMinimum(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException($"dateTimeFirst is '{default(DateTime)}'");
        else if (dateTimeSecond == default)
            throw new ArgumentNullException($"dateTimeSecond is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (dateTimeFirst <= dateTimeSecond)
            return dateTimeFirst;
        // ----------------------------------------------------------------------------------------------------
        return dateTimeSecond;
    }

    public static int GetMinute(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMinute(dateTime);
    }

    public static int GetMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMonth(dateTime);
    }

    public static int GetMonthDiff(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException($"dateTimeFirst is '{default(DateTime)}'");
        else if (dateTimeSecond == default)
            throw new ArgumentNullException($"dateTimeSecond is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeLeft = (dateTimeFirst < dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        DateTime dateTimeRigth = (dateTimeFirst >= dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        // ----------------------------------------------------------------------------------------------------
        return (dateTimeLeft.Day == dateTimeRigth.Day ? default : dateTimeLeft.Day > dateTimeRigth.Day ? default : 1)
               + (dateTimeLeft.Month == dateTimeRigth.Month ? default : dateTimeRigth.Month - dateTimeLeft.Month)
               + (dateTimeLeft.Year == dateTimeRigth.Year ? default : (dateTimeRigth.Year - dateTimeLeft.Year) * 12);
    }

    public static int GetMonthsInYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetMonthsInYear(dateTime.Year, System.Globalization.CultureInfo.InvariantCulture.Calendar.GetEra(dateTime));
    }

    public static int GetSecond(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetSecond(dateTime);
    }

    public static double GetTotalMonthDiff(this DateTime dateTimeFirst, DateTime dateTimeSecond)
    {
        if (dateTimeFirst == default)
            throw new ArgumentNullException($"dateTimeFirst is '{default(DateTime)}'");
        else if (dateTimeSecond == default)
            throw new ArgumentNullException($"dateTimeSecond is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTimeLeft = (dateTimeFirst < dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        DateTime dateTimeRigth = (dateTimeFirst >= dateTimeSecond) ? dateTimeFirst : dateTimeSecond;
        int dateTimeLeftDfM = DateTime.DaysInMonth(dateTimeLeft.Year, dateTimeLeft.Month);
        int dateTimeRigthDfM = DateTime.DaysInMonth(dateTimeRigth.Year, dateTimeRigth.Month);
        // ----------------------------------------------------------------------------------------------------
        double dayFixOne = dateTimeLeft.Day == dateTimeRigth.Day
                           ? 0d
                           : dateTimeLeft.Day > dateTimeRigth.Day
                           ? dateTimeRigth.Day * 1d / dateTimeRigthDfM - dateTimeLeft.Day * 1d / dateTimeLeftDfM
                           : ((dateTimeRigth.Day - dateTimeLeft.Day) * 1d / dateTimeRigthDfM);
        // ----------------------------------------------------------------------------------------------------
        return dayFixOne
               + (dateTimeLeft.Month == dateTimeRigth.Month ? 0 : dateTimeRigth.Month - dateTimeLeft.Month)
               + (dateTimeLeft.Year == dateTimeRigth.Year ? 0 : (dateTimeRigth.Year - dateTimeLeft.Year) * 12);
    }

    public static TimeSpan GetUtcOffset(this DateTime dateTime, string dateTimeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneConverter.TZConvert.GetTimeZoneInfo(dateTimeZoneName)
               .GetUtcOffset(dateTime);
    }

    public static TimeSpan GetUtcOffset(this DateTime dateTime, AT.Infrastructure.SystemTimeZone dateTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffset(dateTime, dateTimeZone.ToString());
    }

    public static int GetUtcOffsetInteger(this DateTime dateTime, string dateTimeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffset(dateTime, dateTimeZoneName)
               .Hours;
    }

    public static int GetUtcOffsetInteger(this DateTime dateTime, AT.Infrastructure.SystemTimeZone dateTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return GetUtcOffsetInteger(dateTime, dateTimeZone.ToString());
    }

    public static int GetWeekOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(dateTime, System.Globalization.DateTimeFormatInfo.CurrentInfo.CalendarWeekRule, System.Globalization.DateTimeFormatInfo.CurrentInfo.FirstDayOfWeek);
    }

    public static int GetYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.Globalization.CultureInfo.InvariantCulture.Calendar.GetYear(dateTime);
    }

    #endregion

    public static double HourAngle(this DateTime dateTime, double Latitude, double GeometricZenith)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        double latRad = Latitude.AsDegreesToRadians();
        double sdRad = dateTime.SolarDeclination().AsDegreesToRadians();
        double someVal = GeometricZenith.AsDegreesToRadians();
        double HA = Math.Acos(Math.Cos(someVal) / (Math.Cos(latRad) * Math.Cos(sdRad)) - Math.Tan(latRad) * Math.Tan(sdRad));
        // ----------------------------------------------------------------------------------------------------
        return HA.AsRadiansToDegrees();
    }

    public static double HourAngleDawn(this DateTime dateTime, double Latitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        double _geometricZenith;
        switch (Kind)
        {
            case AT.Enums.TwilightKind.Nautical:
                {
                    _geometricZenith = 102;
                }
                break;

            case AT.Enums.TwilightKind.Astronomical:
                {
                    _geometricZenith = 108;
                }
                break;

            case AT.Enums.TwilightKind.Civil:
            default:
                {
                    _geometricZenith = 96;
                }
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime.HourAngle(Latitude, _geometricZenith);
    }

    public static double HourAngleDusk(this DateTime dateTime, double Latitude, AT.Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return -dateTime.HourAngleDawn(Latitude, Kind);
    }

    public static double HourAngleSunrise(this DateTime dateTime, double Latitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.HourAngle(Latitude, 90.833);
    }

    public static double HourAngleSunset(this DateTime dateTime, double Latitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return -dateTime.HourAngleSunrise(Latitude);
    }

    public static int LastLeapYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        dateTime = dateTime.AddYears(-1);

        if (dateTime.IsLeapYear())
            return dateTime.Year;

        dateTime = dateTime.AddYears(-(dateTime.Year % 4));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Year + (dateTime.IsLeapYear() ? 0 : -4);
    }

    public static int LengthOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    public static string LengthOfTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan lengthOfTime = DateTime.Now.Subtract(dateTime);

        if (lengthOfTime.Minutes == 0)
            return lengthOfTime.Seconds.ToString() + "s";
        else if (lengthOfTime.Hours == 0)
            return lengthOfTime.Minutes.ToString() + "m";
        else if (lengthOfTime.Days == 0)
            return lengthOfTime.Hours.ToString() + "h";
        // ----------------------------------------------------------------------------------------------------
        return lengthOfTime.Days.ToString() + "d";
    }

    public static string LocalTimeToServerTime(this String localTime, string serverTimeZoneName, string formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (string.IsNullOrEmpty(localTime))
            throw new ArgumentNullException($"localTime is '{default}'");
        else if (string.IsNullOrEmpty(serverTimeZoneName))
            throw new ArgumentNullException($"serverTimeZoneName is '{default}'");
        else if (string.IsNullOrEmpty(formatToReturn))
            throw new ArgumentNullException($"formatToReturn is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(localTime)
               .LocalTimeToServerTime(serverTimeZoneName)
               .ToString(formatToReturn);
    }

    public static string LocalTimeToServerTime(this String localTime, AT.Infrastructure.SystemTimeZone serverTimeZone, string formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (string.IsNullOrEmpty(localTime))
            throw new ArgumentNullException($"localTime is '{default}'");
        else if (serverTimeZone == default)
            throw new ArgumentNullException($"serverTimeZoneName is '{default(AT.Infrastructure.SystemTimeZone)}'");
        else if (string.IsNullOrEmpty(formatToReturn))
            throw new ArgumentNullException($"formatToReturn is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(localTime)
               .LocalTimeToServerTime(serverTimeZone)
               .ToString(formatToReturn);
    }

    public static int MaxWeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return MaxWeekNumber(dateTime.Year, weekRule, weekStart);
    }

    public static int MaxWeekNumber(int year, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (year == default)
            throw new ArgumentNullException($"year is '{default(int)}'");
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().GetWeekOfYear(new DateTime(year, 12, 31), weekRule, weekStart);
    }

    public static int NextLeapYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        dateTime = dateTime.AddYears(1);

        if (dateTime.IsLeapYear())
            return dateTime.Year;

        dateTime = dateTime.AddYears(4 - (dateTime.Year % 4));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Year + (dateTime.IsLeapYear() ? 0 : 4);
    }

    public static string? NullDateToString(this DateTime? dateTime, string format = "M/d/yyyy", string? nullResult = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (dateTime.HasValue)
            return dateTime.Value.ToString(format);
        // ----------------------------------------------------------------------------------------------------
        return nullResult;
    }

    public static int QuarterOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Month switch
        {
            1 or 2 or 3 => 1,
            4 or 5 or 6 => 2,
            7 or 8 or 9 => 3,
            _ => 4,
        };
    }

    public static DateTime ServerTimeToLocalTime(this DateTime dateTime, string timeZoneName)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        TimeZone serverTimeZone = TimeZone.CurrentTimeZone;
        DateTime dateTimeInUtc = serverTimeZone.ToUniversalTime(dateTime);
        // ----------------------------------------------------------------------------------------------------
        return dateTimeInUtc.Local(timeZoneName);
    }

    public static DateTime ServerTimeToLocalTime(this DateTime dateTime, AT.Infrastructure.SystemTimeZone localTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return ServerTimeToLocalTime(dateTime, localTimeZone.ToString());
    }

    public static string ServerTimeToLocalTime(this String serverTime, string timeZoneName, string formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (string.IsNullOrEmpty(serverTime))
            throw new ArgumentNullException($"serverTime is '{default}'");
        else if (string.IsNullOrEmpty(timeZoneName))
            throw new ArgumentNullException($"timeZoneName is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(serverTime)
               .ServerTimeToLocalTime(timeZoneName)
               .ToString(formatToReturn);
    }

    public static string ServerTimeToLocalTime(this String serverTime, AT.Infrastructure.SystemTimeZone localTimeZone, string formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (string.IsNullOrEmpty(serverTime))
            throw new ArgumentNullException($"serverTime is '{default}'");
        else if (localTimeZone == default)
            throw new ArgumentNullException($"localTimeZone is '{default(AT.Infrastructure.SystemTimeZone)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(serverTime)
               .ServerTimeToLocalTime(localTimeZone)
               .ToString(formatToReturn);
    }

    public static AT.Infrastructure.DateTimeRange Since(this DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime == default)
            throw new ArgumentNullException($"startDateTime is '{default(DateTime)}'");
        else if (endDateTime == default)
            throw new ArgumentNullException($"endDateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new AT.Infrastructure.DateTimeRange(startDateTime, endDateTime);
    }

    public static double SolarDeclination(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        double h = AverageAngle * (dateTime.DayOfYear - 2);
        double v = h + (E360OverPi * Math.Sin(h.AsDegreesToRadians()));
        // ----------------------------------------------------------------------------------------------------
        return Math.Asin((MinutesPerDegree / 10) * Math.Sin((v - VAtVernalEquinox).AsDegreesToRadians()))
               .AsRadiansToDegrees();
    }

    public static TimeSpan TimeElapsed(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now - dateTime;
    }

    public static TimeSpan? TimeOfDay(this TimeSpan? value)
    {
        TimeSpan? result = default;
        // ----------------------------------------------------------------------------------------------------
        if (value.HasValue)
            result = new TimeSpan(hours: value.Value.Hours,
                                  minutes: value.Value.Minutes,
                                  seconds: value.Value.Seconds);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static DateTime UnixTimeStampToDateTime(this double unixTimeStamp)
    {
        return new DateTime(_epochDateTime.AddSeconds(unixTimeStamp).ToLocalTime().Ticks);
    }

    public static DateTime UnixTimeStampToDateTimeUTC(this double unixTimeStamp)
    {
        return _epochDateTime.AddSeconds(unixTimeStamp);
    }

    public static AT.Infrastructure.DateTimeRange Until(this DateTime startDateTime, DateTime endDateTime)
    {
        if (startDateTime == default)
            throw new ArgumentNullException($"startDateTime is '{default(DateTime)}'");
        else if (endDateTime == default)
            throw new ArgumentNullException($"endDateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new AT.Infrastructure.DateTimeRange(startDateTime, endDateTime);
    }

    public static int WeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return new System.Globalization.GregorianCalendar().GetWeekOfYear(dateTime, weekRule, weekStart);
    }

    public static double DaysLeft(this DateTime source, DateTime target)
    {
        return (target.Date - source.Date).TotalDays;
    }

    public static string DayName(this DateTime current)
    {
        return current.DayOfWeek.ToString();
    }
}