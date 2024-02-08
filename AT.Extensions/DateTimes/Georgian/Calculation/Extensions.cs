﻿namespace AT.Extensions.DateTimes.Georgian.Calculation;
public static class Extensions : Object
{
    public static int Age(this DateTime dateOfBirth)
    {
        if (dateOfBirth == default)
            throw new ArgumentNullException($"dateOfBirth is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        if (((DateTime.Today.Month < dateOfBirth.Month) || (DateTime.Today.Month == dateOfBirth.Month)) && (DateTime.Today.Day < dateOfBirth.Day))
            return DateTime.Today.Year - dateOfBirth.Year - 1;
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Today.Year - dateOfBirth.Year;
    }

    public static int AgeMonths(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException($"referenceDate is '{default(DateTime)}'");
        else if (today == default)
            throw new ArgumentNullException($"today is '{default(DateTime)}'");
        else if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return (today.Year * 12 + today.Month) - (referenceDate.Year * 12 + referenceDate.Month);
    }

    public static int AgeYears(this DateTime referenceDate, DateTime today)
    {
        if (referenceDate == default)
            throw new ArgumentNullException($"referenceDate is '{default(DateTime)}'");
        else if (today == default)
            throw new ArgumentNullException($"today is '{default(DateTime)}'");
        else if (referenceDate > today)
            throw new ArgumentOutOfRangeException(nameof(referenceDate));
        // ----------------------------------------------------------------------------------------------------
        return today.Year - referenceDate.Year;
    }
}