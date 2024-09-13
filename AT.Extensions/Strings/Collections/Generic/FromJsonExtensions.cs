﻿namespace AT.Extensions.Strings.Collections.Generic;
public static class FromJsonExtensions : Object
{
    public static T FromJson<T>(this String json, params Newtonsoft.Json.JsonConverter[] converters)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        // ----------------------------------------------------------------------------------------------------
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, converters);
    }

    public static T FromJson<T>(this String json, Newtonsoft.Json.JsonSerializerSettings settings)
    {
        ArgumentException.ThrowIfNullOrEmpty(json);
        // ----------------------------------------------------------------------------------------------------
        return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json, settings);
    }
}