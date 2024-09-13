namespace AT.Extensions.Strings.Collections.Generic;
public static class ToJsonExtensions : Object
{
    public static String ToJson<T>(this T value, Newtonsoft.Json.JsonSerializerSettings? settings = default)
    {
        if (settings == default)
            settings = new Newtonsoft.Json.JsonSerializerSettings
            {
                ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver(),
                Converters = new List<Newtonsoft.Json.JsonConverter> 
                {
                    new Newtonsoft.Json.Converters.StringEnumConverter() 
                }
            };
        // ----------------------------------------------------------------------------------------------------
        return Newtonsoft.Json.JsonConvert.SerializeObject(value, settings);
    }
}