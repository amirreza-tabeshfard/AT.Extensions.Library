using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Extraction;
using System.Text.RegularExpressions;

namespace AT.Extensions.DateTimes.Georgian.Conversion;
public static class Extensions : Object
{
    #region Field(s)

    private static readonly System.Text.RegularExpressions.Regex? timespanRegex;

    public const char NegativeBit = '0';
    public const char PositiveBit = '1';

    #endregion

    #region Constructor

    static Extensions()
    {
        timespanRegex = new System.Text.RegularExpressions.Regex(@"((?<h>\d{2})(?<m>\d{2})(?<s>\d{2})?)|(((?<d1>\d{1,2})\.)?(?<h>\d{1,2})\:(?<m>\d{1,2})(\:(?<s>\d{1,2}))?(\[\+(?<d2>\d)\])?)");
    }

    #endregion

    #region Private: Method(s)

    private static TimeSpan? ParseTime(this String dateTime, System.Text.RegularExpressions.Regex regex)
    {
        if (string.IsNullOrEmpty(dateTime))
            throw new ArgumentNullException($"dateTime is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        TimeSpan? result = default;

        if (!string.IsNullOrWhiteSpace(dateTime))
            if (regex.Match(dateTime).Groups["h"].Success && regex.Match(dateTime).Groups["m"].Success)
            {
                int days = 0;

                if (regex.Match(dateTime).Groups["d1"].Success)
                    days = int.Parse(s: regex.Match(dateTime).Groups["d1"].Value,
                                     provider: System.Globalization.CultureInfo.InvariantCulture);
                else if (regex.Match(dateTime).Groups["d2"].Success)
                    days = int.Parse(s: regex.Match(dateTime).Groups["d2"].Value,
                                     provider: System.Globalization.CultureInfo.InvariantCulture);

                int hours = int.Parse(s: regex.Match(dateTime).Groups["h"].Value,
                                      provider: System.Globalization.CultureInfo.InvariantCulture);

                int minutes = int.Parse(s: regex.Match(dateTime).Groups["m"].Value,
                                        provider: System.Globalization.CultureInfo.InvariantCulture);

                int seconds = 0;

                if (regex.Match(dateTime).Groups["s"].Success)
                    seconds = int.Parse(s: regex.Match(dateTime).Groups["s"].Value,
                                        provider: System.Globalization.CultureInfo.InvariantCulture);

                result = new TimeSpan(days: days,
                                      hours: hours,
                                      minutes: minutes,
                                      seconds: seconds);
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    private static string ToClientFormat(DateTime dateTime, string format)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (dateTime == DateTime.MinValue)
            return string.Empty;
        // ----------------------------------------------------------------------------------------------------
        TimeSpan offsetSpan = AT.Infrastructure.LocalTimeZoneConfig.TimeZone.GetUtcOffset(dateTime);
        DateTimeOffset offset = new DateTimeOffset(dateTime.Ticks, offsetSpan);
        // ----------------------------------------------------------------------------------------------------
        return offset.ToString(format);
    }

    #endregion

    public static string ConvertTo24HourFormatWithSeconds(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
    }

    public static string ConvertToFormatDateOnly(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("yyy-MM-dd");
    }

    public static string? ToBitmask(this IEnumerable<DateTime> dateTimes, DateTime begin, DateTime end, bool defaultOnEmpty = false, char positiveBit = PositiveBit, char negativeBit = NegativeBit)
    {
        if (dateTimes == default)
            throw new ArgumentNullException($"dateTimes is '{default(IEnumerable<DateTime>)}'");
        else if (begin == default)
            throw new ArgumentNullException($"begin is '{default(DateTime)}'");
        else if (end == default)
            throw new ArgumentNullException($"end is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();

        if (dateTimes?.Any() ?? false)
            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                char bit = dateTimes.Contains(date)
                           ? positiveBit
                           : negativeBit;

                result.Append(bit);
            }

        return defaultOnEmpty && result.Length == 0
               ? default
               : result.ToString();
    }

    public static string? ToBitmask(this IEnumerable<DateTime> dateTimes, bool defaultOnEmpty = false, char positiveBit = PositiveBit, char negativeBit = NegativeBit)
    {
        if (dateTimes == default)
            throw new ArgumentNullException($"dateTimes is '{default(IEnumerable<DateTime>)}'");
        // ----------------------------------------------------------------------------------------------------
        string? result = default;

        if (dateTimes?.Any() ?? false)
            result = dateTimes?.ToBitmask(begin: dateTimes?.Min() ?? DateTime.MinValue,
                                      end: dateTimes?.Max() ?? DateTime.MinValue,
                                      defaultOnEmpty: defaultOnEmpty,
                                      positiveBit: positiveBit,
                                      negativeBit: negativeBit);

        if (result is null)
            result = defaultOnEmpty
                     ? default
                     : string.Empty;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static string? ToBitmask(this IEnumerable<int> numbers, int length, bool defaultOnEmpty = false, char positiveBit = PositiveBit, char negativeBit = NegativeBit)
    {
        if (numbers == default)
            throw new ArgumentNullException($"numbers is '{default(IEnumerable<int>)}'");
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();

        if (numbers?.Any() ?? false)
            for (int number = 0; number < length; number++)
            {
                char bit = numbers.Contains(number)
                           ? positiveBit
                           : negativeBit;

                result.Append(bit);
            }
        // ----------------------------------------------------------------------------------------------------
        return defaultOnEmpty && result.Length == 0
               ? default
               : result.ToString();
    }

    public static string? ToBitmask(this IEnumerable<int> bits, bool defaultOnEmpty = false, char positiveBit = PositiveBit, char negativeBit = NegativeBit)
    {
        if (bits == default)
            throw new ArgumentNullException($"bits is '{default(IEnumerable<int>)}'");
        // ----------------------------------------------------------------------------------------------------
        string? result = default(string);

        if (bits?.Any() ?? false)
            result = bits.ToBitmask(length: bits.Max(),
                                    defaultOnEmpty: defaultOnEmpty,
                                    positiveBit: positiveBit,
                                    negativeBit: negativeBit);

        if (result is null)
            result = defaultOnEmpty
                     ? default
                     : string.Empty;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static string ToClientDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd'T'00:00:00zzz");
    }

    public static string ToClientDateTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd'T'HH:mm:sszzz");
    }

    public static DateTime ToCreationDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return (dateTime == DateTime.MinValue)
               ? DateTime.UtcNow
               : dateTime;
    }

    public static string ToDateString(this DateTime dateTime, string format = "yyyy-MM-dd", System.Globalization.CultureInfo? provider = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.ToString(format: format, provider: provider ?? System.Globalization.CultureInfo.InvariantCulture);
    }

    public static DateTime? ToDateTime(this object objDateTime)
    {
        if (objDateTime == default)
            throw new ArgumentNullException($"objDateTime is '{default(object)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime? dateTime = default;

        if (objDateTime is DateTime)
            dateTime = objDateTime as DateTime?;
        // ----------------------------------------------------------------------------------------------------
        return dateTime;
    }

    public static DateTime? ToDateTime(this String stringDateTime)
    {
        if (string.IsNullOrEmpty(stringDateTime))
            throw new ArgumentNullException($"stringDateTime is '{default(string)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTime;
        bool isDateTime = DateTime.TryParse(stringDateTime, out dateTime);
        // ----------------------------------------------------------------------------------------------------
        return (isDateTime)
               ? dateTime
               : new DateTime();
    }

    public static DateTime ToDateTime(this TimeSpan time)
    {
        if (time == default)
            throw new ArgumentNullException($"time is '{default(TimeSpan)}'");
        // ----------------------------------------------------------------------------------------------------
        return new DateTime(time.Ticks);
    }

    public static DateTime ToDateTime(this TimeSpan? time)
    {
        DateTime result = default;
        // ----------------------------------------------------------------------------------------------------
        if (time.HasValue)
            result = time.Value.ToDateTime();
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static string? ToFriendlyDateString(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        string? formattedDate = default;

        if (dateTime.Date == DateTime.Today)
            formattedDate = "Today";
        else if (dateTime.Date == DateTime.Today.AddDays(-1))
            formattedDate = "Yesterday";
        else if (dateTime.Date > DateTime.Today.AddDays(-6))
            formattedDate = dateTime.ToString("dddd").ToString();
        else
            formattedDate = dateTime.ToString("MMMM dd, yyyy");

        formattedDate += " @ " + dateTime.ToString("t").ToLower();
        // ----------------------------------------------------------------------------------------------------
        return formattedDate;
    }

    public static DateTime ToLocal(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.In(AT.Infrastructure.LocalTimeZoneConfig.TimeZone);
    }

    public static DateTime ToNewTimeZone(this DateTime dateTime, string fromTimeZone, string toTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (string.IsNullOrEmpty(fromTimeZone))
            throw new ArgumentNullException($"fromTimeZone is '{default}'");
        else if (string.IsNullOrEmpty(toTimeZone))
            throw new ArgumentNullException($"toTimeZone is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime universalTime = dateTime.ToUniversalTime(fromTimeZone, DateTimeKind.Unspecified);
        TimeZoneInfo destinationTimeZone = TimeZoneConverter.TZConvert.GetTimeZoneInfo(toTimeZone);
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTime(universalTime, destinationTimeZone);
    }

    public static DateTime ToNewTimeZone(this DateTime dateTime, AT.Infrastructure.SystemTimeZone fromTimeZone, AT.Infrastructure.SystemTimeZone toTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        else if (fromTimeZone == default)
            throw new ArgumentNullException($"fromTimeZone is '{default(AT.Infrastructure.SystemTimeZone)}'");
        else if (toTimeZone == default)
            throw new ArgumentNullException($"toTimeZone is '{default(AT.Infrastructure.SystemTimeZone)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToNewTimeZone(fromTimeZone.ToString(), toTimeZone.ToString());
    }

    public static string ToNewTimeZone(this String dateTime, string fromTimeZone, string toTimeZone, string formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (string.IsNullOrEmpty(dateTime))
            throw new ArgumentNullException($"dateTime is '{default}'");
        else if (string.IsNullOrEmpty(fromTimeZone))
            throw new ArgumentNullException($"fromTimeZone is '{default}'");
        else if (string.IsNullOrEmpty(toTimeZone))
            throw new ArgumentNullException($"toTimeZone is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(dateTime)
               .ToNewTimeZone(fromTimeZone, toTimeZone)
               .ToString(formatToReturn);
    }

    public static string? ToOracleSqlDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return System.String.Format("to_date('{0}','dd.mm.yyyy hh24.mi.ss')", dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
    }

    [Obsolete]
    public static string? ToRFC822DateString(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        int offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
        string timeZone = "+" + offset.ToString().PadLeft(2, '0');
        if (offset < 0)
        {
            int i = offset * -1;
            timeZone = "-" + i.ToString().PadLeft(2, '0');
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'), System.Globalization.CultureInfo.GetCultureInfo("en-US"));
    }

    public static string ToString(this DateTime dateTime, string format)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        if (string.IsNullOrEmpty(format))
            throw new ArgumentNullException($"format is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString(format);
    }

    public static TimeSpan? ToTimeSpan(this String dateTime, string? delimiters = default)
    {
        if (string.IsNullOrEmpty(dateTime))
            throw new ArgumentNullException($"dateTime is '{default}'");
        // ----------------------------------------------------------------------------------------------------


        if (!string.IsNullOrWhiteSpace(dateTime))
        {
            if (double.TryParse(
                s: dateTime,
                style: System.Globalization.NumberStyles.Any,
                provider: System.Globalization.CultureInfo.CurrentCulture,
                result: out double current)
                && current.ToString(System.Globalization.CultureInfo.CurrentCulture) == dateTime.Trim()
                && (current < 1 || current % 1 != 0))
            {
                return DateTime.FromOADate(current) - DateTime.FromOADate(0);
            }

            if (double.TryParse(
                s: dateTime,
                style: System.Globalization.NumberStyles.Any,
                provider: System.Globalization.CultureInfo.InvariantCulture,
                result: out double invariant)
                && invariant.ToString(System.Globalization.CultureInfo.InvariantCulture) == dateTime.Trim()
                && (invariant < 1 || invariant % 1 != 0))
            {
                return DateTime.FromOADate(invariant) - DateTime.FromOADate(0);
            }

            if (!string.IsNullOrWhiteSpace(delimiters))
            {
                var delimitersEscaped = System.Text.RegularExpressions.Regex.Escape(
                    str: delimiters);

                var delimitersPattern =
                    $@"(?<h>\d{{1,2}})[{delimitersEscaped}](?<m>\d{{1,2}})([{delimitersEscaped}](?<s>\d{{1,2}}))?";
                var delimitersRegex = new System.Text.RegularExpressions.Regex(
                    pattern: delimitersPattern);

                return dateTime.ParseTime(regex: delimitersRegex);
            }

            return dateTime.ParseTime(regex: timespanRegex);
        }

        return default;
    }

    public static TimeSpan? ToTimeSpanSmallDuration(this string? text)
    {
        if (text == null)
            return null;

        TimeSpan time;
        Regex regex = new Regex("^(?<m>([0-9]+))(:|.|\')(?<s>([0-9]{1,2}))(\'\'|\"|:|.|,)(?<f>([0-9]+))$");
        if (regex.IsMatch(text))
        {
            text = regex.Replace(text, "0:0:${m}:${s}.${f}");
            var isValidTimeSpan = TimeSpan.TryParse(text, System.Globalization.CultureInfo.InvariantCulture, out time);
            if (isValidTimeSpan)
                return time;
        }

        return null;
    }

    public static string ToTimeString(this TimeSpan value, string format = @"hh\:mm\:ss")
    {
        if (value == default)
            throw new ArgumentNullException($"dateTime is '{default(System.TimeSpan)}'");
        if (string.IsNullOrEmpty(format))
            throw new ArgumentNullException($"format is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return (value.Ticks < 0 ? "-" : default) + value.ToString(format: format, formatProvider: System.Globalization.CultureInfo.InvariantCulture);
    }

    public static DateTime ToUniversalTime(this DateTime localTime, string localTimeZoneName, DateTimeKind? localTimeType = default)
    {
        if (localTime == default)
            throw new ArgumentNullException($"localTime is '{default(DateTime)}'");
        else if (string.IsNullOrEmpty(localTimeZoneName))
            throw new ArgumentNullException($"localTimeZoneName is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        TimeZoneInfo timeZoneInfo = TimeZoneConverter.TZConvert.GetTimeZoneInfo(localTimeZoneName);

        if (localTimeType is not null)
            localTime = DateTime.SpecifyKind(localTime, DateTimeKind.Unspecified);
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTimeToUtc(localTime, timeZoneInfo);
    }

    public static DateTime ToUniversalTime(this DateTime localTime, AT.Infrastructure.SystemTimeZone localTimeZone)
    {
        if (localTime == default)
            throw new ArgumentNullException($"localTime is '{default(DateTime)}'");
        else if (localTimeZone == default)
            throw new ArgumentNullException($"localTimeZone is '{default(AT.Infrastructure.SystemTimeZone)}'");
        // ----------------------------------------------------------------------------------------------------
        return ToUniversalTime(localTime, localTimeZone.ToString(), default);
    }

    public static DateTime ToUniversalTime(this DateTime localTime, string localTimeZoneName)
    {
        if (localTime == default)
            throw new ArgumentNullException($"localTime is '{default(DateTime)}'");
        else if (string.IsNullOrEmpty(localTimeZoneName))
            throw new ArgumentNullException($"localTimeZoneName is '{default}'");
        // ----------------------------------------------------------------------------------------------------
        return localTime.ToUniversalTime(localTimeZoneName, default);
    }

    public static double ToUnixTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(new DateTime(1970, 1, 1), AT.Enums.DateTimeDifferenceFormat.Seconds);
    }

    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException($"dateTime is '{default(DateTime)}'");
        // ----------------------------------------------------------------------------------------------------
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, millisecond: 0);
        TimeSpan unixTimeSpan = dateTime - unixEpoch;
        // ----------------------------------------------------------------------------------------------------
        return (long)unixTimeSpan.TotalSeconds;
    }
}