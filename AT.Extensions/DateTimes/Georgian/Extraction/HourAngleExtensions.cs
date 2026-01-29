namespace AT.Extensions.DateTimes.Georgian.Extraction;
public static class HourAngleExtensions
{
    public static Double HourAngle(this DateTime dateTime, Double Latitude, Double GeometricZenith)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Double latRad = Latitude.AsDegreesToRadians();
        Double sdRad = dateTime.SolarDeclination().AsDegreesToRadians();
        Double someVal = GeometricZenith.AsDegreesToRadians();
        Double HA = Math.Acos(Math.Cos(someVal) / (Math.Cos(latRad) * Math.Cos(sdRad)) - Math.Tan(latRad) * Math.Tan(sdRad));
        // ----------------------------------------------------------------------------------------------------
        return HA.AsRadiansToDegrees();
    }

    public static Double HourAngleDawn(this DateTime dateTime, Double Latitude, Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        Double _geometricZenith;
        switch (Kind)
        {
            case Enums.TwilightKind.Nautical:
                {
                    _geometricZenith = 102;
                }
                break;

            case Enums.TwilightKind.Astronomical:
                {
                    _geometricZenith = 108;
                }
                break;

            case Enums.TwilightKind.Civil:
            default:
                {
                    _geometricZenith = 96;
                }
                break;
        }
        // ----------------------------------------------------------------------------------------------------
        return dateTime.HourAngle(Latitude, _geometricZenith);
    }

    public static Double HourAngleDusk(this DateTime dateTime, Double Latitude, Enums.TwilightKind Kind)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return -dateTime.HourAngleDawn(Latitude, Kind);
    }

    public static Double HourAngleSunrise(this DateTime dateTime, Double Latitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return dateTime.HourAngle(Latitude, 90.833);
    }

    public static Double HourAngleSunset(this DateTime dateTime, Double Latitude)
    {
        if (dateTime == default)
            throw new ArgumentNullException(nameof(dateTime));
        // ----------------------------------------------------------------------------------------------------
        return -dateTime.HourAngleSunrise(Latitude);
    }
}