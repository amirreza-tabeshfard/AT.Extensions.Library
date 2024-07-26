namespace AT.Extensions.DateTimes.Georgian.Holiday;
public static class Extensions : Object
{
    #region Private: Method(s)

    private static (Int32 month, Int32 day) ExecuteEasterAlgorithm(Int32 year)
    {
        Int32 g = (year / 100 - (year / 100 + 8) / 25 + 1) / 3;
        Int32 h = (19 * (year % 19) + year / 100 - year / 100 / 4 - g + 15) % 30;
        Int32 l = (32 + 2 * (year / 100 % 4) + 2 * (year % 100 / 4) - h - year % 100 % 4) % 7;
        Int32 m = (year % 19 + 11 * h + 22 - l) / 451;
        Int32 month = (h + l - 7 * m + 114) / 31;
        Int32 day = (h + l - 7 * m + 114) % 31 + 2;

        return (month, day);
    }

    #endregion

    public static DateTime AllSaints(this Int32 year)
    {
        return new(year, 11, 1);
    }

    public static DateTime Armistice(this Int32 year)
    {
        return new(year, 11, 11);
    }

    public static DateTime Ascension(this Int32 year)
    {
        return Easter(year).AddDays(38);
    }

    public static DateTime AssumptionOfMary(this Int32 year)
    {
        return new(year, 8, 15);
    }

    public static DateTime Bastille(this Int32 year)
    {
        return new(year, 7, 14);
    }

    public static DateTime Christmas(this Int32 year)
    {
        return new(year, 12, 25);
    }

    public static DateTime Easter(this Int32 year)
    {
        (Int32 month, Int32 day) = ExecuteEasterAlgorithm(year);

        return new DateTime(year, month, day);
    }

    public static DateTime Labor(this Int32 year)
    {
        return new(year, 5, 1);
    }

    public static DateTime NewYear(this Int32 year)
    {
        return new(year, 1, 1);
    }

    public static DateTime Whit(this Int32 year)
    {
        return Easter(year).AddDays(49);
    }

    public static DateTime WorldWarTwo(this Int32 year)
    {
        return new(year, 5, 8);
    }
}