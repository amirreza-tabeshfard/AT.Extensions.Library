namespace AT.Extensions.Strings.Collections;
public static class JsonToExtensions : Object
{
    public static IDictionary<String, object> JsonToDictionary(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return (Dictionary<String, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(value, typeof(Dictionary<String, object>));
    }
}