namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class AgoExtensions
{
    public static DateTime DaysAgo(this DateTime source, Int32 days)
    {
        return source.AddDays(-days);
    }

    public static DateTime WeeksAgo(this DateTime source, Int32 weeks)
    {
        var days = weeks * 7;
        return source.AddDays(-days);
    }

    public static DateTime MonthsAgo(this DateTime source, Int32 months)
    {
        return source.AddMonths(-months);
    }

    public static DateTime YearsAgo(this DateTime source, Int32 years)
    {
        return source.AddYears(-years);
    }
}