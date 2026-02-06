namespace AT.Extensions.DateTimes.Persian;
public static  class GetExtensions
{
    public static String GetPersianAge(this DateTime birthDate, out Int32 day, out Int32 month, out Int32 year, out Int32 week)
    {
        String result = String.Empty;
        TimeSpan x = DateTime.Now - birthDate;
        DateTime dateTime = DateTime.MinValue + x;

        year = dateTime.Year - 1;
        month = dateTime.Month - 1;
        day = dateTime.Day - 1;
        week = day / 7;
        day = day % 7;

        if (year != 0)
            result += $"{year} سال ";

        if (month != 0)
            result += $"{month} ماه ";

        if (week != 0)
            result += $"{week} هفته ";

        if (day != 0)
            result += $"{day} روز ";

        return result;
    }

    public static String GetPersianAge(this DateTime birthDate)
    {
        String result = String.Empty;
        TimeSpan x = DateTime.Now - birthDate;
        DateTime dateTime = DateTime.MinValue + x;

        Int32 year = dateTime.Year - 1;
        Int32 month = dateTime.Month - 1;
        Int32 day = dateTime.Day - 1;
        Int32 week = day / 7;

        day = day % 7;

        Int32 hour = dateTime.Hour;
        Int32 minute = dateTime.Minute;
        Int32 second = dateTime.Second;

        if (year != 0)
            result += $"{year} سال ";

        if (month != 0)
            result += $"{month} ماه ";

        if (week != 0)
            result += $"{week} هفته ";

        if (day != 0)
            result += $"{day} روز ";

        if (hour != 0)
            result += $"{hour} ساعت ";

        if (minute != 0)
            result += $"{minute} دقیقه ";

        if (second != 0)
            result += $"{second} ثانیه ";

        return result;
    }
}