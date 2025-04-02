namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromBytesExtensions
    : Object
{
    public static String BuildXmlFromBytes(this Byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(data)))
        {
            throw new ArgumentException("The provided byte array parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("fallback"))
        {
            throw new InvalidOperationException("Character encoding fallback occurred while converting bytes to XML String.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("A nested exception occurred during byte-to-string conversion.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode bytes using UTF-8 encoding.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new InvalidOperationException("System ran out of memory while processing the byte array.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory allocation failed during string conversion.", ex);
        }
        catch (Exception ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("An unknown error occurred with an available stack trace.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting bytes to XML String.", ex);
        }

    }

    public static System.Xml.Linq.XDocument BuildXmlFromBytes(this Byte[] data, System.Text.Encoding encoding)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlString = encoding.GetString(data);
            return System.Xml.Linq.XDocument.Parse(xmlString);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(data)))
        {
            throw new ArgumentException("The provided byte array parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(encoding)))
        {
            throw new ArgumentException("The provided encoding parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("fallback"))
        {
            throw new InvalidOperationException("Character encoding fallback occurred while converting bytes to a string.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("A nested exception occurred during byte-to-string conversion.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Failed to decode bytes using the specified encoding.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML parsing failed at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("A nested XML parsing error occurred.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format detected while parsing the string.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new InvalidOperationException("System ran out of memory while processing the byte array.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory allocation failed during XML parsing.", ex);
        }
        catch (Exception ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("An unknown error occurred with an available stack trace.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting bytes to XML.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromBytes(this Byte[] data, System.Xml.XmlReaderSettings settings)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentNullException.ThrowIfNull(settings);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using MemoryStream ms = new(data);
            using System.Xml.XmlReader reader = System.Xml.XmlReader.Create(ms, settings);
            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.Load(reader);
            return xmlDoc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(data)))
        {
            throw new ArgumentException("The provided byte array parameter is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(settings)))
        {
            throw new ArgumentException("The provided XmlReaderSettings parameter is invalid.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("stream"))
        {
            throw new InvalidOperationException("An error occurred while accessing the memory stream.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("I/O error occurred while reading the byte array as a stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML parsing failed at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("A nested XML parsing error occurred.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format detected while loading the document.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(MemoryStream)))
        {
            throw new InvalidOperationException("The memory stream has been disposed before reading XML.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(System.Xml.XmlReader)))
        {
            throw new InvalidOperationException("The XML reader has been disposed before reading XML.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new InvalidOperationException("System ran out of memory while processing the XML document.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory allocation failed during XML parsing.", ex);
        }
        catch (Exception ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("An unknown error occurred with an available stack trace.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while loading XML from Byte array.", ex);
        }
    }

    public static Boolean BuildXmlFromBytes(this Byte[] data, out System.Xml.Linq.XDocument? document)
    {
        document = default;
        // ----------------------------------------------------------------------------------------------------
        if (data == null || data.Length == 0)
            return false;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlString = System.Text.Encoding.UTF8.GetString(data);
            document = System.Xml.Linq.XDocument.Parse(xmlString);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static Boolean BuildXmlFromBytes(this Byte[] data, System.Text.Encoding encoding, out String? xmlString)
    {
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        xmlString = default;
        // ----------------------------------------------------------------------------------------------------
        if (data == null || data.Length == 0)
            return false;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlString = encoding.GetString(data);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromBytes(this Byte[] data, Boolean ignoreWhitespace)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlReaderSettings settings = new()
            {
                IgnoreWhitespace = ignoreWhitespace
            };
            using MemoryStream ms = new(data);
            using System.Xml.XmlReader reader = System.Xml.XmlReader.Create(ms, settings);
            return System.Xml.Linq.XDocument.Load(reader);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(data)))
        {
            throw new ArgumentException("The provided byte array parameter is invalid.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("stream"))
        {
            throw new InvalidOperationException("An error occurred while accessing the memory stream.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("I/O error occurred while reading the byte array as a stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML parsing failed at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.InnerException is not null)
        {
            throw new InvalidOperationException("A nested XML parsing error occurred.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Invalid XML format detected while loading the document.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(MemoryStream)))
        {
            throw new InvalidOperationException("The memory stream has been disposed before reading XML.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(System.Xml.XmlReader)))
        {
            throw new InvalidOperationException("The XML reader has been disposed before reading XML.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new InvalidOperationException("System ran out of memory while processing the XML document.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory allocation failed during XML parsing.", ex);
        }
        catch (Exception ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("An unknown error occurred with an available stack trace.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while loading XML from bytes.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromBytes(this Byte[] data, String rootNodeName)
    {
        ArgumentNullException.ThrowIfNull(data);
        ArgumentException.ThrowIfNullOrEmpty(rootNodeName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootNodeName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlString = System.Text.Encoding.UTF8.GetString(data);
            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.LoadXml($"<{rootNodeName}>{xmlString}</{rootNodeName}>");
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(data)))
        {
            throw new ArgumentNullException("The provided byte array parameter cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(rootNodeName)))
        {
            throw new ArgumentException("The provided root node name cannot be null, empty, or whitespace.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("fallback"))
        {
            throw new InvalidOperationException("The encoding failed while converting bytes to string.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new InvalidOperationException("Decoder fallback error occurred while decoding the byte array to string.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Root element"))
        {
            throw new InvalidOperationException("The XML format is invalid after wrapping with the root node.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber > 0)
        {
            throw new InvalidOperationException($"XML error occurred at line {ex.LineNumber}, position {ex.LinePosition}.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("Error occurred while parsing the XML string after adding the root node.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.Message.Contains("String length"))
        {
            throw new InvalidOperationException("The XML string length is out of range.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(System.Xml.XmlDocument)))
        {
            throw new InvalidOperationException("The XML document object has been disposed before use.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals(nameof(System.Text.Encoding)))
        {
            throw new InvalidOperationException("The encoding object has been disposed before use.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("memory"))
        {
            throw new InvalidOperationException("System ran out of memory while processing the XML.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Memory allocation failed during XML processing.", ex);
        }
        catch (Exception ex) when (ex.StackTrace is not null)
        {
            throw new InvalidOperationException("An unexpected error occurred with an available stack trace.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while wrapping XML with the root node.", ex);
        }
    }
}