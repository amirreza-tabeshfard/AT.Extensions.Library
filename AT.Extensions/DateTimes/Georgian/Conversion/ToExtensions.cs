using AT.Extensions.DateTimes.Georgian.Boundary;
using AT.Extensions.DateTimes.Georgian.Extraction;
using AT.Extensions.Strings.Comparison;

namespace AT.Extensions.DateTimes.Georgian.Conversion;
public static class ToExtensions
{
    #region Field(s)

    private static readonly System.Text.RegularExpressions.Regex? timespanRegex;

    public const Char NegativeBit = '0';
    public const Char PositiveBit = '1';

    #endregion

    #region Constructor

    static ToExtensions()
    {
        timespanRegex = new System.Text.RegularExpressions.Regex(@"((?<h>\d{2})(?<m>\d{2})(?<s>\d{2})?)|(((?<d1>\d{1,2})\.)?(?<h>\d{1,2})\:(?<m>\d{1,2})(\:(?<s>\d{1,2}))?(\[\+(?<d2>\d)\])?)");
    }

    #endregion

    #region Private: Method(s)

    private static TimeSpan? ParseTime(this String dateTime, System.Text.RegularExpressions.Regex regex)
    {
        if (dateTime.IsNullOrEmpty() || dateTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        TimeSpan? result = default;

        if (!String.IsNullOrWhiteSpace(dateTime))
            if (regex.Match(dateTime).Groups["h"].Success && regex.Match(dateTime).Groups["m"].Success)
            {
                Int32 days = 0;

                if (regex.Match(dateTime).Groups["d1"].Success)
                    days = Int32.Parse(s: regex.Match(dateTime).Groups["d1"].Value,
                                     provider: System.Globalization.CultureInfo.InvariantCulture);
                else if (regex.Match(dateTime).Groups["d2"].Success)
                    days = Int32.Parse(s: regex.Match(dateTime).Groups["d2"].Value,
                                     provider: System.Globalization.CultureInfo.InvariantCulture);

                Int32 hours = Int32.Parse(s: regex.Match(dateTime).Groups["h"].Value,
                                      provider: System.Globalization.CultureInfo.InvariantCulture);

                Int32 minutes = Int32.Parse(s: regex.Match(dateTime).Groups["m"].Value,
                                        provider: System.Globalization.CultureInfo.InvariantCulture);

                Int32 seconds = 0;

                if (regex.Match(dateTime).Groups["s"].Success)
                    seconds = Int32.Parse(s: regex.Match(dateTime).Groups["s"].Value,
                                        provider: System.Globalization.CultureInfo.InvariantCulture);

                result = new TimeSpan(days: days,
                                      hours: hours,
                                      minutes: minutes,
                                      seconds: seconds);
            }
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    private static String ToClientFormat(DateTime dateTime, String format)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (dateTime == DateTime.MinValue)
            return String.Empty;
        // ----------------------------------------------------------------------------------------------------
        TimeSpan offsetSpan = AT.Infrastructure.LocalTimeZoneConfig.TimeZone.GetUtcOffset(dateTime);
        DateTimeOffset offset = new DateTimeOffset(dateTime.Ticks, offsetSpan);
        // ----------------------------------------------------------------------------------------------------
        return offset.ToString(format);
    }

    #endregion

    public static String? ToBitmask(this IEnumerable<DateTime> dateTimes, DateTime begin, DateTime end, Boolean defaultOnEmpty = false, Char positiveBit = PositiveBit, Char negativeBit = NegativeBit)
    {
        if (dateTimes == default)
            throw new ArgumentNullException(nameof(dateTimes));
        else if (begin == default)
            throw new ArgumentNullException(nameof(begin));
        else if (end == default)
            throw new ArgumentNullException(nameof(end));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();

        if (dateTimes?.Any() ?? false)
            for (DateTime date = begin; date <= end; date = date.AddDays(1))
            {
                Char bit = dateTimes.Contains(date)
                           ? positiveBit
                           : negativeBit;

                result.Append(bit);
            }

        return defaultOnEmpty && result.Length == 0
               ? default
               : result.ToString();
    }

    public static String? ToBitmask(this IEnumerable<DateTime> dateTimes, Boolean defaultOnEmpty = false, Char positiveBit = PositiveBit, Char negativeBit = NegativeBit)
    {
        if (dateTimes == default)
            throw new ArgumentNullException(nameof(dateTimes));
        // ----------------------------------------------------------------------------------------------------
        String? result = default;

        if (dateTimes?.Any() ?? false)
            result = dateTimes?.ToBitmask(begin: dateTimes?.Min() ?? DateTime.MinValue,
                                      end: dateTimes?.Max() ?? DateTime.MinValue,
                                      defaultOnEmpty: defaultOnEmpty,
                                      positiveBit: positiveBit,
                                      negativeBit: negativeBit);

        if (result is null)
            result = defaultOnEmpty
                     ? default
                     : String.Empty;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String? ToBitmask(this IEnumerable<Int32> numbers, Int32 length, Boolean defaultOnEmpty = false, Char positiveBit = PositiveBit, Char negativeBit = NegativeBit)
    {
        if (numbers == default)
            throw new ArgumentNullException(nameof(numbers));
        // ----------------------------------------------------------------------------------------------------
        System.Text.StringBuilder result = new();

        if (numbers?.Any() ?? false)
            for (Int32 number = 0; number < length; number++)
            {
                Char bit = numbers.Contains(number)
                           ? positiveBit
                           : negativeBit;

                result.Append(bit);
            }
        // ----------------------------------------------------------------------------------------------------
        return defaultOnEmpty && result.Length == 0
               ? default
               : result.ToString();
    }

    public static String? ToBitmask(this IEnumerable<Int32> bits, Boolean defaultOnEmpty = false, Char positiveBit = PositiveBit, Char negativeBit = NegativeBit)
    {
        if (bits == default)
            throw new ArgumentNullException(nameof(bits));
        // ----------------------------------------------------------------------------------------------------
        String? result = default(String);

        if (bits?.Any() ?? false)
            result = bits.ToBitmask(length: bits.Max(),
                                    defaultOnEmpty: defaultOnEmpty,
                                    positiveBit: positiveBit,
                                    negativeBit: negativeBit);

        if (result is null)
            result = defaultOnEmpty
                     ? default
                     : String.Empty;
        // ----------------------------------------------------------------------------------------------------
        return result;
    }

    public static String ToClientDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd'T'00:00:00zzz");
    }

    public static String ToClientDateTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return ToClientFormat(dateTime, "yyyy-MM-dd'T'HH:mm:sszzz");
    }

