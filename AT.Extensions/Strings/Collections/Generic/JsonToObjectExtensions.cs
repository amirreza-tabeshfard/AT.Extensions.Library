using Newtonsoft.Json;

namespace AT.Extensions.Strings.Collections.Generic;
public static class JsonToObjectExtensions : Object
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