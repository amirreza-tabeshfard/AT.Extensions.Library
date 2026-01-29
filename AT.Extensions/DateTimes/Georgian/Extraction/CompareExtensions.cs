namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class CompareExtensions
{
    public static Double CompareTo(this DateTime dateTime, DateTime value, Enums.DateTimeDifferenceFormat differenceFormat = Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (value == default)
            throw new ArgumentNullException(nameof(value));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan result = dateTime - value;
        return differenceFormat switch
        {
            Enums.DateTimeDifferenceFormat.Days => result.TotalDays,
            Enums.DateTimeDifferenceFormat.Hours => result.TotalHours,
            Enums.DateTimeDifferenceFormat.Milliseconds => result.TotalMilliseconds,
            Enums.DateTimeDifferenceFormat.Minutes => result.TotalMinutes,
            Enums.DateTimeDifferenceFormat.Months => result.TotalDays / 30,
            Enums.DateTimeDifferenceFormat.Seconds => result.TotalSeconds,
            Enums.DateTimeDifferenceFormat.Weeks => result.TotalDays / 7,
            Enums.DateTimeDifferenceFormat.Years => result.TotalDays / 365,
            _ => default,
        };
    }

    public static Int32 CompareWithoutMinutes(this DateTime dateTime, DateTime toDateTimeCompare)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (toDateTimeCompare == default)
            throw new ArgumentNullException(nameof(toDateTimeCompare));
        // ----------------------------------------------------------------------------------------------------
        dateTime = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, millisecond: 0);
        toDateTimeCompare = new DateTime(toDateTimeCompare.Year, toDateTimeCompare.Month, toDateTimeCompare.Day, toDateTimeCompare.Hour, 0, 0, millisecond: 0);
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(toDateTimeCompare);
    }
}