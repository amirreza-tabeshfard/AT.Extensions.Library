namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class LengthOfExtensions : Object
{
    public static Int32 LengthOfMonth(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.DaysInMonth(dateTime.Year, dateTime.Month);
    }

    public static String LengthOfTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
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
}