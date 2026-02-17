namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
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