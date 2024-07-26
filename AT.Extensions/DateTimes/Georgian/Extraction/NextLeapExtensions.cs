using AT.Extensions.DateTimes.Georgian.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class NextLeapExtensions : Object
{
    public static Int32 NextLeapYear(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        dateTime = dateTime.AddYears(1);

        if (dateTime.IsLeapYear())
            return dateTime.Year;

        dateTime = dateTime.AddYears(4 - (dateTime.Year % 4));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Year + (dateTime.IsLeapYear() ? 0 : 4);
    }
}