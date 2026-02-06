using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.BuildXML;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class BuildXmlFromDictionaryExtensions
{
    private static XDocument TryBuildXmlFromDictionaryInternal(Object dictionary)
    {
        try
        {
            if (dictionary == null)
                throw new ArgumentNullException(nameof(dictionary), "Input dictionary cannot be null.");

            var xDoc = new XDocument();
            var root = new XElement("Root");
            xDoc.Add(root);

            var dictType = dictionary.GetType();
            if (!dictType.IsGenericType || !dictType.GetGenericTypeDefinition().FullName!.StartsWith("System.Collections.Generic.Dictionary"))
                throw new ArgumentException("Input must be a Dictionary<String, T> reference type.");

            dynamic dynDict = dictionary;
            foreach (var kvp in dynDict)
                ProcessElement(kvp.Key, kvp.Value, root);

            return xDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dictionary"))
        {
            throw new InvalidOperationException("Failed to build XML: The input dictionary was null. Ensure you provide a valid Dictionary.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dictionary"))
        {
            throw new InvalidOperationException("Failed to build XML: The input Object is not a valid Dictionary<String, T>. Check the type of the dictionary.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("key"))
        {
            throw new InvalidOperationException("Failed to build XML: Encountered a null key in the dictionary. All keys must be non-null strings.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Failed to build XML: An error occurred while creating or adding XML elements. Check nested collections and element names.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Failed to build XML: An element or value could not be converted to a valid XML format.", ex);
        }
        catch (OverflowException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("Failed to build XML: Numeric value is too large to be represented in XML element.", ex);
        }
        catch (InvalidCastException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections"))
        {
            throw new InvalidOperationException("Failed to build XML: A value in the dictionary could not be cast to the expected type for processing.", ex);
        }
        catch (NullReferenceException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections"))
        {
            throw new InvalidOperationException("Failed to build XML: Encountered a null reference while enumerating the dictionary or collection.", ex);
        }
        catch (IndexOutOfRangeException ex) when (ex.Source is not null && ex.Source.Equals("System.Collections"))
        {
            throw new InvalidOperationException("Failed to build XML: Attempted to access an element outside the bounds of a collection.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to build XML from dictionary: {ex}", ex);
        }
    }

    private static void ProcessElement(String key, Object value, XElement parent)
    {
        if (key == null)
            throw new ArgumentException("Dictionary key cannot be null.");

        if (value == null)
        {
            parent.Add(new XElement(key));
            return;
        }

        if (value is String s)
        {
            parent.Add(new XElement(key, s));
            return;
        }

        if (value is XElement xElement)
        {
            parent.Add(new XElement(key, xElement));
            return;
        }

        if (value is IEnumerable<String> stringEnumerable)
        {
            var listElement = new XElement(key);
            
            foreach (var item in stringEnumerable)
                listElement.Add(new XElement("Item", item));
            
            parent.Add(listElement);
            
            return;
        }

        if (value is IEnumerable<Object> objectEnumerable)
        {
            var objListElement = new XElement(key);

            foreach (var item in objectEnumerable)
                ProcessElement("Item", item, objListElement);
            
            parent.Add(objListElement);
            
            return;
        }

        if (value is Array array)
        {
            var arrayElement = new XElement(key);
            
            foreach (var item in array)
                ProcessElement("Item", item, arrayElement);
            
            parent.Add(arrayElement);
            
            return;
        }

        if (value is Dictionary<String, String> dictStr)
        {
            var nestedStr = new XElement(key);
            
            foreach (var kvp in dictStr)
                ProcessElement(kvp.Key, kvp.Value, nestedStr);
            
            parent.Add(nestedStr);
            
            return;
        }

        if (value is Dictionary<String, Object> dictObj)
        {
            var nestedObj = new XElement(key);
            
            foreach (var kvp in dictObj)
                ProcessElement(kvp.Key, kvp.Value, nestedObj);
            
            parent.Add(nestedObj);
            
            return;
        }

        if (value is IEnumerable<Dictionary<String, String>> listDictStr)
        {
            var listDictElement = new XElement(key);
            
            foreach (var dict in listDictStr)
                ProcessElement("Item", dict, listDictElement);
            
            parent.Add(listDictElement);
            
            return;
        }

        parent.Add(new XElement(key, value.ToString()));
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class BuildXmlFromDictionaryExtensions
{
    public static XDocument BuildXmlFromDictionary(this Dictionary<String, String> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Object> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, List<String>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, List<Object>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Dictionary<String, String>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Dictionary<String, Object>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, IEnumerable<String>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, IEnumerable<Object>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, XElement> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, List<XElement>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Object[]> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, String[]> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Dictionary<String, List<String>>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, Dictionary<String, Object[]>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }

    public static XDocument BuildXmlFromDictionary(this Dictionary<String, List<Dictionary<String, String>>> dictionary)
    {
        return TryBuildXmlFromDictionaryInternal(dictionary);
    }
}