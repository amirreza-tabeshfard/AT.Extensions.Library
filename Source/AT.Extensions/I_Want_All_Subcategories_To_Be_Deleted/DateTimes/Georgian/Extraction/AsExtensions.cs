namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.DateTimes.Georgian.Extraction;
public static class AsExtensions
{
    public static Double AsDegreesToRadians(this Double value)
    {
        return (Math.PI / 180) * value;
    }

    public static Double AsRadiansToDegrees(this Double value)
    {
        return (180 / Math.PI) * value;
    }
}