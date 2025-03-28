namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromBase64Extensions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromBase64(this String base64String)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] bytes = Convert.FromBase64String(base64String);
            String xmlContent = System.Text.Encoding.UTF8.GetString(bytes);

            System.Xml.XmlDocument doc = new();
            doc.LoadXml(xmlContent);
            return doc;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is a properly encoded Base64 value.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Error decoding Base64 string due to encoding issues.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format: The XML content is malformed or incomplete.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Unexpected end of file"))
        {
            throw new InvalidOperationException("Invalid XML format: The XML content appears to be truncated.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the Base64 string.", ex);
        }
    }

    public static System.Xml.XmlNode BuildXmlFromBase64(this String base64String, System.Xml.XmlDocument xmlDoc)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentNullException.ThrowIfNull(xmlDoc);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            return xmlDoc.ImportNode(doc.DocumentElement, true);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (xmlDoc is null)
        {
            throw new InvalidOperationException("Target XmlDocument cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is correctly encoded in Base64.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format: The XML content is malformed or incorrectly structured.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Unexpected end of file"))
        {
            throw new InvalidOperationException("Invalid XML format: The XML content appears to be truncated or incomplete.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("XML document does not contain a valid root element to import.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML node.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromBase64(this String base64String, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] bytes = Convert.FromBase64String(base64String);
            String xmlContent = encoding.GetString(bytes);

            System.Xml.XmlDocument doc = new();
            doc.LoadXml(xmlContent);
            return doc;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be null or empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be null or whitespace.", ex);
        }
        catch (ArgumentNullException ex)
        {
            throw new InvalidOperationException("Encoding cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Error decoding Base64 to XML due to character encoding issues.", ex);
        }
        catch (Exception ex) when (ex is FormatException || ex is System.Xml.XmlException)
        {
            throw new InvalidOperationException("Error processing Base64 to XML.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred.", ex);
        }
    }

    public static Stream BuildXmlFromBase64(this String base64String, bool returnAsStream)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] bytes = Convert.FromBase64String(base64String);
            return new MemoryStream(bytes);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentException ex) when (!returnAsStream)
        {
            throw new InvalidOperationException("The returnAsStream parameter must be set to true to return a valid stream.", ex);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is correctly encoded in Base64.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting Base64 to Stream.", ex);
        }
    }

    public static System.Xml.XmlReader BuildXmlFromBase64(this String base64String, System.Xml.XmlReaderSettings settings)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            return System.Xml.XmlReader.Create(new StringReader(doc.OuterXml), settings);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (settings is null)
        {
            throw new InvalidOperationException("XML Reader settings cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is correctly encoded in Base64.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format. Ensure the Base64 string represents a valid XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting Base64 to XML Reader.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromBase64(this String base64String, String rootName)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentException.ThrowIfNullOrEmpty(rootName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootName);
        // ----------------------------------------------------------------------------------------------------
        System.Xml.XmlDocument? doc = default;
        System.Xml.XmlElement? root = default;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            doc = base64String.BuildXmlFromBase64();
            root = doc.CreateElement(rootName);
            root.AppendChild(doc.DocumentElement);
            doc.AppendChild(root);
            return doc;
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(rootName))
        {
            throw new InvalidOperationException("Root element name cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(rootName))
        {
            throw new InvalidOperationException("Root element name cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (rootName is null)
        {
            throw new InvalidOperationException("Root element name cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is correctly encoded in Base64.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format. Ensure the Base64 string represents a valid XML document.", ex);
        }
        catch (NullReferenceException ex) when (doc?.DocumentElement is null)
        {
            throw new InvalidOperationException("Generated XML document does not contain a valid root element.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting Base64 to XML Document.", ex);
        }
    }

    public static System.Xml.XmlNodeList BuildXmlFromBase64(this String base64String, String xPath, System.Xml.XmlNamespaceManager nsManager)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentException.ThrowIfNullOrEmpty(xPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(xPath);
        ArgumentNullException.ThrowIfNull(nsManager);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            return doc.SelectNodes(xPath, nsManager);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(xPath))
        {
            throw new InvalidOperationException("XPath expression cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(xPath))
        {
            throw new InvalidOperationException("XPath expression cannot be whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (xPath is null)
        {
            throw new InvalidOperationException("XPath expression cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (nsManager is null)
        {
            throw new InvalidOperationException("Namespace Manager cannot be null.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid Base64 format. Ensure the input string is correctly encoded in Base64.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format. Ensure the Base64 string represents a valid XML document.", ex);
        }
        catch (System.Xml.XPath.XPathException ex)
        {
            throw new InvalidOperationException("Error evaluating the provided XPath expression. Ensure it is correctly formatted.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML document.", ex);
        }
    }

    public static System.Xml.XmlNode BuildXmlFromBase64(this String base64String, object xPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentException.ThrowIfNullOrEmpty(xPath.ToString());
        ArgumentException.ThrowIfNullOrWhiteSpace(xPath.ToString());
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            return doc.SelectSingleNode(xPath.ToString());
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (xPath == null)
        {
            throw new InvalidOperationException("XPath cannot be null.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(xPath.ToString()))
        {
            throw new InvalidOperationException("XPath cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(xPath.ToString()))
        {
            throw new InvalidOperationException("XPath cannot be whitespace.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("Invalid format in the Base64 string.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error processing XML data.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("The XML document or XPath expression resulted in a null reference.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the Base64 string and XPath.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromBase64(this String base64String, bool preserveNamespaces, System.Xml.XmlNamespaceManager nsManager)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentNullException.ThrowIfNull(nsManager);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            if (!preserveNamespaces)
                foreach (System.Xml.XmlNode node in doc.SelectNodes("//*"))
                    node.Prefix = "";
            return doc;
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (nsManager == null)
        {
            throw new InvalidOperationException("XmlNamespaceManager cannot be null.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error processing XML data from the Base64 string.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("A null reference occurred while processing the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the Base64 string.", ex);
        }
    }

    public static void BuildXmlFromBase64(this String base64String, System.Xml.XmlWriter writer)
    {
        ArgumentException.ThrowIfNullOrEmpty(base64String);
        ArgumentException.ThrowIfNullOrWhiteSpace(base64String);
        ArgumentNullException.ThrowIfNull(writer);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = base64String.BuildXmlFromBase64();
            doc.WriteTo(writer);
        }
        catch (ArgumentNullException ex) when (base64String is null)
        {
            throw new InvalidOperationException("Input Base64 string cannot be null.", ex);
        }
        catch (ArgumentNullException ex) when (writer == null)
        {
            throw new InvalidOperationException("XmlWriter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrEmpty(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be empty.", ex);
        }
        catch (ArgumentException ex) when (string.IsNullOrWhiteSpace(base64String))
        {
            throw new InvalidOperationException("Input Base64 string cannot be whitespace.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error processing XML data from the Base64 string.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while writing XML to the XmlWriter.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while building the XML from the Base64 string and writing it to the XmlWriter.", ex);
        }
    }
}