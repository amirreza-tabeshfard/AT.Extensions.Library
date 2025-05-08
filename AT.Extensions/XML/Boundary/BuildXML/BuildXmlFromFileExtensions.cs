namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromFileExtensions
    : Object
{
    public static System.Xml.XmlDocument BuildXmlFromFile(this String path)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.Load(path);

            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("Path parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("Path parameter is invalid.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(path))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("Could not find a part of the path"))
        {
            throw new InvalidOperationException("Directory not found in the specified path.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new InvalidOperationException("Access to the file path is denied.", ex);
        }
        catch (PathTooLongException ex) when (ex.Message.Contains("The specified path, file name, or both are too long"))
        {
            throw new InvalidOperationException("The specified file path is too long.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("already in use"))
        {
            throw new InvalidOperationException("The file is currently in use by another process.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format at the root level.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("Unexpected end of XML file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The '"))
        {
            throw new InvalidOperationException("Malformed XML structure detected.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("path format is not supported"))
        {
            throw new InvalidOperationException("The given path format is not supported.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("The caller does not have the required permission"))
        {
            throw new InvalidOperationException("Caller does not have permission to access the file.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("Operation is not valid due to the current state of the object"))
        {
            throw new InvalidOperationException("Invalid operation due to the current state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while loading XML from file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromFile(this String path, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new(path, encoding);
            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.Load(reader);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("Encoding parameter is null.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("Path parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("Path parameter is invalid.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("Could not find a part of the path"))
        {
            throw new InvalidOperationException("Directory not found in the specified path.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("already in use"))
        {
            throw new InvalidOperationException("The file is currently in use by another process.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("path format is not supported"))
        {
            throw new InvalidOperationException("The given path format is not supported.", ex);
        }
        catch (PathTooLongException ex) when (ex.Message.Contains("The specified path, file name, or both are too long"))
        {
            throw new InvalidOperationException("The specified file path is too long.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("The caller does not have the required permission"))
        {
            throw new InvalidOperationException("Caller does not have permission to access the file.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new InvalidOperationException("Access to the file path is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format at the root level.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("Unexpected end of XML file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The '"))
        {
            throw new InvalidOperationException("Malformed XML structure detected.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("Operation is not valid due to the current state of the object"))
        {
            throw new InvalidOperationException("Invalid operation due to the current state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while reading XML with specified encoding.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromFile(this FileInfo file)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new();
            xmlDoc.Load(file.FullName);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("file"))
        {
            throw new InvalidOperationException("FileInfo parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("file"))
        {
            throw new InvalidOperationException("FileInfo parameter is invalid.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("Could not find a part of the path"))
        {
            throw new InvalidOperationException("Directory not found in the specified path.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !System.IO.File.Exists(ex.FileName))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("already in use"))
        {
            throw new InvalidOperationException("The file is currently in use by another process.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("path format is not supported"))
        {
            throw new InvalidOperationException("The given path format is not supported.", ex);
        }
        catch (PathTooLongException ex) when (ex.Message.Contains("The specified path, file name, or both are too long"))
        {
            throw new InvalidOperationException("The specified file path is too long.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("The caller does not have the required permission"))
        {
            throw new InvalidOperationException("Caller does not have permission to access the file.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new InvalidOperationException("Access to the file path is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format at the root level.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("Unexpected end of XML file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The '"))
        {
            throw new InvalidOperationException("Malformed XML structure detected.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("Operation is not valid due to the current state of the object"))
        {
            throw new InvalidOperationException("Invalid operation due to the current state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting the file to XmlDocument.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromFile(this String path, Boolean useLinqToXml)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            if (!useLinqToXml)
                throw new InvalidOperationException("The useLinqToXml parameter must be set to true to load XDocument.");

            return System.Xml.Linq.XDocument.Load(path);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("useLinqToXml"))
        {
            throw new InvalidOperationException("You must set useLinqToXml to true to load XDocument.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("The path parameter is null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("useLinqToXml"))
        {
            throw new InvalidOperationException("The useLinqToXml parameter must be set to true to load XDocument.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Message.Contains("Could not find a part of the path"))
        {
            throw new InvalidOperationException("Directory not found in the specified path.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !System.IO.File.Exists(ex.FileName))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("already in use"))
        {
            throw new InvalidOperationException("The file is currently in use by another process.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Contains("path format is not supported"))
        {
            throw new InvalidOperationException("The given path format is not supported.", ex);
        }
        catch (PathTooLongException ex) when (ex.Message.Contains("The specified path, file name, or both are too long"))
        {
            throw new InvalidOperationException("The specified file path is too long.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Message.Contains("The caller does not have the required permission"))
        {
            throw new InvalidOperationException("Caller does not have permission to access the file.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new InvalidOperationException("Access to the file path is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new InvalidOperationException("Invalid XML format at the root level.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end of file"))
        {
            throw new InvalidOperationException("Unexpected end of XML file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("The '"))
        {
            throw new InvalidOperationException("Malformed XML structure detected.", ex);
        }
        catch (SystemException ex) when (ex.Message.Contains("Operation is not valid due to the current state of the object"))
        {
            throw new InvalidOperationException("Invalid operation due to the current state.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while loading XDocument from file.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromFile(this FileInfo file, Boolean validate)
    {
        ArgumentNullException.ThrowIfNull(file);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(file.FullName);

            if (validate)
            {
                if (doc.Root == null)
                    throw new System.Xml.XmlException("The XML file has no root element.");
            }

            return doc;
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(file.FullName, StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("File not found during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new System.Xml.XmlException("Directory not found during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new System.Xml.XmlException("Unauthorized access during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new System.Xml.XmlException("File path too long during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (IOException ex)
        {
            throw new System.Xml.XmlException("IO error during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber.Equals(0))
        {
            throw new System.Xml.XmlException("Malformed XML with no line information during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("unexpected end", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Unexpected end of XML during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("root", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("XML root element issue during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (System.Xml.Schema.XmlSchemaValidationException ex)
        {
            throw new System.Xml.XmlException("Schema validation error during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("file", StringComparison.OrdinalIgnoreCase))
        {
            throw new System.Xml.XmlException("Input file argument is null during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (ArgumentException ex)
        {
            throw new System.Xml.XmlException("Invalid argument during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new System.Xml.XmlException("Unsupported file operation during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new System.Xml.XmlException("Security exception during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new System.Xml.XmlException("Invalid operation during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (FormatException ex)
        {
            throw new System.Xml.XmlException("Invalid format during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new System.Xml.XmlException("Out of memory during loading System.Xml.Linq.XDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new System.Xml.XmlException("Unknown error during loading System.Xml.Linq.XDocument.", ex);
        }
    }

    public static String BuildXmlFromFile(this Stream stream)
    {
        ArgumentNullException.ThrowIfNull(stream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new(stream);
            return reader.ReadToEnd();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("stream"))
        {
            throw new ArgumentNullException("The provided stream parameter is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("stream") && !stream.CanRead)
        {
            throw new ArgumentException("The provided stream cannot be read.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("stream"))
        {
            throw new ArgumentException("The provided stream parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate bytes"))
        {
            throw new IOException("Error decoding the stream content.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("cannot read"))
        {
            throw new IOException("Cannot read from the provided stream.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new IOException("The stream object has already been disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory"))
        {
            throw new IOException("Not enough memory to read the stream.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("access is denied"))
        {
            throw new IOException("Access to the stream was denied.", ex);
        }
        catch (Exception ex)
        {
            throw new IOException("An unexpected error occurred while reading the XML from the stream.", ex);
        }
    }

    public static String BuildXmlFromFile(this Byte[] data)
    {
        ArgumentNullException.ThrowIfNull(data);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return System.Text.Encoding.UTF8.GetString(data);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("data"))
        {
            throw new ArgumentNullException("The provided byte array is null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("data"))
        {
            throw new ArgumentException("The provided byte array is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Message.Contains("Unable to translate bytes"))
        {
            throw new InvalidOperationException("Error decoding the byte array into a valid string.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Message.Contains("Insufficient memory"))
        {
            throw new InvalidOperationException("Not enough memory to process the byte array.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("Invalid byte sequence"))
        {
            throw new InvalidOperationException("The byte array format is invalid for XML conversion.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting the byte array to XML.", ex);
        }
    }

    public static String BuildXmlFromFile(this String path, Boolean trimContent, String errorPrefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentException.ThrowIfNullOrEmpty(errorPrefix);
        ArgumentException.ThrowIfNullOrWhiteSpace(errorPrefix);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = File.ReadAllText(path);

            return trimContent ? content.Trim() : content;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new ArgumentException("The provided file path is null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("errorPrefix"))
        {
            throw new ArgumentException("The provided errorPrefix is null or empty.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Message.Contains("file not found"))
        {
            throw new FileNotFoundException($"{errorPrefix} The file was not found.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new UnauthorizedAccessException($"{errorPrefix} Access to the file path is denied.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The path is not valid"))
        {
            throw new IOException($"{errorPrefix} Error while reading the file XML.", ex);
        }
        catch (Exception ex)
        {
            throw new IOException($"{errorPrefix} An unexpected error occurred while reading the XML file.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromFile(this String path, String rootElementName)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentException.ThrowIfNullOrEmpty(rootElementName);
        ArgumentException.ThrowIfNullOrWhiteSpace(rootElementName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(path);
            System.Xml.Linq.XElement? element = doc.Element(rootElementName);

            if (element == null)
                throw new InvalidOperationException("Root element not found.");

            return element;
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Root element not found."))
        {
            throw new System.Xml.XmlException($"The element '{rootElementName}' was not found in the file.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new InvalidOperationException("The specified file path is invalid.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("rootElementName"))
        {
            throw new InvalidOperationException("The specified root element name is invalid.", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new InvalidOperationException("The specified directory for the file path was not found.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new InvalidOperationException("The specified file path is too long.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An I/O error occurred while accessing the file.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the file path is denied.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LineNumber.Equals(0))
        {
            throw new InvalidOperationException("Invalid XML format: No content found in the file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.LinePosition.Equals(0))
        {
            throw new InvalidOperationException("Invalid XML format: Error at the beginning of the file.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("An error occurred while parsing the XML file.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The file path format is not supported.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("A security error occurred while accessing the file.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("The XML document structure is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unknown error occurred while building the XElement from the file.", ex);
        }
    }

    public static MemoryStream BuildXmlFromFile(this String path, System.Text.Encoding encoding, Boolean leaveOpen)
    {
        ArgumentException.ThrowIfNullOrEmpty(path);
        ArgumentException.ThrowIfNullOrWhiteSpace(path);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = File.ReadAllText(path, encoding);
            Byte[] bytes = encoding.GetBytes(content);

            MemoryStream ms = new(bytes, 0, bytes.Length, false, leaveOpen);
            return ms;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new IOException("Invalid argument: path is null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new IOException("Invalid argument: encoding is null.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new IOException("Directory not found for the specified path.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(path))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new InvalidOperationException("The specified file was not found.", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new IOException("The specified file was not found.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while reading the file.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new IOException("The path format is not supported.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new IOException("Security error: you do not have permission to access the file.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new IOException("Unauthorized access: you do not have permission to read the file.", ex);
        }
        catch (System.Text.DecoderFallbackException ex)
        {
            throw new IOException("The file content could not be decoded using the specified encoding.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new IOException("Insufficient memory to allocate for reading the file.", ex);
        }
        catch (SystemException ex)
        {
            throw new IOException("A system error occurred during the file processing.", ex);
        }
        catch (Exception ex)
        {
            throw new IOException("An unexpected error occurred while building MemoryStream from XML file.", ex);
        }

    }
}