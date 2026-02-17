using Newtonsoft.Json;

namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections.Generic;
public static class JsonToObjectExtensions
{
    public static T JsonToObject<T>(this String json)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        // ----------------------------------------------------------------------------------------------------
        JsonSerializerSettings settings = new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore };
        // ----------------------------------------------------------------------------------------------------
        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}