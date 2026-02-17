namespace AT.Extensions.I_Want_All_Subcategories_To_Be_Deleted.Strings.Collections;
public static class JsonToExtensions
{
    public static IDictionary<String, object> JsonToDictionary(this String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        // ----------------------------------------------------------------------------------------------------
        return (Dictionary<String, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(value, typeof(Dictionary<String, object>));
    }
}