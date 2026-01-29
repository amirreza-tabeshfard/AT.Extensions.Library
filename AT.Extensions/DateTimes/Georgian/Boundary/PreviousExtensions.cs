namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class PreviousExtensions
{
    public static DateTime PreviousDay(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-1);
    }

    public static DateTime PreviousMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddMonths(-1);
    }

    public static DateTime PreviousWeek(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddDays(-7);
    }

    public static DateTime PreviousYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddYears(-1);
    }
}