    public static DateTime ToCreationDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return (dateTime == DateTime.MinValue)
               ? DateTime.UtcNow
               : dateTime;
    }

    public static String ToDateString(this DateTime dateTime, String format = "yyyy-MM-dd", System.Globalization.CultureInfo? provider = default)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.Date.ToString(format: format, provider: provider ?? System.Globalization.CultureInfo.InvariantCulture);
    }

    public static DateTime? ToDateTime(this object objDateTime)
    {
        if (objDateTime == default)
            throw new ArgumentNullException(nameof(objDateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime? dateTime = default;

        if (objDateTime is DateTime)
            dateTime = objDateTime as DateTime?;
        // ----------------------------------------------------------------------------------------------------
        return dateTime;
    }

    public static DateTime? ToDateTime(this String stringDateTime)
    {
        if (stringDateTime.IsNullOrEmpty() || stringDateTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(stringDateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime dateTime;
        Boolean isDateTime = DateTime.TryParse(stringDateTime, out dateTime);
        // ----------------------------------------------------------------------------------------------------
        return (isDateTime)
               ? dateTime
               : new DateTime();
    }

    public static DateTime ToDateTime(this TimeSpan time)
    {
        if (time == default)
            throw new ArgumentNullException(nameof(time));
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

    public static String? ToFriendlyDateString(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        String? formattedDate = default;

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
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.In(AT.Infrastructure.LocalTimeZoneConfig.TimeZone);
    }

    public static DateTime ToNewTimeZone(this DateTime dateTime, String fromTimeZone, String toTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (fromTimeZone.IsNullOrEmpty() || fromTimeZone.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(fromTimeZone));
        else if (toTimeZone.IsNullOrEmpty() || toTimeZone.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(toTimeZone));
        // ----------------------------------------------------------------------------------------------------
        DateTime universalTime = dateTime.ToUniversalTime(fromTimeZone, DateTimeKind.Unspecified);
        TimeZoneInfo destinationTimeZone = TimeZoneConverter.TZConvert.GetTimeZoneInfo(toTimeZone);
        // ----------------------------------------------------------------------------------------------------
        return TimeZoneInfo.ConvertTime(universalTime, destinationTimeZone);
    }

    public static DateTime ToNewTimeZone(this DateTime dateTime, AT.Infrastructure.SystemTimeZone fromTimeZone, AT.Infrastructure.SystemTimeZone toTimeZone)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        else if (fromTimeZone == default)
            throw new ArgumentNullException(nameof(fromTimeZone));
        else if (toTimeZone == default)
            throw new ArgumentNullException(nameof(toTimeZone));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToNewTimeZone(fromTimeZone.ToString(), toTimeZone.ToString());
    }

    public static String ToNewTimeZone(this String dateTime, String fromTimeZone, String toTimeZone, String formatToReturn = "M/dd/yyyy h:mm tt")
    {
        if (dateTime.IsNullOrEmpty() || dateTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(dateTime));
        else if (fromTimeZone.IsNullOrEmpty() || fromTimeZone.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(fromTimeZone));
        else if (toTimeZone.IsNullOrEmpty() || toTimeZone.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(toTimeZone));
        // ----------------------------------------------------------------------------------------------------
        return DateTime.Parse(dateTime)
               .ToNewTimeZone(fromTimeZone, toTimeZone)
               .ToString(formatToReturn);
    }

    public static String? ToOracleSqlDate(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return System.String.Format("to_date('{0}','dd.mm.yyyy hh24.mi.ss')", dateTime.ToString("dd.MM.yyyy HH:mm:ss"));
    }

    public static String? ToRFC822DateString(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Int32 offset = TimeZone.CurrentTimeZone.GetUtcOffset(DateTime.Now).Hours;
        String timeZone = "+" + offset.ToString().PadLeft(2, '0');
        if (offset < 0)
        {
            Int32 i = offset * -1;
            timeZone = "-" + i.ToString().PadLeft(2, '0');
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString("ddd, dd MMM yyyy HH:mm:ss " + timeZone.PadRight(5, '0'), System.Globalization.CultureInfo.GetCultureInfo("en-US"));
    }

    public static String ToString(this DateTime dateTime, String format)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        if (format.IsNullOrEmpty() || format.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(format));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.ToString(format);
    }

    public static TimeSpan? ToTimeSpan(this String dateTime, String? delimiters = default)
    {
        if (dateTime.IsNullOrEmpty() || dateTime.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------


        if (!String.IsNullOrWhiteSpace(dateTime))
        {
            if (Double.TryParse(
                s: dateTime,
                style: System.Globalization.NumberStyles.Any,
                provider: System.Globalization.CultureInfo.CurrentCulture,
                result: out Double current)
                && current.ToString(System.Globalization.CultureInfo.CurrentCulture) == dateTime.Trim()
                && (current < 1 || current % 1 != 0))
            {
                return DateTime.FromOADate(current) - DateTime.FromOADate(0);
            }

            if (Double.TryParse(
                s: dateTime,
                style: System.Globalization.NumberStyles.Any,
                provider: System.Globalization.CultureInfo.InvariantCulture,
                result: out Double invariant)
                && invariant.ToString(System.Globalization.CultureInfo.InvariantCulture) == dateTime.Trim()
                && (invariant < 1 || invariant % 1 != 0))
            {
                return DateTime.FromOADate(invariant) - DateTime.FromOADate(0);
            }

            if (!String.IsNullOrWhiteSpace(delimiters))
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

    public static TimeSpan? ToTimeSpanSmallDuration(this String? text)
    {
        if (text == default)
            return null;

        TimeSpan time;
        System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex("^(?<m>([0-9]+))(:|.|\')(?<s>([0-9]{1,2}))(\'\'|\"|:|.|,)(?<f>([0-9]+))$");
        if (regex.IsMatch(text))
        {
            text = regex.Replace(text, "0:0:${m}:${s}.${f}");
            var isValidTimeSpan = TimeSpan.TryParse(text, System.Globalization.CultureInfo.InvariantCulture, out time);
            if (isValidTimeSpan)
                return time;
        }

        return null;
    }

    public static String ToTimeString(this TimeSpan value, String format = @"hh\:mm\:ss")
    {
        if (value == default)
            throw new ArgumentNullException(nameof(value));
        if (format.IsNullOrEmpty() || format.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(format));
        // ----------------------------------------------------------------------------------------------------
        return (value.Ticks < 0 ? "-" : default) + value.ToString(format: format, formatProvider: System.Globalization.CultureInfo.InvariantCulture);
    }

    public static DateTime ToUniversalTime(this DateTime localTime, String localTimeZoneName, DateTimeKind? localTimeType = default)
    {
        if (localTime == default)
            throw new ArgumentNullException(nameof(localTime));
        else if (localTimeZoneName.IsNullOrEmpty() || localTimeZoneName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(localTimeZoneName));
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
            throw new ArgumentNullException(nameof(localTime));
        else if (localTimeZone == default)
            throw new ArgumentNullException(nameof(localTimeZone));
        // ----------------------------------------------------------------------------------------------------
        return ToUniversalTime(localTime, localTimeZone.ToString(), default);
    }

    public static DateTime ToUniversalTime(this DateTime localTime, String localTimeZoneName)
    {
        if (localTime == default)
            throw new ArgumentNullException(nameof(localTime));
        else if (localTimeZoneName.IsNullOrEmpty() || localTimeZoneName.IsNullOrWhiteSpace())
            throw new ArgumentNullException(nameof(localTimeZoneName));
        // ----------------------------------------------------------------------------------------------------
        return localTime.ToUniversalTime(localTimeZoneName, default);
    }

    public static Double ToUnixTime(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.CompareTo(new DateTime(1970, 1, 1), AT.Enums.DateTimeDifferenceFormat.Seconds);
    }

    public static Int64 ToUnixTimestamp(this DateTime dateTime)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        DateTime unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, millisecond: 0);
        TimeSpan unixTimeSpan = dateTime - unixEpoch;
        // ----------------------------------------------------------------------------------------------------
        return (Int64)unixTimeSpan.TotalSeconds;
    }
}