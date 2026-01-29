namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static  class UnixTimeStampToExtensions
{
    #region Field(s)

    private static readonly DateTime _epochDateTime;

    #endregion

    #region Constructor

    static UnixTimeStampToExtensions()
    {
        _epochDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }

    #endregion

    public static DateTime UnixTimeStampToDateTime(this Double unixTimeStamp)
    {
        return new DateTime(_epochDateTime.AddSeconds(unixTimeStamp).ToLocalTime().Ticks);
    }

    public static DateTime UnixTimeStampToDateTimeUTC(this Double unixTimeStamp)
    {
        return _epochDateTime.AddSeconds(unixTimeStamp);
    }
}