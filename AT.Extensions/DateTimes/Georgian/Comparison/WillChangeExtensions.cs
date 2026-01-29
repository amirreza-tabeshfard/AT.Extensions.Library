using AT.Extensions.DateTimes.Georgian.Addition;

namespace AT.Extensions.DateTimes.Georgian.Comparison;
public static class WillChangeExtensions
{
    public static Boolean WillChangeDate(this DateTime dateTime, Double value, Enums.DateTimeDifferenceFormat differenceFormat = Enums.DateTimeDifferenceFormat.Hours)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date != dateTime.Add(value, differenceFormat).Date;
    }

    public static Boolean WillChangeMonth(this DateTime dateTime, Double value, Enums.DateTimeDifferenceFormat differenceFormat = Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Month != dateTime.Add(value, differenceFormat).Month;
    }

    public static Boolean WillChangeYear(this DateTime dateTime, Double value, Enums.DateTimeDifferenceFormat differenceFormat = Enums.DateTimeDifferenceFormat.Days)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Year != dateTime.Add(value, differenceFormat).Year;
    }
}