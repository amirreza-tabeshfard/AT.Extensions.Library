using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Conversion;
using AT.Extensions.DateTimes.Georgian.Extraction;
using AT.Extensions.DateTimes.Georgian.Holiday;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Collections;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly Dictionary<int, IReadOnlyCollection<DateTime>>? _holidaysCache = default;

    private const String FromToDatesSeparator = ">";
    private const char NegativeBit = '0';
    private const char PositiveBit = '1';

    private static readonly String[] fromToDatesSeparators = new String[] { FromToDatesSeparator };

    #endregion

    #region Constructor

    static Extensions()
    {
        _holidaysCache = new();
    }

    #endregion

    #region Private: Method(s)

    private static IEnumerable<DateTime> SelectDates(this IEnumerable<String> sections)
    {
        foreach (String section in sections)
        {
            DateTime?[] currents = section.Split(separator: fromToDatesSeparators,
                                                        options: StringSplitOptions.RemoveEmptyEntries)
                                                 .Select(c => c.ToDateTime())
                                                 .Where(d => d.HasValue)
                                                 .ToArray();

            if (currents is not null)
                if (currents.Any())
                {
                    DateTime from = currents[0].Value;
                    DateTime to = currents.Last().Value;

                    if (to < from)
                        throw new FormatException(message: $"The dates order is wrong. The first date is later than the second date: {section}.");

                    for (var date = from; date <= to; date = date.AddDays(1))
                        yield return date.Date;
                }
                else
                    yield return currents.Single().Value.Date;
        }
    }

    private static IEnumerable<int> GetBits(this String bitMask, char positiveBit = PositiveBit)
    {
        if (bitMask?.Any() ?? false)
        {
            char[] bits = bitMask.ToCharArray();

            for (int i = 0; i < bits.Length; i++)
                if (bits[i] == positiveBit)
                    yield return i;
        }
    }

    #endregion

    public static IReadOnlyCollection<DateTime>? AllHolidays(this int year)
    {
        if (_holidaysCache is not null)
            if (!_holidaysCache.ContainsKey(year))
            {
                _holidaysCache[year] = new List<DateTime>()
                {
                    year.Easter(),
                    year.Ascension(),
                    year.Whit(),
                    year.NewYear(),
                    year.Labor(),
                    year.WorldWarTwo(),
                    year.Bastille(),
                    year.AssumptionOfMary(),
                    year.AllSaints(),
                    year.Armistice(),
                    year.Christmas(),
                };

                return _holidaysCache[year];
            }

        return default;
    }

    public static IEnumerable<DateTime> GenerateBusinessDaysList(this DateTime fisrtDateTime, DateTime lastDateTime, IEnumerable<DateTime> holidays, List<int> weekends)
    {
        if (fisrtDateTime == default)
            throw new ArgumentNullException(nameof(fisrtDateTime));
        else if (lastDateTime == default)
            throw new ArgumentNullException(nameof(lastDateTime));
        else if (fisrtDateTime > lastDateTime)
            throw new ArgumentException("Incorrect last date " + lastDateTime);
        // ----------------------------------------------------------------------------------------------------
        List<DateTime> result = GenerateDateList(fisrtDateTime, lastDateTime).ToList();
        // ----------------------------------------------------------------------------------------------------
        foreach (int d in weekends)
            for (int i = 0; i < result.Count; i++)
                if ((int)result[i].DayOfWeek == d)
                    result.RemoveAt(i);

        foreach (DateTime d in holidays)
            for (int i = 0; i < result.Count; i++)
                if (result[i] == d)
                    result.RemoveAt(i);
        // ----------------------------------------------------------------------------------------------------
        return result.OrderBy(d => d.Date);
    }

    public static IEnumerable<DateTime> GenerateDateList(this DateTime fisrtDateTime, DateTime lastDateTime)
    {
        if (fisrtDateTime == default)
            throw new ArgumentNullException(nameof(fisrtDateTime));
        else if (lastDateTime == default)
            throw new ArgumentNullException(nameof(lastDateTime));
        else if (fisrtDateTime > lastDateTime)
            throw new ArgumentException("Incorrect last date " + lastDateTime);
        // ----------------------------------------------------------------------------------------------------
        List<DateTime> result = new List<DateTime>();

        for (DateTime day = fisrtDateTime.Date; day.Date <= lastDateTime.Date; day = day.AddDays(1))
            result.Add(day);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static List<int>? GetCalendarChangedList(this DateTime dateTime, AT.Enums.CalendarFormat format)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        List<int>? result = default;
        System.Globalization.Calendar? calendar = default;
        switch (format)
        {
            case AT.Enums.CalendarFormat.ChineseLunisolar: calendar = new System.Globalization.ChineseLunisolarCalendar(); break;
            case AT.Enums.CalendarFormat.Gregorian: calendar = new System.Globalization.GregorianCalendar(); break;
            case AT.Enums.CalendarFormat.Hebrew: calendar = new System.Globalization.HebrewCalendar(); break;
            case AT.Enums.CalendarFormat.Hijri: calendar = new System.Globalization.HijriCalendar(); break;
            case AT.Enums.CalendarFormat.Japanese: calendar = new System.Globalization.JapaneseCalendar(); break;
            case AT.Enums.CalendarFormat.JapaneseLunisolar: calendar = new System.Globalization.JapaneseLunisolarCalendar(); break;
            case AT.Enums.CalendarFormat.Julian: calendar = new System.Globalization.JulianCalendar(); break;
            case AT.Enums.CalendarFormat.Korean: calendar = new System.Globalization.KoreanCalendar(); break;
            case AT.Enums.CalendarFormat.KoreanLunisolar: calendar = new System.Globalization.KoreanLunisolarCalendar(); break;
            case AT.Enums.CalendarFormat.Persian: calendar = new System.Globalization.PersianCalendar(); break;
            case AT.Enums.CalendarFormat.Taiwan: calendar = new System.Globalization.TaiwanCalendar(); break;
            case AT.Enums.CalendarFormat.TaiwanLunisolar: calendar = new System.Globalization.TaiwanLunisolarCalendar(); break;
            case AT.Enums.CalendarFormat.ThaiBuddhist: calendar = new System.Globalization.ThaiBuddhistCalendar(); break;
            case AT.Enums.CalendarFormat.UmAlQura: calendar = new System.Globalization.UmAlQuraCalendar(); break;
        }

        if (calendar is not null)
        {
            result = new();
            result.AddRange(new int[] {
                    calendar.GetYear(dateTime),
                    calendar.GetMonth(dateTime),
                    calendar.GetDayOfMonth(dateTime)
                 });
        }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static IEnumerable<DateTime> GetDateListInCurrentWeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime datePointer = dateTime.AddDays(-8);

        int datePointerWeekNo = datePointer.WeekNumber(weekRule, weekStart);
        int weekNo = dateTime.WeekNumber(weekRule, weekStart);

        while (datePointerWeekNo <= weekNo)
        {
            if (datePointerWeekNo.Equals(weekNo))
                yield return datePointer;

            datePointer.AddDays(1);
            datePointerWeekNo = datePointer.WeekNumber(weekRule, weekStart);
        }
    }

    public static IEnumerable<DateTime> GetDateRangeTo(this DateTime self, DateTime toDate)
    {
        if (self == default)
            throw new ArgumentNullException(nameof(self));
        else if (toDate == default)
            throw new ArgumentNullException(nameof(toDate));
        // ----------------------------------------------------------------------------------------------------
        IEnumerable<int> range = Enumerable.Range(start: 0, count: new TimeSpan(ticks: toDate.Ticks - self.Ticks).Days);

        foreach (int p in range)
            yield return self.Date.AddDays(p);
    }

    public static IEnumerable<DateTime> GetDates(this DateTime from, DateTime to, IEnumerable<DayOfWeek>? daysOfWeek = default)
    {
        if (from == default)
            throw new ArgumentNullException(nameof(from));
        else if (to == default)
            throw new ArgumentNullException(nameof(to));
        // ----------------------------------------------------------------------------------------------------
        DateTime start = from <= to ? from : to;
        DateTime end = to >= from ? to : from;

        for (DateTime date = start; date <= end; date = date.AddDays(1))
            if (!(daysOfWeek?.Any() ?? false) || daysOfWeek.Contains(date.DayOfWeek))
                yield return date;
    }

    public static IEnumerable<DateTime> GetDates(this String bitMask, DateTime startDate, DateTime? endDate = default, char positiveBit = PositiveBit)
    {
        if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        // ----------------------------------------------------------------------------------------------------
        int[]? bits = default;

        if (!bitMask.IsNullOrEmpty() || !bitMask.IsNullOrWhiteSpace())
            bits = bitMask?.GetBits(positiveBit: positiveBit)
                   .ToArray();

        if (bits?.Any() ?? false)
        {
            if (endDate == default && startDate != default)
                endDate = startDate.AddDays(bits.Last());

            do
            {
                foreach (int bit in bits)
                {
                    DateTime result = startDate.AddDays(bit);

                    if (result > endDate)
                        yield break;

                    yield return result.Date;
                }

                if (!bitMask.IsNullOrEmpty() || !bitMask.IsNullOrWhiteSpace())
                    startDate = startDate.AddDays(bitMask.Length);
            }
            while (startDate <= endDate);
        }
    }

    public static IEnumerable<DateTime> GetDates(this String dates, String separator = ",")
    {
        if (separator?.Contains(FromToDatesSeparator) ?? false)
            throw new ArgumentException(message: $"The argument splitter cannot be '{FromToDatesSeparator}' since it is used to split from and to values of periods.",
                                        paramName: nameof(separator));


        IEnumerable<DateTime>? result = default;

        if (!String.IsNullOrWhiteSpace(dates))
            if (!String.IsNullOrWhiteSpace(separator))
            {
                String[] sectionSeparators = new String[] { separator };

                String[] sections = dates.Split(separator: sectionSeparators,
                                                options: StringSplitOptions.RemoveEmptyEntries);

                result = sections.SelectDates().OrderBy(d => d).ToArray();
            }

        return result ?? Enumerable.Empty<DateTime>();
    }

    public static IEnumerable<DateTime> GetShifted(this IEnumerable<DateTime> dates, int shift)
    {
        if (dates is not null)
            if (dates?.Any() ?? false)
                foreach (DateTime date in dates)
                    yield return date.GetShifted(shift);
    }
}