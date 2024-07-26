namespace AT.Extensions.DateTimes.Georgian.Collections;
public static class GenerateExtensions : Object
{
    public static IEnumerable<DateTime> GenerateBusinessDaysList(this DateTime fisrtDateTime, DateTime lastDateTime, IEnumerable<DateTime> holidays, List<Int32> weekends)
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
        foreach (Int32 d in weekends)
            for (Int32 i = 0; i < result.Count; i++)
                if ((Int32)result[i].DayOfWeek == d)
                    result.RemoveAt(i);

        foreach (DateTime d in holidays)
            for (Int32 i = 0; i < result.Count; i++)
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
        List<DateTime> result = new();

        for (DateTime day = fisrtDateTime.Date; day.Date <= lastDateTime.Date; day = day.AddDays(1))
            result.Add(day);
        // ----------------------------------------------------------------------------------------------------
        return result;
    }
}