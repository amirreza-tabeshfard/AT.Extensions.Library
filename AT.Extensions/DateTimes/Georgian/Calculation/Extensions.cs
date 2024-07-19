namespace AT.Extensions.DateTimes.Georgian.Calculation;
public static class Extensions : Object
{
    public static Int32 Age(this DateTime dateOfBirth)
    {
        if (dateOfBirth == default)
            throw new ArgumentNullException(nameof(dateOfBirth));
        // ----------------------------------------------------------------------------------------------------
        if (((DateTime.Today.Month < dateOfBirth.Month) || (DateTime.Today.Month == dateOfBirth.Month)) && (DateTime.Today.Day < dateOfBirth.Day))
            return DateTime.Today.Year - dateOfBirth.Year - 1;
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Today.Year - dateOfBirth.Year;
    }

    public static Int32 AgeMonths(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException(nameof(referenceDate));
        else if (today == default)
            throw new ArgumentNullException(nameof(today));
        else if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return (today.Year * 12 + today.Month) - (referenceDate.Year * 12 + referenceDate.Month);
    }

    public static Int32 AgeYears(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException(nameof(referenceDate));
        else if (today == default)
            throw new ArgumentNullException(nameof(today));
        else if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return today.Year - referenceDate.Year;
    }
}