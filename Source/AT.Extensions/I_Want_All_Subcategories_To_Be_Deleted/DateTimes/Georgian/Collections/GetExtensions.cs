using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Boundary;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Collections;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Conversion;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
using AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Comparison;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Collections;
public static class GetExtensions
{
    #region Field(s)

    private const String FromToDatesSeparator = ">";
    private const Char NegativeBit = '0';
    private const Char PositiveBit = '1';

    private static readonly String[] fromToDatesSeparators = new String[] { FromToDatesSeparator };

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

    private static IEnumerable<Int32> GetBits(this String bitMask, Char positiveBit = PositiveBit)
    {
        if (bitMask?.Any() ?? false)
        {
            Char[] bits = bitMask.ToCharArray();

            for (Int32 i = 0; i < bits.Length; i++)
                if (bits[i] == positiveBit)
                    yield return i;
        }
    }

    #endregion

    public static IEnumerable<DateTime> GetDateListInCurrentWeekNumber(this DateTime dateTime, System.Globalization.CalendarWeekRule weekRule = System.Globalization.CalendarWeekRule.FirstDay, DayOfWeek weekStart = DayOfWeek.Monday)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime datePointer = dateTime.AddDays(-8);

        Int32 datePointerWeekNo = datePointer.WeekNumber(weekRule, weekStart);
        Int32 weekNo = dateTime.WeekNumber(weekRule, weekStart);

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
        IEnumerable<Int32> range = Enumerable.Range(start: 0, count: new TimeSpan(ticks: toDate.Ticks - self.Ticks).Days);

        foreach (Int32 p in range)
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

    public static IEnumerable<DateTime> GetDates(this String bitMask, DateTime startDate, DateTime? endDate = default, Char positiveBit = PositiveBit)
    {
        if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        // ----------------------------------------------------------------------------------------------------
        Int32[]? bits = default;

        if (!bitMask.IsNullOrEmpty() || !bitMask.IsNullOrWhiteSpace())
            bits = bitMask?.GetBits(positiveBit: positiveBit)
                   .ToArray();

        if (bits?.Any() ?? false)
        {
            if (endDate == default && startDate != default)
                endDate = startDate.AddDays(bits.Last());

            do
            {
                foreach (Int32 bit in bits)
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

    public static IEnumerable<DateTime> GetShifted(this IEnumerable<DateTime> dates, Int32 shift)
    {
        if (dates is not null)
            if (dates?.Any() ?? false)
                foreach (DateTime date in dates)
                    yield return date.GetShifted(shift);
    }
}