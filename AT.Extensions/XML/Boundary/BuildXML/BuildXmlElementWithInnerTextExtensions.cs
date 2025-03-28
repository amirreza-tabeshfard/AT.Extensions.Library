namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementWithInnerTextExtensions
    : Object
{
    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, String innerText)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, innerText);
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("Parent element cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the element name or inner text is incorrect.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("An element with the same name"))
        {
            throw new InvalidOperationException("The parent element already contains a child element with the same name.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Element name"))
        {
            throw new InvalidOperationException("There was an invalid operation involving the element name during XML creation.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A required reference is null during XML element creation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML element due to an unexpected error.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, String innerText, String attributeName, String attributeValue)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        ArgumentException.ThrowIfNullOrEmpty(attributeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, innerText);
            newElement.SetAttributeValue(attributeName, attributeValue);
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName"))
        {
            throw new InvalidOperationException("The attribute name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue"))
        {
            throw new InvalidOperationException("The attribute value cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("A required argument was null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Element name"))
        {
            throw new InvalidOperationException("There was an invalid operation involving the element name during XML element creation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Attribute setting"))
        {
            throw new InvalidOperationException("An error occurred while setting the attribute value.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the element name, inner text, or attribute value is incorrect.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A required reference is null during XML element creation or attribute setting.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML element with attribute due to an unexpected error.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, String innerText, String attributeName1, String attributeValue1, String attributeName2, String attributeValue2)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        ArgumentException.ThrowIfNullOrEmpty(attributeName1);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName1);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue1);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue1);
        ArgumentException.ThrowIfNullOrEmpty(attributeName2);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeName2);
        ArgumentException.ThrowIfNullOrEmpty(attributeValue2);
        ArgumentException.ThrowIfNullOrWhiteSpace(attributeValue2);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, innerText);
            newElement.SetAttributeValue(attributeName1, attributeValue1);
            newElement.SetAttributeValue(attributeName2, attributeValue2);
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName1"))
        {
            throw new InvalidOperationException("The first attribute name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue1"))
        {
            throw new InvalidOperationException("The first attribute value cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeName2"))
        {
            throw new InvalidOperationException("The second attribute name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributeValue2"))
        {
            throw new InvalidOperationException("The second attribute value cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("A required argument was null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Element name"))
        {
            throw new InvalidOperationException("There was an invalid operation involving the element name during XML element creation.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Attribute setting"))
        {
            throw new InvalidOperationException("An error occurred while setting the attributes of the element.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The format of the element name, inner text, or attributes is incorrect.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A required reference is null during XML element creation or attribute setting.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Failed to build XML element with multiple attributes due to an unexpected error.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, String innerText, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, innerText);
            newElement.SetAttributeValue("DateCreated", date.ToString("yyyy-MM-dd"));
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("A required argument was null.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("The parent element reference is null.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Date"))
        {
            throw new InvalidOperationException("The date format is invalid or could not be parsed.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("XML element"))
        {
            throw new InvalidOperationException("An invalid operation occurred while building the XML element with date.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, Int32 id, String innerText)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        // ----------------------------------------------------------------------------------------------------
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new("Item", new System.Xml.Linq.XAttribute("ID", id), innerText);
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("The parent element cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be empty or whitespace.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("id"))
        {
            throw new InvalidOperationException("The ID must be greater than zero.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while building the XML element.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("XML processing error occurred while building XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Add"))
        {
            throw new InvalidOperationException("Failed to add the new element to the parent element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, Int32 id)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        // ----------------------------------------------------------------------------------------------------
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, new System.Xml.Linq.XAttribute("ID", id));
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("The parent element cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be empty or whitespace.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("id"))
        {
            throw new InvalidOperationException("The ID must be greater than zero.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while building the XML element.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("XML processing error occurred while building XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Add"))
        {
            throw new InvalidOperationException("Failed to add the new element to the parent element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, Int32 id, DateTime date)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        // ----------------------------------------------------------------------------------------------------
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "ID must be greater than zero.");
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new("Item", new System.Xml.Linq.XAttribute("ID", id), new System.Xml.Linq.XAttribute("DateCreated", date.ToString("yyyy-MM-dd")));
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("The parent element cannot be null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("id"))
        {
            throw new InvalidOperationException("The ID must be greater than zero.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException("An argument provided to the XML element is invalid.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Date"))
        {
            throw new InvalidOperationException("Failed to format the date attribute correctly.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference was encountered while adding the element.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("XML processing error occurred while building the XML element.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Add"))
        {
            throw new InvalidOperationException("Failed to add the new element to the parent element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }

    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, IEnumerable<Int32> ids)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentNullException.ThrowIfNull(ids);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, ids.Select(id => new System.Xml.Linq.XElement("ID", id)));
            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("The parent element cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("ids"))
        {
            throw new InvalidOperationException("The IDs collection cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && string.IsNullOrEmpty(ex.ParamName))
        {
            throw new InvalidOperationException("The element name cannot be empty or null.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("parentElement"))
        {
            throw new InvalidOperationException("The parent element is null or not properly initialized.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("ids"))
        {
            throw new InvalidOperationException("The IDs collection is null or not properly initialized.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("ID"))
        {
            throw new InvalidOperationException("An issue occurred while adding the new XML element with IDs.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("ID"))
        {
            throw new InvalidOperationException("There was a formatting issue with the IDs while building the XML element.", ex);
        }
        catch (OverflowException ex) when (ex.Message.Contains("ID"))
        {
            throw new InvalidOperationException("An overflow error occurred while processing the IDs.", ex);
        }
        catch (InvalidCastException ex) when (ex.Message.Contains("ID"))
        {
            throw new InvalidOperationException("An invalid cast occurred while processing the IDs.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlElementWithInnerText(this System.Xml.Linq.XElement parentElement, String elementName, String innerText, IEnumerable<KeyValuePair<String, String>> attributes)
    {
        ArgumentNullException.ThrowIfNull(parentElement);
        ArgumentException.ThrowIfNullOrEmpty(elementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementName);
        ArgumentException.ThrowIfNullOrEmpty(innerText);
        ArgumentException.ThrowIfNullOrWhiteSpace(innerText);
        ArgumentNullException.ThrowIfNull(attributes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement newElement = new(elementName, innerText);

            foreach (KeyValuePair<string, string> attribute in attributes)
                newElement.SetAttributeValue(attribute.Key, attribute.Value);

            parentElement.Add(newElement);
            return newElement;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parentElement"))
        {
            throw new InvalidOperationException("The parent element cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributes"))
        {
            throw new InvalidOperationException("The attributes collection cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name cannot be null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("elementName"))
        {
            throw new InvalidOperationException("The element name is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("innerText"))
        {
            throw new InvalidOperationException("The inner text is invalid or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("attributes"))
        {
            throw new InvalidOperationException("The attributes collection is invalid or empty.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("parentElement"))
        {
            throw new InvalidOperationException("The parent element is null or not properly initialized.", ex);
        }
        catch (NullReferenceException ex) when (ex.Message.Contains("attributes"))
        {
            throw new InvalidOperationException("The attributes collection is null or not properly initialized.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("attribute"))
        {
            throw new InvalidOperationException("There was an issue formatting the attribute values.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("XML element"))
        {
            throw new InvalidOperationException("An issue occurred while adding the new XML element with attributes.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("SetAttributeValue"))
        {
            throw new InvalidOperationException("An error occurred while setting the attribute values of the XML element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML element.", ex);
        }
    }
}