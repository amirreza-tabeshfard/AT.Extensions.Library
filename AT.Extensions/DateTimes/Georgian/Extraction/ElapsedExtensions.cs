namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class ElapsedExtensions : Object
{
    public static TimeSpan Elapsed(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now.Subtract(dateTime);
    }

    public static Double ElapsedSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Now.Subtract(dateTime).TotalSeconds;
    }
}