namespace AT.Extensions.DateTimes.Georgian.Holiday;
public static class Extensions : Object
{
    #region Private: Method(s)

    private static (int month, int day) ExecuteEasterAlgorithm(int year)
    {
        int g = (year / 100 - (year / 100 + 8) / 25 + 1) / 3;
        int h = (19 * (year % 19) + year / 100 - year / 100 / 4 - g + 15) % 30;
        int l = (32 + 2 * (year / 100 % 4) + 2 * (year % 100 / 4) - h - year % 100 % 4) % 7;
        int m = (year % 19 + 11 * h + 22 - l) / 451;
        int month = (h + l - 7 * m + 114) / 31;
        int day = (h + l - 7 * m + 114) % 31 + 2;

        return (month, day);
    }

    #endregion

    public static DateTime Easter(this int year)
    {
        (int month, int day) = ExecuteEasterAlgorithm(year);

        return new DateTime(year, month, day);
    }

    public static DateTime Ascension(this int year)
    {
        return Easter(year).AddDays(38);
    }

    public static DateTime Whit(this int year)
    {
        return Easter(year).AddDays(49);
    }

    public static DateTime NewYear(this int year)
    {
        return new(year, 1, 1);
    }

    public static DateTime Labor(this int year)
    {
        return new(year, 5, 1);
    }

    public static DateTime WorldWarTwo(this int year)
    {
        return new(year, 5, 8);
    }

    public static DateTime Bastille(this int year)
    {
        return new(year, 7, 14);
    }

    public static DateTime AssumptionOfMary(this int year)
    {
        return new(year, 8, 15);
    }

    public static DateTime AllSaints(this int year)
    {
        return new(year, 11, 1);
    }

    public static DateTime Armistice(this int year)
    {
        return new(year, 11, 11);
    }

    public static DateTime Christmas(this int year)
    {
        return new(year, 12, 25);
    }
}