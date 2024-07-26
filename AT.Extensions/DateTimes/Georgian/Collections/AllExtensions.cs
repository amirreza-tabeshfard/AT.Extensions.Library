﻿using AT.Extensions.DateTimes.Georgian.Holiday;

namespace AT.Extensions.DateTimes.Georgian.Collections;
public static class AllExtensions : Object
{
    #region Field(s)

    private static readonly Dictionary<Int32, IReadOnlyCollection<DateTime>>? _holidaysCache = default;

    #endregion

    #region Constructor

    static AllExtensions()
    {
        _holidaysCache = new();
    }

    #endregion

    public static IReadOnlyCollection<DateTime>? AllHolidays(this Int32 year)
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
}