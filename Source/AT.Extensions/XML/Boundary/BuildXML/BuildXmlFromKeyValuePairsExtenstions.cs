using System.Collections;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromKeyValuePairsExtenstions
{
    private static XElement ConvertToXml(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            var root = new XElement("Root");

            switch (source)
            {
                case IDictionary<String, String> dictString:
                    {
                        foreach (var kv in dictString)
                            root.Add(new XElement(kv.Key, kv.Value ?? String.Empty));
                    }
                    break;

                case IDictionary<String, Object> dictObject:
                    {
                        foreach (var kv in dictObject)
                            root.Add(new XElement(kv.Key, kv.Value?.ToString() ?? String.Empty));
                    }
                    break;

                case IDictionary idict:
                    {
                        foreach (DictionaryEntry kv in idict)
                            root.Add(new XElement(kv.Key.ToString(), kv.Value?.ToString() ?? String.Empty));
                    }
                    break;

                case IEnumerable<KeyValuePair<String, String>> kvpStringList:
                    {
                        foreach (var kv in kvpStringList)
                            root.Add(new XElement(kv.Key, kv.Value ?? String.Empty));
                    }
                    break;

                case IEnumerable<KeyValuePair<String, Object>> kvpObjectList:
                    {
                        foreach (var kv in kvpObjectList)
                            root.Add(new XElement(kv.Key, kv.Value?.ToString() ?? String.Empty));
                    }
                    break;

                case IEnumerable list:
                    {
                        foreach (var item in list)
                        {
                            if (item is KeyValuePair<String, String> kvs)
                                root.Add(new XElement(kvs.Key, kvs.Value ?? String.Empty));
                            else if (item is KeyValuePair<String, Object> kvo)
                                root.Add(new XElement(kvo.Key, kvo.Value?.ToString() ?? String.Empty));
                            else
                                throw new InvalidOperationException($"Unsupported element type: {item.GetType().FullName}");
                        }
                    }
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported source type: {source.GetType().FullName}");
            }

            return root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException($"Conversion failed: Input source cannot be null. Parameter: {ex.ParamName}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections.IDictionary"))
        {
            throw new InvalidOperationException($"Conversion failed: Unsupported element found in non-generic IDictionary.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections.Generic.Dictionary`2"))
        {
            throw new InvalidOperationException($"Conversion failed: Unsupported Dictionary type encountered during XML conversion.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections.Generic.KeyValuePair`2[]"))
        {
            throw new InvalidOperationException($"Conversion failed: Unsupported array of KeyValuePair encountered.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections.IEnumerable"))
        {
            throw new InvalidOperationException($"Conversion failed: Unsupported IEnumerable element type found.", ex);
        }
        catch (InvalidOperationException ex) when (!string.IsNullOrWhiteSpace(ex.Message))
        {
            throw new InvalidOperationException($"Conversion failed: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Conversion failed due to an unexpected error: {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromKeyValuePairsExtenstions
{
    public static XElement BuildXmlFromKeyValuePairs(this Dictionary<String, String> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this Dictionary<String, Object> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this Hashtable source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IDictionary source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this List<KeyValuePair<String, String>> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this List<KeyValuePair<String, Object>> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IEnumerable<KeyValuePair<String, String>> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IEnumerable<KeyValuePair<String, Object>> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IReadOnlyDictionary<String, String> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IReadOnlyDictionary<String, Object> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IDictionary<String, String> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this IDictionary<String, Object> source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this KeyValuePair<String, String>[] source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this KeyValuePair<String, Object>[] source)
    {
        return ConvertToXml(source);
    }

    public static XElement BuildXmlFromKeyValuePairs(this Object source)
    {
        return ConvertToXml(source);
    }
}