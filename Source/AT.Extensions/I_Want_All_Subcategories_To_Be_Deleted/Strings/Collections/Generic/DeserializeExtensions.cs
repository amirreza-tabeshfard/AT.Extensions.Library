namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections.Generic;
public static class DeserializeExtensions
{
    public static T Deserialize<T>(this String jsonString)
    {
        ArgumentException.ThrowIfNullOrEmpty(jsonString);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
    }
}