namespace AT.Extensions.DateTimes.Georgian.Boundary;
public static class DateTimeExtensions
{
    #region Private: Method(s)

    private static DateTime WorkMethod(DateTime dateTime, Int64 returnType, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int64 interval1 = (Int64)timeInterval;
        Int64 ticksFromFloor = 0L;
        Int32 intervalFloor = 0;
        Int32 floorOffset = 0;
        Int32 intervalLength = 0;
        DateTime floorDate;
        DateTime ceilingDate;

        if (interval1 > 132L)
        {
            floorDate = new DateTime(dateTime.Ticks - (dateTime.Ticks % interval1), dateTime.Kind);
            if (returnType != 0L)
                ticksFromFloor = interval1 / returnType;
        }
        else if (interval1 < 8L)
        {
            intervalFloor = (Int32)(interval1) - 1;
            floorOffset = (Int32)dateTime.DayOfWeek * -1;
            floorDate = new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, 0, 0, 0, dateTime.Kind).AddDays(-(intervalFloor > floorOffset ? floorOffset + 7 - intervalFloor : floorOffset - intervalFloor));
            if (returnType != 0L)
                ticksFromFloor = TimeSpan.TicksPerDay * 7L / returnType;
        }
        else
        {
            intervalLength = interval1 >= 130L ? 12 : (Int32)(interval1 / 10L);
            intervalFloor = (Int32)(interval1 % intervalLength);
            floorOffset = (dateTime.Month - 1) % intervalLength;
            floorDate = new DateTime(dateTime.Year, dateTime.Month, 1, 0, 0, 0, dateTime.Kind).AddMonths(-(intervalFloor > floorOffset ? floorOffset + intervalLength - intervalFloor : floorOffset - intervalFloor));
            if (returnType != 0L)
            {
                ceilingDate = floorDate.AddMonths(intervalLength);
                ticksFromFloor = (Int64)ceilingDate.Subtract(floorDate).Ticks / returnType;
            }
        }
        // ----------------------------------------------------------------------------------------------------
        return floorDate.AddTicks(ticksFromFloor);
    }

    #endregion

    public static DateTime DateTimeCeiling(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 1L, timeInterval);
    }

    public static DateTime DateTimeCeilingUnbounded(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 1L, timeInterval).AddTicks(-1);
    }

    public static DateTime DateTimeFloor(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 0L, timeInterval);
    }

    public static DateTime DateTimeMidpoint(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return WorkMethod(dateTime, 2L, timeInterval);
    }

    public static DateTime DateTimeRound(this DateTime dateTime, AT.Enums.TimeInterval timeInterval)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        if (dateTime >= WorkMethod(dateTime, 2L, timeInterval))
            return WorkMethod(dateTime, 1L, timeInterval);
        else
            return WorkMethod(dateTime, 0L, timeInterval);
    }
}