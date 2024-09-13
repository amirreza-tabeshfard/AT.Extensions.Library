namespace AT.Extensions.Strings.Collections.Generic;
public static class DeserializeExtensions : Object
{
    public static T Deserialize<T>(this String jsonString)
    {
        ArgumentException.ThrowIfNullOrEmpty(jsonString);
        // ----------------------------------------------------------------------------------------------------
        return System.Text.Json.JsonSerializer.Deserialize<T>(jsonString);
    }
}