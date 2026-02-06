namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class AfterExtensions
{
    public static DateTime DaysAfter(this DateTime source, Int32 days)
    {
        return source.AddDays(days);
    }

    public static DateTime WeeksAfter(this DateTime source, Int32 weeks)
    {
        int days = weeks * 7;
        return source.AddDays(days);
    }

    public static DateTime MonthsAfter(this DateTime source, Int32 months)
    {
        return source.AddMonths(months);
    }

    public static DateTime YearsAfter(this DateTime source, Int32 years)
    {
        return source.AddYears(years);
    }
}