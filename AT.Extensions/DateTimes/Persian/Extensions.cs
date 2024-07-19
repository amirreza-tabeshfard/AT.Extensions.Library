namespace AT.Extensions.DateTimes.Persian;
public static class Extensions : object
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

    public static String ToPersianDateYMD(this DateTime date)
    {
        System.Globalization.PersianCalendar persianCalendar = new System.Globalization.PersianCalendar();
        Int32 intYear = persianCalendar.GetYear(date);
        Int32 intMonth = persianCalendar.GetMonth(date);
        Int32 intDay = persianCalendar.GetDayOfMonth(date);
        return intYear.ToString() + "/" + intMonth.ToString() + "/" + intDay.ToString();
    }

    public static String ToPersianDateDMY(this DateTime date)
    {
        System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
        Int32 intYear = PC.GetYear(date);
        Int32 intMonth = PC.GetMonth(date);
        Int32 intDay = PC.GetDayOfMonth(date);
        return intDay.ToString() + "/" + intMonth.ToString() + "/" + intYear.ToString();
    }

    public static String ToPersianDateString(this DateTime date)
    {
        if (date != default)
        {
            System.Globalization.PersianCalendar PC = new System.Globalization.PersianCalendar();
            Int32 intYear = PC.GetYear(date);
            Int32 intMonth = PC.GetMonth(date);
            Int32 intDayOfMonth = PC.GetDayOfMonth(date);
            DayOfWeek enDayOfWeek = PC.GetDayOfWeek(date);
            String monthName, dayName;

            switch (intMonth)
            {
                case 1:
                    {
                        monthName = "فروردین";
                    }
                    break;

                case 2:
                    {
                        monthName = "اردیبهشت";
                    }
                    break;

                case 3:
                    {
                        monthName = "خرداد";
                    }
                    break;

                case 4:
                    {
                        monthName = "تیر";
                    }
                    break;

                case 5:
                    {
                        monthName = "مرداد";
                    }
                    break;

                case 6:
                    {
                        monthName = "شهریور";
                    }
                    break;

                case 7:
                    {
                        monthName = "مهر";
                    }
                    break;

                case 8:
                    {
                        monthName = "آبان";
                    }
                    break;

                case 9:
                    {
                        monthName = "آذر";
                    }
                    break;

                case 10:
                    {
                        monthName = "دی";
                    }
                    break;

                case 11:
                    {
                        monthName = "بهمن";
                    }
                    break;

                case 12:
                    {
                        monthName = "اسفند";
                    }
                    break;

                default:
                    {
                        monthName = default;
                    }
                    break;
            }

            switch (enDayOfWeek)
            {
                case DayOfWeek.Friday:
                    {
                        dayName = "جمعه";
                    }
                    break;

                case DayOfWeek.Monday:
                    {
                        dayName = "دوشنبه";
                    }
                    break;

                case DayOfWeek.Saturday:
                    {
                        dayName = "شنبه";
                    }
                    break;

                case DayOfWeek.Sunday:
                    {
                        dayName = "یکشنبه";
                    }
                    break;

                case DayOfWeek.Thursday:
                    {
                        dayName = "پنجشنبه";
                    }
                    break;

                case DayOfWeek.Tuesday:
                    {
                        dayName = "سه شنبه";
                    }
                    break;

                case DayOfWeek.Wednesday:
                    {
                        dayName = "چهارشنبه";
                    }
                    break;

                default:
                    {
                        dayName = default;
                    }
                    break;
            }

            return $"{dayName} {intDayOfMonth} {monthName} {intYear}";
        }

        return default;
    }
}