using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class DiffExtensions
{
    public static Int64 DateDiff(this DateTime startDate, String datePart, DateTime endDate)
    {
        if (startDate == default)
            throw new ArgumentNullException(nameof(startDate));
        else if (datePart.IsNullOrEmpty() || datePart.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(datePart));
        else if (endDate == default)
            throw new ArgumentNullException(nameof(endDate));
        // ----------------------------------------------------------------------------------------------------
        Int64 DateDiffVal = 0;
        System.Globalization.Calendar calendar = Thread.CurrentThread.CurrentCulture.Calendar;
        TimeSpan timeSpan = new TimeSpan(endDate.Ticks - startDate.Ticks);
        // ----------------------------------------------------------------------------------------------------
        switch (datePart.ToLower().Trim())
        {
            #region year
            case "year":
            case "yy":
            case "yyyy":
                {
                    DateDiffVal = (Int64)(calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate));
                }
                break;
            #endregion

            #region quarter
            case "quarter":
            case "qq":
            case "q":
                {
                    DateDiffVal = (Int64)((((calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate)) * 4)
                                  + ((calendar.GetMonth(endDate) - 1) / 3))
                                  - ((calendar.GetMonth(startDate) - 1) / 3));
                }
                break;
            #endregion

            #region month
            case "month":
            case "mm":
            case "m":
                {
                    DateDiffVal = (Int64)(((calendar.GetYear(endDate)
                                  - calendar.GetYear(startDate)) * 12
                                  + calendar.GetMonth(endDate))
                                  - calendar.GetMonth(startDate));
                }
                break;
            #endregion

            #region day
            case "day":
            case "d":
            case "dd":
                {
                    DateDiffVal = (Int64)timeSpan.TotalDays;
                }
                break;
            #endregion

            #region week
            case "week":
            case "wk":
            case "ww":
                {
                    DateDiffVal = (Int64)(timeSpan.TotalDays / 7);
                }
                break;
            #endregion

            #region hour
            case "hour":
            case "hh":
                {
                    DateDiffVal = (Int64)timeSpan.TotalHours;
                }
                break;
            #endregion

            #region minute
            case "minute":
            case "mi":
            case "n":
                {
                    DateDiffVal = (Int64)timeSpan.TotalMinutes;
                }
                break;
            #endregion

            #region second
            case "second":
            case "ss":
            case "s":
                {
                    DateDiffVal = (Int64)timeSpan.TotalSeconds;
                }
                break;
            #endregion

            #region millisecond
            case "millisecond":
            case "ms":
                {
                    DateDiffVal = (Int64)timeSpan.TotalMilliseconds;
                }
                break;
            #endregion

            default:
                throw new Exception(String.Format("DatePart \"{0}\" is unknown", datePart));
        }
        // ----------------------------------------------------------------------------------------------------
        return DateDiffVal;
    }
}