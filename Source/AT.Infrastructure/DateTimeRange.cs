namespace AT.Infrastructure;
public struct DateTimeRange : IEquatable<DateTimeRange>
{
    #region Field(s)

    public readonly DateTime Start;

    public readonly DateTime End;

    public readonly Boolean IsMoment;

    #endregion

    #region Constructor

    public DateTimeRange(DateTime start, DateTime end)
    {
        if (start > end)
        {
            throw new ArgumentException("end time before start time");
        }

        this.Start = start;
        this.End = end;
        this.IsMoment = start == end;
    }

    #endregion

    #region Method(s): Private
            
    private static DateTime StartOfSecond(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, dateTime.Second, millisecond: 0);
    }

    private static DateTime StartOfMinute(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, millisecond: 0);
    }
    
    private static DateTime StartOfHour(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, 0, 0, millisecond: 0);
    }

    private static DateTime EndOfSecond(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return StartOfSecond(dateTime).AddSeconds(1).AddTicks(-1);
    }

    private static DateTime EndOfMinute(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return StartOfMinute(dateTime).AddMinutes(1).AddTicks(-1);
    }

    private static DateTime EndOfTheDay(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return StartOfHour(dateTime).AddDays(1).AddTicks(-1);
    }

    private static DateTime EndOfTheMonth(DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(dateTime.Year, dateTime.Month, 1)
               .AddMonths(1)
               .AddDays(-1);
    }

    #endregion

    #region Method(s)

    public static DateTimeRange FromSecond(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute, Int32 second)
    {
        var start = new DateTime(year, month, day, hour, minute, second);
        var range = new DateTimeRange(start, EndOfSecond(start));
        return range;
    }

    public static DateTimeRange FromMinute(Int32 year, Int32 month, Int32 day, Int32 hour, Int32 minute)
    {
        var start = new DateTime(year, month, day, hour, minute, 0);
        var range = new DateTimeRange(start, EndOfMinute(start));
        return range;
    }

    public static DateTimeRange FromHour(Int32 year, Int32 month, Int32 day, Int32 hour)
    {
        var start = new DateTime(year, month, day, hour, 0, 0);
        var range = new DateTimeRange(start, EndOfMinute(start));
        return range;
    }

    public static DateTimeRange FromDate(Int32 year, Int32 month, Int32 day)
    {
        var start = new DateTime(year, month, day);
        var range = new DateTimeRange(start, EndOfTheDay(start));
        return range;
    }

    public static DateTimeRange FromMonth(Int32 year, Int32 month)
    {
        var start = new DateTime(year, month, 1);
        var range = new DateTimeRange(
            start,
            EndOfTheMonth(start));
        return range;
    }

    public static DateTimeRange FromYear(Int32 year)
    {
        var range = new DateTimeRange(
            new DateTime(year, 1, 1),
            new DateTime(year + 1, 1, 1).AddTicks(-1));
        return range;
    }

    public Boolean Contains(DateTime moment)
    {
        return this.Start <= moment && this.End >= moment;
    }

    public Boolean Contains(DateTimeRange that)
    {
        return this.Start <= that.Start && this.End >= that.End;
    }

    public Boolean Intersects(DateTimeRange that)
    {
        return this.Start <= that.End && this.End >= that.Start;
    }

    public Boolean Equals(DateTimeRange other)
    {
        return this.Start == other.Start &&
            this.End == other.End;
    }

    #endregion

    #region Operator(s)

    public static Boolean operator ==(DateTimeRange r1, DateTimeRange r2)
    {
        return r1.Equals(r2);
    }

    public static Boolean operator !=(DateTimeRange r1, DateTimeRange r2)
    {
        return !r1.Equals(r2);
    }

    #endregion

    #region Override(s)

    public override Boolean Equals(object obj)
    {
        return obj is DateTimeRange range && this.Equals(range);
    }

    public override Int32 GetHashCode()
    {
        return (13 * this.Start.GetHashCode()) + this.End.GetHashCode();
    }

    public override String ToString()
    {
        return this.Start + " - " + this.End;
    }

    #endregion
}