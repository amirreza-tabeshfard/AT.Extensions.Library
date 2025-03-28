namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromAppConfigExtenstions
    : Object
{
    public static String BuildXmlFromAppConfig(this String xmlContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(xmlContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(xmlContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Parse(xmlContent);
            return doc.ToString();
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(xmlContent))
        {
            throw new InvalidOperationException("XML content cannot be null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(xmlContent))
        {
            throw new InvalidOperationException("XML content cannot contain only white spaces.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The XML content is not well-formed.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while processing the XML content.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to process the XML content.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML content.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(file.FullName);
            return doc.ToString();
        }
        catch (ArgumentNullException ex) when (file is null)
        {
            throw new InvalidOperationException("The provided file is null.", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException("The specified XML file was not found at the provided path.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the specified XML file is denied. Please check file permissions.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The XML content in the file is not well-formed.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the XML file. Ensure the file is accessible and not in use.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to process the XML file. Consider freeing up memory resources.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML file.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(stream);
            return doc.ToString();
        }
        catch (ArgumentException ex) when (stream is null)
        {
            throw new InvalidOperationException("The provided stream is null. Please provide a valid stream.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The XML format in the stream is invalid. Ensure the stream contains well-formed XML.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the stream. Please ensure the stream is accessible and not in use by another process.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the stream is denied. Ensure you have the necessary permissions to read the stream.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML from the stream.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using MemoryStream stream = new(data);
            return stream.BuildXmlFromAppConfig();
        }
        catch (ArgumentException ex) when (data is null)
        {
            throw new InvalidOperationException("The provided byte array is null. Please provide a valid byte array.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The XML format in the byte array is invalid. Ensure the byte array contains well-formed XML.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while reading the byte array. Please ensure the byte array is accessible.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to process the byte array. Consider freeing up memory resources.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the XML from the byte array.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this System.Xml.XmlDocument xmlDocument)
    {
        ArgumentNullException.ThrowIfNull(xmlDocument);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return xmlDocument.OuterXml;
        }
        catch (ArgumentNullException ex) when (xmlDocument is null)
        {
            throw new InvalidOperationException("The provided XmlDocument is null. Please ensure the XmlDocument is properly initialized.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while processing the XML document. The document may not be well-formed.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while converting XmlDocument to string. Consider freeing up memory resources.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("The XmlDocument is improperly initialized or references null values.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting XmlDocument to string.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this System.Xml.Linq.XElement xElement)
    {
        ArgumentNullException.ThrowIfNull(xElement);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return xElement.ToString();
        }
        catch (ArgumentNullException ex) when (xElement is null)
        {
            throw new InvalidOperationException("The provided XElement is null. Ensure the XElement is properly initialized before usage.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while processing the XML element. The XElement may be malformed or invalid.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while converting XElement to string. Please ensure enough memory is available for this operation.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new InvalidOperationException("The XElement reference is invalid or improperly initialized.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting XElement to string.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this Dictionary<String, String> configData)
    {
        ArgumentNullException.ThrowIfNull(configData);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("AppConfig",
                                            configData.Select(selector: static kv =>
                                            {
                                                return new System.Xml.Linq.XElement(kv.Key, kv.Value);
                                            }));
            return root.ToString();
        }
        catch (ArgumentNullException ex) when (configData is null)
        {
            throw new InvalidOperationException("The provided dictionary is null. Please ensure it is initialized properly before usage.", ex);
        }
        catch (ArgumentException ex) when (configData.Any(kv => string.IsNullOrEmpty(kv.Key)))
        {
            throw new InvalidOperationException("The dictionary contains null or empty keys, which are invalid for XML elements.", ex);
        }
        catch (ArgumentException ex) when (configData.Any(kv => string.IsNullOrEmpty(kv.Value)))
        {
            throw new InvalidOperationException("The dictionary contains null or empty values, which are invalid for XML elements.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while generating XML from the dictionary. The keys or values may contain invalid characters for XML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while creating XML from the dictionary. Please ensure there is enough memory available for this operation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from the dictionary.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this System.Collections.Specialized.NameValueCollection config)
    {
        ArgumentNullException.ThrowIfNull(config);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("AppConfig",
                                            config.AllKeys.Select(key =>
                                            {
                                                return new System.Xml.Linq.XElement(key, config[key]);
                                            }));
            return root.ToString();
        }
        catch (ArgumentNullException ex) when (config is null)
        {
            throw new InvalidOperationException("The provided NameValueCollection is null. Please ensure it is initialized properly before usage.", ex);
        }
        catch (ArgumentException ex) when (config.AllKeys.Any(key => string.IsNullOrEmpty(key)))
        {
            throw new InvalidOperationException("The NameValueCollection contains null or empty keys, which are invalid for XML elements.", ex);
        }
        catch (ArgumentException ex) when (config.AllKeys.Any(key => string.IsNullOrEmpty(config[key])))
        {
            throw new InvalidOperationException("The NameValueCollection contains null or empty values, which are invalid for XML elements.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while generating XML from the NameValueCollection. The keys or values may contain invalid characters for XML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while creating XML from the NameValueCollection. Please ensure there is enough memory available for this operation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while generating XML from the NameValueCollection.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this System.Xml.XmlReader reader)
    {
        ArgumentNullException.ThrowIfNull(reader);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(reader);
            return doc.ToString();
        }
        catch (ArgumentNullException ex) when (reader is null)
        {
            throw new InvalidOperationException("The provided XmlReader is null. Please ensure it is initialized properly before usage.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format encountered while reading from XmlReader. The XML may be malformed or contain invalid characters.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("I/O error occurred while reading XML from the XmlReader. This could be due to file access issues or a corrupted input stream.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while reading XML from the XmlReader. Please ensure there is enough memory available for this operation.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while reading XML from the XmlReader.", ex);
        }
    }

    public static String BuildXmlFromAppConfig(this FileInfo file, String xPath)
    {
        ArgumentNullException.ThrowIfNull(file);
        ArgumentException.ThrowIfNullOrEmpty(xPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(xPath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = new();
            doc.Load(file.FullName);
            System.Xml.XmlNode? node = doc.SelectSingleNode(xPath);
            return node?.OuterXml ?? "<Empty></Empty>";
        }
        catch (ArgumentNullException ex) when (file == null)
        {
            throw new InvalidOperationException("The provided file is null.", ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrEmpty(xPath))
        {
            throw new InvalidOperationException("The provided XPath is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (string.IsNullOrWhiteSpace(xPath))
        {
            throw new InvalidOperationException("The provided XPath is null or consists only of whitespace.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new InvalidOperationException("The provided XPath is invalid or empty.", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException("The config file was not found.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while loading or processing the XML document.", ex);
        }
        catch (System.Xml.XPath.XPathException ex)
        {
            throw new InvalidOperationException("Invalid XPath expression provided.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory while processing the XML file.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the config file is denied.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the config file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while extracting XML with XPath.", ex);
        }
    }
}