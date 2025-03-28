namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementFromObjectExtensions
    : Object
{
    public static System.Xml.XmlElement BuildXmlElementFromObject(this String xmlString)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlString);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlString);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            using StringReader reader = new(xmlString);
            using System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(reader, new System.Xml.XmlReaderSettings
            {
                DtdProcessing = System.Xml.DtdProcessing.Prohibit
            });

            xmlDoc.Load(xmlReader);
            return xmlDoc.DocumentElement ?? throw new ArgumentException("XML does not contain a root element.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString"))
        {
            throw new ArgumentException("Input XML string cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString") && string.IsNullOrWhiteSpace(ex.Message))
        {
            throw new ArgumentException("Input XML string cannot be null, empty, or whitespace.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new ArgumentException("Invalid XML format. The XML string is not well-formed.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the XML data. Please check file access or network status.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xmlString"))
        {
            throw new ArgumentException("Input XML string cannot be null.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML. Please verify the input and try again.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred. Please review the stack trace for more details.", ex);
        }
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this Int32 value, String elementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlElement element = new System.Xml.XmlDocument().CreateElement(elementName);
        element.InnerText = value.ToString();
        // ----------------------------------------------------------------------------------------------------
        return element;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this Boolean value, String elementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlElement element = new System.Xml.XmlDocument().CreateElement(elementName);
        element.InnerText = value.ToString().ToLower();
        // ----------------------------------------------------------------------------------------------------
        return element;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this DateTime dateTime, String elementName, String format)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(format);
        ArgumentException.ThrowIfNullOrWhiteSpace(format);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlElement element = new System.Xml.XmlDocument().CreateElement(elementName);
        element.InnerText = dateTime.ToString(format);
        // ----------------------------------------------------------------------------------------------------
        return element;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this Dictionary<String, String> dictionary, String rootElementName)
    {
        ArgumentNullException.ThrowIfNull(dictionary);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument xmlDoc = new();
        System.Xml.XmlElement root = xmlDoc.CreateElement(rootElementName);
        foreach ((KeyValuePair<string, string> kvp, System.Xml.XmlElement child) in from KeyValuePair<string, string> kvp in dictionary
                                                                                    let child = xmlDoc.CreateElement(kvp.Key)
                                                                                    select (kvp, child))
        {
            child.InnerText = kvp.Value;
            root.AppendChild(child);
        }
        // ----------------------------------------------------------------------------------------------------
        return root;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this String[] array, String rootElementName, String childElementName)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(childElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(childElementName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument xmlDoc = new();
        System.Xml.XmlElement root = xmlDoc.CreateElement(rootElementName);
        foreach ((string item, System.Xml.XmlElement child) in from string item in array
                                                               let child = xmlDoc.CreateElement(childElementName)
                                                               select (item, child))
        {
            child.InnerText = item;
            root.AppendChild(child);
        }
        // ----------------------------------------------------------------------------------------------------
        return root;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this Object obj)
    {
        ArgumentNullException.ThrowIfNull(obj);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument xmlDoc = new();
        System.Xml.XmlElement root = xmlDoc.CreateElement(obj.GetType().Name);
        foreach ((System.Reflection.PropertyInfo prop, System.Xml.XmlElement element) in from System.Reflection.PropertyInfo prop in obj.GetType().GetProperties()
                                                                                         let element = xmlDoc.CreateElement(prop.Name)
                                                                                         select (prop, element))
        {
            element.InnerText = prop.GetValue(obj)?.ToString() ?? String.Empty;
            root.AppendChild(element);
        }
        // ----------------------------------------------------------------------------------------------------
        return root;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this System.Xml.Linq.XElement xElement)
    {
        ArgumentNullException.ThrowIfNull(xElement);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument xmlDoc = new();
        xmlDoc.LoadXml(xElement.ToString());
        // ----------------------------------------------------------------------------------------------------
        return xmlDoc.DocumentElement;
    }

    public static System.Xml.XmlElement BuildXmlElementFromObject(this Guid guid, String elementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlElement element = new System.Xml.XmlDocument().CreateElement(elementName);
        element.InnerText = guid.ToString();
        // ----------------------------------------------------------------------------------------------------
        return element;
    }
}