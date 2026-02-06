using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromSocialMediaJsonExtensions
{
    private static XDocument ProcessJson(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException("Input cannot be null.");

            var doc = new XDocument();
            var root = new XElement("SocialMediaData");
            doc.Add(root);

            if (input is String jsonString)
            {
                if (String.IsNullOrWhiteSpace(jsonString))
                    throw new ArgumentException("JSON String is empty.");
            
                root.Add(new XElement("JsonString", jsonString));
            }
            else if (input is Dictionary<String, Object> dict)
            {
                foreach (var kvp in dict)
                    root.Add(new XElement(kvp.Key, kvp.Value?.ToString() ?? String.Empty));
            }
            else if (input is List<Dictionary<String, Object>> list)
            {
                foreach (var item in list)
                {
                    var itemElement = new XElement("Item");
             
                    foreach (var kvp in item)
                        itemElement.Add(new XElement(kvp.Key, kvp.Value?.ToString() ?? String.Empty));
                    
                    root.Add(itemElement);
                }
            }
            else if (input is Newtonsoft.Json.Linq.JObject jObject)
            {
                foreach (var prop in jObject.Properties())
                    root.Add(new XElement(prop.Name, prop.Value.ToString()));
            }
            else if (input is Newtonsoft.Json.Linq.JArray jArray)
            {
                foreach (var item in jArray)
                    root.Add(new XElement("Item", item.ToString()));
            }
            else
                root.Add(new XElement("ObjectData", input.ToString()));

            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("jsonString"))
        {
            throw new InvalidOperationException("Failed to build XML: The provided JSON String is empty or invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Failed to build XML: Input cannot be null.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Failed to build XML: Invalid cast occurred while converting Object to XML element.", ex);
        }
        catch (Newtonsoft.Json.JsonReaderException ex) when (ex.Source is not null && ex.Source.Equals("Newtonsoft.Json"))
        {
            throw new InvalidOperationException("Failed to build XML: JSON parsing failed. Ensure the JSON structure is correct.", ex);
        }
        catch (System.IO.IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Failed to build XML: IO error occurred while reading input.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Failed to build XML: Input format is not supported.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Failed to build XML: Numeric value in JSON is too large or too small.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from Social Media JSON. Details: " + ex.Message, ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromSocialMediaJsonExtensions
{
    public static XDocument BuildXmlFromSocialMediaJson(this String jsonContent)
    {
        return ProcessJson(jsonContent);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this Dictionary<String, Object> jsonDict)
    {
        return ProcessJson(jsonDict);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this List<Dictionary<String, Object>> jsonList)
    {
        return ProcessJson(jsonList);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this Object socialMediaObject)
    {
        return ProcessJson(socialMediaObject);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this Uri jsonUri)
    {
        return ProcessJson(jsonUri);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.IO.Stream jsonStream)
    {
        return ProcessJson(jsonStream);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.IO.TextReader jsonReader)
    {
        return ProcessJson(jsonReader);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.Text.StringBuilder jsonBuilder)
    {
        return ProcessJson(jsonBuilder);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.Net.Http.HttpContent httpContent)
    {
        return ProcessJson(httpContent);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.IO.FileInfo jsonFile)
    {
        return ProcessJson(jsonFile);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this IEnumerable<Object> jsonEnumerable)
    {
        return ProcessJson(jsonEnumerable);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this Newtonsoft.Json.Linq.JObject jObject)
    {
        return ProcessJson(jObject);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this Newtonsoft.Json.Linq.JArray jArray)
    {
        return ProcessJson(jArray);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.Dynamic.ExpandoObject expando)
    {
        return ProcessJson(expando);
    }

    public static XDocument BuildXmlFromSocialMediaJson(this System.Collections.Hashtable hashtable)
    {
        return ProcessJson(hashtable);
    }
}