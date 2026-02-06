using Newtonsoft.Json.Linq;
using System.Dynamic;
using System.Text.Json;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromJsonExtensions
{
    private static XDocument ConvertToXml(Object jsonSource)
    {
        try
        {
            if (jsonSource == null)
                throw new ArgumentNullException(nameof(jsonSource), "Input JSON source cannot be null.");

            if (jsonSource is String jsonString)
            {
                if (String.IsNullOrWhiteSpace(jsonString))
                    throw new ArgumentException("Input JSON String cannot be empty or whitespace.", nameof(jsonSource));

                try
                {
                    using JsonDocument doc = JsonDocument.Parse(jsonString);
                    return JsonElementToXDocument(doc.RootElement, "Root");
                }
                catch (JsonException ex)
                {
                    throw new InvalidOperationException("Failed to parse JSON String into JSON document.", ex);
                }
            }

            if (jsonSource is JObject jObject)
            {
                var doc = new XDocument();
                doc.Add(JObjectToXElement(jObject, "Root"));
                return doc;
            }

            if (jsonSource is JsonDocument jsonDoc)
                return JsonElementToXDocument(jsonDoc.RootElement, "Root");
            
            if (jsonSource is IDictionary<String, Object> dictObj)
                return DictionaryToXDocument(dictObj, "Root");
            
            if (jsonSource is IDictionary<String, String> dictStr)
                return DictionaryToXDocument(dictStr, "Root");

            if (jsonSource is IEnumerable<Object> listObj)
            {
                var root = new XElement("Root");
                foreach (var item in listObj)
                    root.Add(ConvertToXml(item).Root);
                return new XDocument(root);
            }

            if (jsonSource is ExpandoObject expando)
                return DictionaryToXDocument((IDictionary<String, Object>)expando, "Root");

            if (jsonSource is Tuple<String, Object> tuple)
            {
                var root = new XElement(tuple.Item1);
                root.Add(ConvertToXml(tuple.Item2).Root);
                return new XDocument(root);
            }

            if (jsonSource is KeyValuePair<String, Object> kvp)
            {
                var root = new XElement(kvp.Key);
                root.Add(ConvertToXml(kvp.Value).Root);
                return new XDocument(root);
            }

            return ObjectToXDocument(jsonSource);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonSource"))
        {
            throw new InvalidOperationException($"Conversion failed: ArgumentException detected. Parameter '{ex.ParamName}' is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonSource"))
        {
            throw new InvalidOperationException($"Conversion failed: ArgumentNullException detected. Parameter '{ex.ParamName}' cannot be null.", ex);
        }
        catch (JsonException ex) when (ex.Path is not null && ex.Path.Equals("$"))
        {
            throw new InvalidOperationException("Conversion failed: JsonException detected while parsing the JSON document at the root path.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Json"))
        {
            throw new InvalidOperationException("Conversion failed: InvalidOperationException detected in System.Text.Json processing.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("Newtonsoft.Json"))
        {
            throw new InvalidOperationException("Conversion failed: InvalidOperationException detected in Newtonsoft.Json processing.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Input String"))
        {
            throw new InvalidOperationException("Conversion failed: FormatException detected due to invalid String format.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("Value was either too large or too small"))
        {
            throw new InvalidOperationException("Conversion failed: OverflowException detected due to numeric value out of range.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("Serialization"))
        {
            throw new InvalidOperationException("Conversion failed: NotSupportedException detected during Object serialization.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Conversion failed: Unexpected exception of type {jsonSource?.GetType().FullName ?? "Unknown"} occurred.", ex);
        }
    }

    private static XDocument JsonElementToXDocument(JsonElement element, String name)
    {
        var root = new XElement(name);
        switch (element.ValueKind)
        {
            case JsonValueKind.Object:
                {
                    foreach (var prop in element.EnumerateObject())
                        root.Add(JsonElementToXDocument(prop.Value, prop.Name).Root);
                }
                break;

            case JsonValueKind.Array:
                {
                    foreach (var item in element.EnumerateArray())
                        root.Add(JsonElementToXDocument(item, "Item").Root);
                }
                break;

            case JsonValueKind.String:
                {
                    root.Value = element.GetString();
                }
                break;

            case JsonValueKind.Number:
                {
                    root.Value = element.GetRawText();
                }
                break;

            case JsonValueKind.True:
            case JsonValueKind.False:
                {
                    root.Value = element.GetBoolean().ToString();
                }
                break;

            case JsonValueKind.Null:
                {
                    root.Value = String.Empty;
                }
                break;

            default:
                {
                    root.Value = element.GetRawText();
                }
                break;
        }

        return new XDocument(root);
    }

    private static XElement JObjectToXElement(JObject obj, String name)
    {
        var element = new XElement(name);
        foreach (var prop in obj.Properties())
        {
            if (prop.Value is JObject nestedObj)
                element.Add(JObjectToXElement(nestedObj, prop.Name));
            else if (prop.Value is JArray jArray)
            {
                foreach (var item in jArray)
                    element.Add(JObjectToXElement(item as JObject ?? new JObject(), "Item"));
            }
            else
                element.Add(new XElement(prop.Name, prop.Value.ToString()));
        }
        return element;
    }

    private static XDocument DictionaryToXDocument(IDictionary<String, Object> dict, String rootName)
    {
        var root = new XElement(rootName);
        foreach (var kvp in dict)
        {
            if (kvp.Value != null)
                root.Add(ConvertToXml(kvp.Value).Root.Name == "Root" ? ConvertToXml(kvp.Value).Root.Elements() : new XElement(kvp.Key, kvp.Value));
            else
                root.Add(new XElement(kvp.Key, String.Empty));
        }
        return new XDocument(root);
    }

    private static XDocument DictionaryToXDocument(IDictionary<String, String> dict, String rootName)
    {
        var root = new XElement(rootName);
        foreach (var kvp in dict)
            root.Add(new XElement(kvp.Key, kvp.Value));
        return new XDocument(root);
    }

    private static XDocument ObjectToXDocument(Object obj)
    {
        var json = JsonSerializer.Serialize(obj);
        using var doc = JsonDocument.Parse(json);
        return JsonElementToXDocument(doc.RootElement, "Root");
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromJsonExtensions
{
    public static XDocument BuildXmlFromJson(this String json)
    {
        return ConvertToXml(json);
    }

    public static XDocument BuildXmlFromJson(this JObject jsonObject)
    {
        return ConvertToXml(jsonObject);
    }

    public static XDocument BuildXmlFromJson(this JsonDocument jsonDoc)
    {
        return ConvertToXml(jsonDoc);
    }

    public static XDocument BuildXmlFromJson(this Dictionary<String, Object> dict)
    {
        return ConvertToXml(dict);
    }

    public static XDocument BuildXmlFromJson(this ExpandoObject expando)
    {
        return ConvertToXml(expando);
    }

    public static XDocument BuildXmlFromJson(this List<Dictionary<String, Object>> list)
    {
        return ConvertToXml(list);
    }

    public static XDocument BuildXmlFromJson(this List<JObject> list)
    {
        return ConvertToXml(list);
    }

    public static XDocument BuildXmlFromJson(this Object obj)
    {
        return ConvertToXml(obj);
    }

    public static XDocument BuildXmlFromJson(this List<Object> list)
    {
        return ConvertToXml(list);
    }

    public static XDocument BuildXmlFromJson(this Dictionary<String, String> dict)
    {
        return ConvertToXml(dict);
    }

    public static XDocument BuildXmlFromJson(this Dictionary<String, List<Object>> dict)
    {
        return ConvertToXml(dict);
    }

    public static XDocument BuildXmlFromJson(this Tuple<String, Object> tuple)
    {
        return ConvertToXml(tuple);
    }

    public static XDocument BuildXmlFromJson(this KeyValuePair<String, Object> kvp)
    {
        return ConvertToXml(kvp);
    }

    public static XDocument BuildXmlFromJson(this Object[] array)
    {
        return ConvertToXml(array);
    }

    public static XDocument BuildXmlFromJson(this List<ExpandoObject> list)
    {
        return ConvertToXml(list);
    }
}