namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
public static class CompareExtensions
{
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