namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromDynamicObjectExtenstions
    : Object
{
    public static String BuildXmlFromDynamicObject(this Object obj, String rootElementName)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xmlDoc = new(content: new System.Xml.Linq.XElement(rootElementName, obj));
            return xmlDoc.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The provided object is null. Failed to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name is null. Failed to build XML.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name is empty or contains only white spaces. Failed to build XML.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The object could not be formatted correctly into XML. Check the structure of the dynamic object.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while creating the XML. Please check the input values.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an error while processing XML data. The structure may be invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from dynamic object.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromDynamicObject(this Object obj, String rootElementName, String declaration)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(declaration);
        ArgumentException.ThrowIfNullOrWhiteSpace(declaration);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument xmlDoc = new(declaration: new System.Xml.Linq.XDeclaration(declaration, "utf-8", "yes"), content: new System.Xml.Linq.XElement(rootElementName, obj));

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The object parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("declaration"))
        {
            throw new InvalidOperationException("The XML declaration cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The root element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("declaration") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The XML declaration cannot be null, empty, or whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Failed to build XML from dynamic object."))
        {
            throw new InvalidOperationException("There was an error in building the XML document.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an error in the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDynamicObject(this Object obj, String rootElementName, String childElementName, Boolean includeAttributes)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(childElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(childElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(name: rootElementName, content: new System.Xml.Linq.XElement(childElementName, obj));
            
            if (includeAttributes)
                element.SetAttributeValue("created", DateTime.Now.ToString());

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The object parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("childElementName"))
        {
            throw new InvalidOperationException("The child element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The root element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("childElementName") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The child element name cannot be null, empty, or whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Failed to build XML from dynamic Object"))
        {
            throw new InvalidOperationException("There was an error in building the XML element from the dynamic object.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an error in the XML structure or format.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The dynamic object parameter cannot be null.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static String BuildXmlFromDynamicObject(this Object obj, String rootElementName, String childElementName, Boolean includeAttributes, Boolean formatted)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(childElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(childElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(name: rootElementName,content: new System.Xml.Linq.XElement(childElementName, obj));
            
            if (includeAttributes)
                element.SetAttributeValue("created", DateTime.Now.ToString());

            System.Xml.Linq.XDocument xmlDoc = new(element);
            return formatted ? xmlDoc.ToString(System.Xml.Linq.SaveOptions.None) : xmlDoc.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The object parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("childElementName"))
        {
            throw new InvalidOperationException("The child element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The root element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("childElementName") && string.IsNullOrWhiteSpace(ex.ParamName))
        {
            throw new InvalidOperationException("The child element name cannot be null, empty, or whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Failed to build XML from dynamic Object"))
        {
            throw new InvalidOperationException("There was an error while building the XML from the dynamic object.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("There was an error in the XML structure or format.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("formatted"))
        {
            throw new InvalidOperationException("The formatted flag is out of valid range.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("There was an error in formatting the date or XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML string.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromDynamicObject(this Object obj, String rootElementName, Dictionary<String, Object> attributes)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName, obj);

            if (attributes != null)
                foreach (KeyValuePair<String, Object> attribute in attributes)
                    element.SetAttributeValue(attribute.Key, attribute.Value);

            System.Xml.Linq.XDocument xmlDoc = new(element);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The provided object is null. Failed to build XML.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name is either null or empty. Failed to build XML.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName") && string.IsNullOrWhiteSpace(rootElementName))
        {
            throw new InvalidOperationException("The root element name is empty or consists of only whitespace. Failed to build XML.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributes"))
        {
            throw new InvalidOperationException("The attributes dictionary is null. Failed to build XML.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("General failure occurred while building XML from the dynamic object.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the dynamic object.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDynamicObject(this Object obj, String rootElementName, String childElementName, Boolean includeAttributes, String dateFormat)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(childElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(childElementName);
        ArgumentException.ThrowIfNullOrEmpty(dateFormat);
        ArgumentException.ThrowIfNullOrWhiteSpace(dateFormat);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName, new System.Xml.Linq.XElement(childElementName, obj));
            
            if (includeAttributes)
                element.SetAttributeValue("created", DateTime.Now.ToString(dateFormat));

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("obj"))
        {
            throw new InvalidOperationException("The input object cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The root element name is either null, empty, or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("childElementName"))
        {
            throw new InvalidOperationException("The child element name is either null, empty, or consists of only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("dateFormat"))
        {
            throw new InvalidOperationException("The date format is either null, empty, or consists of only whitespace.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("String was not recognized as a valid DateTime"))
        {
            throw new InvalidOperationException("The provided date format is invalid and cannot be parsed.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while building the XML from the dynamic object.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the dynamic object.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromDynamicObject(this Object obj, String rootElementName, String childElementName, Boolean includeAttributes, String dateFormat, String version)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentException.ThrowIfNullOrEmpty(childElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(childElementName);
        ArgumentException.ThrowIfNullOrEmpty(dateFormat);
        ArgumentException.ThrowIfNullOrWhiteSpace(dateFormat);
        ArgumentException.ThrowIfNullOrEmpty(version);
        ArgumentException.ThrowIfNullOrWhiteSpace(version);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName, new System.Xml.Linq.XElement(childElementName, obj));
            
            if (includeAttributes)
            {
                element.SetAttributeValue("created", DateTime.Now.ToString(dateFormat));
                element.SetAttributeValue("version", version);
            }

            System.Xml.Linq.XDocument xmlDoc = new(new System.Xml.Linq.XDeclaration("1.0", "utf-8", "yes"), element);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(obj)))
        {
            throw new ArgumentNullException(nameof(obj), "The provided object cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentNullException(nameof(rootElementName), "The root element name cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(childElementName)))
        {
            throw new ArgumentNullException(nameof(childElementName), "The child element name cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dateFormat)))
        {
            throw new ArgumentNullException(nameof(dateFormat), "The date format cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(version)))
        {
            throw new ArgumentNullException(nameof(version), "The version cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentException("The root element name cannot be empty or whitespace.", nameof(rootElementName));
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(childElementName)))
        {
            throw new ArgumentException("The child element name cannot be empty or whitespace.", nameof(childElementName));
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(dateFormat)))
        {
            throw new ArgumentException("The date format cannot be empty or whitespace.", nameof(dateFormat));
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(version)))
        {
            throw new ArgumentException("The version cannot be empty or whitespace.", nameof(version));
        }
        catch (FormatException ex)
        {
            throw new FormatException("An error occurred while formatting date or XML content.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while building the XML document.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation occurred while processing the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building XML from the dynamic object.", ex);
        }
    }

    public static String BuildXmlFromDynamicObject(this Object obj, String rootElementName, List<KeyValuePair<String, Object>> attributes)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName, obj);
            
            foreach (KeyValuePair<String, Object> attribute in attributes)
                element.SetAttributeValue(attribute.Key, attribute.Value);

            System.Xml.Linq.XDocument xmlDoc = new(element);
            return xmlDoc.ToString();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(obj)))
        {
            throw new ArgumentNullException(nameof(obj), "The provided object cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentNullException(nameof(rootElementName), "The root element name cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(attributes)))
        {
            throw new ArgumentNullException(nameof(attributes), "The attributes list cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentException("The root element name cannot be empty.", nameof(rootElementName));
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentException("The root element name cannot be whitespace.", nameof(rootElementName));
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML-related error occurred while building the document.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("A general invalid operation occurred during XML generation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML from dynamic Object due to an unexpected error.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromDynamicObject(this Object obj, String rootElementName, IEnumerable<String> childElementNames)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        ArgumentNullException.ThrowIfNull(childElementNames);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName);
            
            foreach (String childElementName in childElementNames)
                element.Add(new System.Xml.Linq.XElement(childElementName, obj));

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(obj)))
        {
            throw new ArgumentNullException(nameof(obj), "The provided object is null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentNullException(nameof(rootElementName), "The root element name cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(childElementNames)))
        {
            throw new ArgumentNullException(nameof(childElementNames), "The collection of child element names cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentException("The root element name cannot be empty or whitespace.", nameof(rootElementName));
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An XML error occurred while building the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An invalid operation was encountered while constructing the XML document.", ex);
        }
        catch (FormatException ex)
        {
            throw new FormatException("The format of a provided value is incorrect while constructing XML elements.", ex);
        }
        catch (OverflowException ex)
        {
            throw new OverflowException("A numeric overflow occurred while processing values for XML generation.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new OutOfMemoryException("The system ran out of memory while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from the dynamic object.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromDynamicObject(this Object obj, String rootElementName, Boolean includeAttributes, Boolean formatted, Boolean includeTimestamp)
    {
        ArgumentNullException.ThrowIfNull(obj);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(rootElementName, obj);
            
            if (includeAttributes)
                element.SetAttributeValue("created", DateTime.Now.ToString());

            if (includeTimestamp)
                element.SetAttributeValue("timestamp", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));

            System.Xml.Linq.XDocument xmlDoc = new(element);
            return formatted ? xmlDoc : xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(obj)))
        {
            throw new ArgumentNullException(nameof(obj), "The provided object cannot be null.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentNullException(nameof(rootElementName), "The root element name cannot be null.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootElementName)))
        {
            throw new ArgumentException("The root element name cannot be empty or whitespace.", nameof(rootElementName));
        }
        catch (FormatException ex)
        {
            throw new FormatException("An error occurred while formatting date values.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while processing the XML structure.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new Exception("An unknown error occurred during XML generation.", ex);
        }
    }
}