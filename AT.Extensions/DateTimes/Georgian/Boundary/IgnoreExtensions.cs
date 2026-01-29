namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class IgnoreExtensions
{
    public static DateTime IgnoreTimeSpan(this DateTime dateTime, TimeSpan timeSpan)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        if (timeSpan == TimeSpan.Zero)
            return dateTime;
        // ----------------------------------------------------------------------------------------------------
        return dateTime.AddTicks(-(dateTime.Ticks % timeSpan.Ticks));
    }

    public static DateTime IgnoreMilliseconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromMilliseconds(1000));
    }

    public static DateTime IgnoreSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromSeconds(60));
    }

    public static DateTime IgnoreMinutes(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromMinutes(60));
    }

    public static DateTime IgnoreHours(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.IgnoreTimeSpan(TimeSpan.FromHours(24));
    }
}