namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class QuarterOfExtensions
{
    public static Int32 QuarterOfYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Month switch
        {
            1 or 2 or 3 => 1,
            4 or 5 or 6 => 2,
            7 or 8 or 9 => 3,
            _ => 4,
        };
    }
}