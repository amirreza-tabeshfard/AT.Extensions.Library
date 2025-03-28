namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementWithAttributesExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, String attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue);
            element.Add(newElement);

            return element;
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Element") && ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML element error occurred: {ex.Message} at line {ex.LineNumber}.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for parameter: element.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for parameter: elementName.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for parameter: attributeName.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for parameter: attributeValue.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("empty"))
        {
            throw new InvalidOperationException($"Argument exception due to empty parameter: {ex.Message}.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("whitespace"))
        {
            throw new InvalidOperationException($"Argument exception due to parameter containing only whitespace: {ex.Message}.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("value"))
        {
            throw new InvalidOperationException($"Argument exception due to invalid attribute value: {ex.Message}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Invalid operation when adding XML element"))
        {
            throw new InvalidOperationException($"Invalid operation while adding XML element: {ex.Message}.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("building XML"))
        {
            throw new InvalidOperationException($"Error occurred during the process of building XML element: {ex.Message}.", ex);
        }
        catch (Exception ex) when (ex is ArgumentOutOfRangeException)
        {
            throw new InvalidOperationException($"Argument out of range exception occurred: {ex.Message}", ex);
        }
        catch (Exception ex) when (ex.Message.Contains("unexpected"))
        {
            throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Int32 attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue);
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for the 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for the 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for the 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for the 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error occurred: {ex.Message}", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException($"Format exception: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Operation error occurred while building XML element: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Boolean attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue);
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException($"Format exception: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Operation error occurred while building XML element: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, DateTime attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue.ToString("o"));
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException($"DateTime format error: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Operation error occurred while building XML element: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"An unexpected error occurred: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Decimal attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue.ToString("F2"));
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new InvalidOperationException($"Invalid format for decimal value: {ex.Message}", ex);
        }
        catch (OverflowException ex)
        {
            throw new InvalidOperationException($"Overflow error: The decimal value is out of range: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Error occurred during the operation: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Guid attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue.ToString());
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new InvalidOperationException($"Invalid GUID format: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Invalid operation during XML element creation: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Double attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, attributeValue.ToString("F4"));
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid"))
        {
            throw new InvalidOperationException($"Invalid format for Double value: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException($"Invalid operation during XML element creation: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, List<String> attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);
            newElement.SetAttributeValue(attributeName, String.Join(",", attributeValue));
            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("List"))
        {
            throw new InvalidOperationException("Argument exception: Invalid 'List<String>' parameter.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("List"))
        {
            throw new InvalidOperationException($"Operation error while processing List<String>: {ex.Message}", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, Dictionary<String, String> attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);

            foreach (KeyValuePair<string, string> kvp in attributeValue)
                newElement.SetAttributeValue(kvp.Key, kvp.Value);

            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Dictionary"))
        {
            throw new InvalidOperationException("Argument exception: Invalid 'Dictionary<String, String>' parameter.", ex);
        }
        catch (KeyNotFoundException ex) when (ex.Message.Contains("Key"))
        {
            throw new InvalidOperationException("Key not found in the dictionary while processing attributes.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("foreach"))
        {
            throw new InvalidOperationException("Invalid operation error while iterating over the dictionary keys.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException($"XML processing error: {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithAttributes(this System.Xml.Linq.XElement element, String elementName, String attributeName, object[] attributeValue)
    {
        ArgumentNullException.ThrowIfNull(element);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        ArgumentNullException.ThrowIfNull(attributeValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName);

            string joinedValues = String.Join(",", attributeValue.Select(v => v?.ToString() ?? string.Empty));
            newElement.SetAttributeValue(attributeName, joinedValues);

            element.Add(newElement);

            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("element"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'element' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'elementName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeName' parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception occurred for 'attributeValue' parameter.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("Argument exception: 'elementName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("Argument exception: 'attributeName' cannot be null or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("Argument null exception: 'attributeValue' cannot be null.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("Object of type"))
        {
            throw new InvalidOperationException("Invalid cast exception occurred during processing of attribute values.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("array"))
        {
            throw new InvalidOperationException("Invalid cast exception while processing the array elements.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("array"))
        {
            throw new InvalidOperationException("Null reference exception occurred while processing array elements.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("attributeValue"))
        {
            throw new InvalidOperationException("Null reference exception occurred due to 'attributeValue' being null.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("element"))
        {
            throw new InvalidOperationException("Null reference exception occurred due to 'element' being null.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Unexpected error: {ex.Message}", ex);
        }
    }
}