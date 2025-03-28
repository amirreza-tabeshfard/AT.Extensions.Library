namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlElementFromXmlStringExtensions
    : Object
{
    public static System.Xml.XmlElement? BuildXmlElementFromXmlString(this String xml)
    {
        ArgumentException.ThrowIfNullOrEmpty(xml);
        ArgumentException.ThrowIfNullOrWhiteSpace(xml);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = new();
            doc.LoadXml(xml);
            return doc.DocumentElement;
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Out of memory"))
        {
            throw new InvalidOperationException("XML is too large to process. Memory allocation failed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("XML") && ex.Message.Contains("not well-formed"))
        {
            throw new ArgumentException("The XML format is invalid: The structure is not well-formed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("Data at the root level is invalid"))
        {
            throw new ArgumentException("Invalid XML format: Root level data is not valid.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Equals("There is an error in XML document"))
        {
            throw new ArgumentException("Invalid XML format: There is an error in the XML document.", ex);
        }
        catch (System.InvalidOperationException ex) when (ex.Message.Contains("Element not found"))
        {
            throw new InvalidOperationException("XML document does not contain the expected root element.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xml"))
        {
            throw new ArgumentException("The provided XML string cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Equals("The value cannot be null or empty"))
        {
            throw new ArgumentException("The XML string is null or empty, which is invalid input.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("white space"))
        {
            throw new ArgumentException("The provided XML string contains only white spaces, which is invalid input.", ex);
        }
        catch (ApplicationException ex) when (ex.Message.Contains("unexpected error"))
        {
            throw new ApplicationException("An unexpected error occurred while processing XML. Please check the XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unforeseen error occurred while handling the XML.", ex);
        }
    }

    public static System.Xml.XmlElement BuildXmlElementFromXmlString(this String xml, System.Xml.XmlElement parent)
    {
        ArgumentException.ThrowIfNullOrEmpty(xml);
        ArgumentException.ThrowIfNullOrWhiteSpace(xml);
        ArgumentNullException.ThrowIfNull(parent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlElement element = parent.OwnerDocument?.CreateElement(parent.Name)
                                            ?? throw new InvalidOperationException("Parent document is null.");
            element.InnerXml = xml;
            parent.AppendChild(element);
            return element;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("parent"))
        {
            throw new ArgumentNullException("Parent element is null. Ensure a valid XML element is provided.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xml"))
        {
            throw new ArgumentNullException("The XML string parameter cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Parent document is null"))
        {
            throw new InvalidOperationException("Parent document is null. Ensure that the parent element has a valid owner document.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error creating XML element"))
        {
            throw new InvalidOperationException("Error occurred while creating XML element. Verify the parent element is valid and properly initialized.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Invalid XML"))
        {
            throw new System.Xml.XmlException("Invalid XML content detected in 'xml' parameter. The XML structure may be malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'xml' string could not be loaded"))
        {
            throw new System.Xml.XmlException("The XML string cannot be parsed correctly. Ensure the XML is well-formed and valid.", ex);
        }
        catch (Exception ex) when (ex is InvalidOperationException)
        {
            throw new InvalidOperationException("An unexpected error occurred while appending the XML element. Check the XML structure and parent element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unforeseen error occurred while processing the XML element. Please check the inputs.", ex);
        }
    }

    public static System.Xml.XmlElement BuildXmlElementFromXmlString(this String xml, String rootName)
    {
        ArgumentException.ThrowIfNullOrEmpty(xml);
        ArgumentException.ThrowIfNullOrWhiteSpace(xml);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = new();
            System.Xml.XmlElement root = doc.CreateElement(rootName);
            doc.AppendChild(root);

            root.InnerXml = xml;

            return root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xml"))
        {
            throw new ArgumentNullException("The XML string cannot be null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootName"))
        {
            throw new ArgumentNullException("The root element name cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Invalid root element name"))
        {
            throw new ArgumentException("The root element name provided is invalid. Ensure it's a non-empty, valid XML element name.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Invalid XML format"))
        {
            throw new System.Xml.XmlException("Invalid XML format in the provided string. The XML content may be malformed.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The 'xml' string could not be loaded"))
        {
            throw new System.Xml.XmlException("XML content is not well-formed. Please check the XML string for errors.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("An error occurred while appending the root element"))
        {
            throw new InvalidOperationException("An error occurred while appending the root element. Ensure the document structure is correct.", ex);
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            throw new ArgumentException("An unexpected error occurred due to an invalid argument.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while creating the XML element. Please check the inputs and try again.", ex);
        }
    }

    public static System.Xml.XmlElement BuildXmlElementFromXmlString(this String xml, String schema, Boolean validate)
    {
        ArgumentException.ThrowIfNullOrEmpty(xml);
        ArgumentException.ThrowIfNullOrWhiteSpace(xml);
        ArgumentException.ThrowIfNullOrEmpty(schema);
        ArgumentException.ThrowIfNullOrWhiteSpace(schema);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = new();
            doc.LoadXml(xml);

            if (validate)
            {
                System.Xml.Schema.XmlSchemaSet schemaSet = new();

                using StringReader stringReader = new(schema);
                using System.Xml.XmlReader xmlReader = System.Xml.XmlReader.Create(stringReader);

                schemaSet.Add("", xmlReader);
                doc.Schemas.Add(schemaSet);
                doc.Validate(null);
            }

            return doc.DocumentElement!;
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Invalid XML format"))
        {
            throw new ArgumentException("The provided XML is malformed. Please ensure the XML string is valid.", ex);
        }
        catch (ArgumentException ex) when (ex.Message.Contains("Invalid XML schema provided"))
        {
            throw new ArgumentException("The provided XML schema is invalid or malformed. Ensure the schema is well-formed XML.", ex);
        }
        catch (System.Xml.Schema.XmlSchemaValidationException ex) when (ex.Message.Contains("XML validation failed"))
        {
            throw new ArgumentException("XML validation failed. The provided XML does not conform to the schema.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("Error reading the XML schema"))
        {
            throw new IOException("There was an error reading the XML schema. Check the schema format or file path.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Invalid XML format"))
        {
            throw new ArgumentException("The XML format is invalid. Please ensure the XML string is correctly structured.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Error reading XML"))
        {
            throw new ArgumentException("There was an error reading the provided XML string. Verify the XML string is accessible and valid.", ex);
        }
        catch (Exception ex) when (ex is ArgumentException)
        {
            throw new ArgumentException("An unexpected argument error occurred while processing the XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML document. Please check your input.", ex);
        }
    }
}