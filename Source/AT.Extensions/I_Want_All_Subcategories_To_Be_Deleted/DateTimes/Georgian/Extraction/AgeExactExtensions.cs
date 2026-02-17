namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
public static class AgeExactExtensions
{
    public static decimal AgeExactYears(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException(nameof(referenceDate));
        else if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return Math.Round(((decimal)(today.Year * 12 + today.Month) - (referenceDate.Year * 12 + referenceDate.Month)) / 12, 2);
    }
}