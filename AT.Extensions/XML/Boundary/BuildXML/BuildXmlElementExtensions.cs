namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(elementName);
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, String value)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(elementName, value);
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, String attributeName, String attributeValue)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(elementName, new System.Xml.Linq.XAttribute(attributeName, attributeValue));
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, Dictionary<String, String> attributes)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(elementName, attributes.Select(a => new System.Xml.Linq.XAttribute(a.Key, a.Value)));
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, params System.Xml.Linq.XElement[] children)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(children);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(elementName, children);
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, System.Xml.Linq.XNamespace ns)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(ns);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(ns + elementName);
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, System.Xml.Linq.XNamespace ns, Dictionary<String, String> attributes)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(ns);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(ns + elementName, attributes.Select(a => new System.Xml.Linq.XAttribute(a.Key, a.Value)));
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, System.Xml.Linq.XNamespace ns, Dictionary<String, String> attributes, params System.Xml.Linq.XElement[] children)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(ns);
        ArgumentNullException.ThrowIfNull(attributes);
        ArgumentNullException.ThrowIfNull(children);
        // ----------------------------------------------------------------------------------------------------
        return new System.Xml.Linq.XElement(ns + elementName, attributes.Select(a => new System.Xml.Linq.XAttribute(a.Key, a.Value)), children);
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, String value, Dictionary<String, String>? attributes)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.Linq.XElement element = new(elementName, value);
        // ----------------------------------------------------------------------------------------------------
        if (attributes is not null)
            foreach (KeyValuePair<string, string> attr in attributes)
                element.Add(new System.Xml.Linq.XAttribute(attr.Key, attr.Value));
        // ----------------------------------------------------------------------------------------------------
        return element;
    }

    public static System.Xml.Linq.XElement BuildXmlElement(this String elementName, System.Xml.Linq.XNamespace ns, Dictionary<String, String> attributes, String value, params System.Xml.Linq.XElement[] children)
    {
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(ns);
        ArgumentNullException.ThrowIfNull(attributes);
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        ArgumentNullException.ThrowIfNull(children);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.Linq.XElement element = new(ns + elementName, value, children);
        // ----------------------------------------------------------------------------------------------------
        foreach (KeyValuePair<string, string> attr in attributes)
            element.Add(new System.Xml.Linq.XAttribute(attr.Key, attr.Value));
        // ----------------------------------------------------------------------------------------------------
        return element;
    }
}