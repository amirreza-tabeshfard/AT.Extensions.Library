namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementWithNamespaceExtenstions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XNamespace ns = namespaceUri;
            return new System.Xml.Linq.XElement(ns + elementName);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (UriFormatException ex) when (namespaceUri.StartsWith("http", StringComparison.OrdinalIgnoreCase) == false)
        {
            throw new FormatException("The provided namespace URI does not follow a valid URI format.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML element name.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, String value)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrWhiteSpace(value);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.Value = value;
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("value", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided value for the XML element is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("value", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided value for the XML element is either empty or consists of only whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set value", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting the value of the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating and setting value for the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, String attributeName, String attributeValue)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.SetAttributeValue(attributeName, attributeValue);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("attributeName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided attribute name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("attributeValue", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided attribute value is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("attributeName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided attribute name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("attributeValue", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided attribute value is either empty or consists of only whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting an attribute for the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting the attribute of the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and setting its attribute.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, params (String name, String value)[] attributes)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            foreach ((String name, String value) in attributes)
                element.SetAttributeValue(name, value);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("attributes", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided attributes collection is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("attributes", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more attributes provided are invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("name", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more attribute names are empty or consist of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("value", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more attribute values are empty or consist of only whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting one or more attributes for the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting an attribute of the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set multiple attributes", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting multiple attributes of the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and setting its attributes.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, System.Xml.Linq.XElement childElement)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(childElement);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.Add(childElement);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("childElement", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided child element is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("child", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding the child element to the XML structure.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add child", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding the child element to the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and adding a child element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, params System.Xml.Linq.XElement[] childElements)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(childElements);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.Add(childElements);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("childElements", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided child elements collection is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("childElements", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more child elements are invalid or null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("child", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding the child elements to the XML structure.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add child", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding child elements to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The provided child elements sequence is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and adding child elements.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, System.Xml.Linq.XCData cdata)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(cdata);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.Add(cdata);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("cdata", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided CDATA section is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("cdata", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided CDATA section is invalid or empty.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("cdata", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An XML processing error occurred while adding CDATA to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add cdata", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding CDATA to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The provided CDATA sequence is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and adding CDATA.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, System.Xml.Linq.XComment comment)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(comment);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri);
            element.Add(comment);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("comment", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XML comment is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("comment", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided XML comment is invalid or empty.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("comment", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An XML processing error occurred while adding the comment to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add comment", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding a comment to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("sequence", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("The provided XML comment sequence is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element and adding the comment.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, String value, params (String name, String attributeValue)[] attributes)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri, attributes);
            element.Value = value;
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("attributes", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided attributes array is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("value", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided value for the XML element is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("attributes", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more attribute name-value pairs are invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("value", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided value for the XML element is invalid.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while processing XML attributes.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("value", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while setting the value of the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding attributes to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set value", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting the value of the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element, setting its value, and adding attributes.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithNamespace(this System.Xml.Linq.XDocument doc, String elementName, String namespaceUri, System.Xml.Linq.XCData cdata, params (String name, String attributeValue)[] attributes)
    {
        ArgumentNullException.ThrowIfNull(doc);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(namespaceUri);
        ArgumentException.ThrowIfNullOrWhiteSpace(namespaceUri);
        ArgumentNullException.ThrowIfNull(cdata);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = doc.BuildXmlElementWithNamespace(elementName, namespaceUri, attributes);
            element.Add(cdata);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("doc", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided XDocument instance is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("elementName", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided element name is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("namespaceUri", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided namespace URI is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("cdata", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided CDATA section is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName?.Equals("attributes", StringComparison.OrdinalIgnoreCase) ?? false)
        {
            throw new ArgumentNullException("The provided attributes array is null.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("elementName", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided element name is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("namespaceUri", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided namespace URI is either empty or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("attributes", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("One or more attribute name-value pairs are invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("cdata", StringComparison.OrdinalIgnoreCase))
        {
            throw new ArgumentException("The provided CDATA content is invalid.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("namespace", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while processing the XML namespace.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("element", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while creating the XML element.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while processing XML attributes.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("CDATA", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("An error occurred while adding the CDATA section to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("modify", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An invalid operation occurred while modifying the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add attribute", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding attributes to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("add CDATA", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while adding the CDATA section to the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("set value", StringComparison.OrdinalIgnoreCase))
        {
            throw new InvalidOperationException("An error occurred while setting the value of the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element, adding CDATA, and setting attributes.", ex);
        }
    }
}