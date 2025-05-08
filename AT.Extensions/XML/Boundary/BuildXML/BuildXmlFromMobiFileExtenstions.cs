namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromMobiFileExtenstions
    : Object
{
    public static System.Xml.Linq.XDocument BuildXmlFromMobiFile(this String mobiContent)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = new(new System.Xml.Linq.XElement("Boundary", new System.Xml.Linq.XElement("Content", mobiContent.Trim())));

            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("Invalid argument: mobiContent parameter is invalid.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Invalid operation occurred while creating the XML document.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("The operation is not supported by the XML creation engine.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Attempted to access a disposed object during XML processing.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Insufficient memory to construct the XML document.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Security error encountered while processing the XML.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Malformed XML content detected during XML document creation.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML from mobi String content.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromMobiFile(this Byte[] mobiBytes)
    {
        ArgumentNullException.ThrowIfNull(mobiBytes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = System.Text.Encoding.UTF8.GetString(mobiBytes);
            return content.BuildXmlFromMobiFile();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiBytes"))
        {
            throw new ApplicationException("Invalid argument: mobiBytes parameter is invalid.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Byte array could not be decoded using UTF-8 encoding.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new ApplicationException("Invalid operation occurred during byte array decoding.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new ApplicationException("The UTF-8 encoding is not supported in the current environment.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new ApplicationException("An object involved in the decoding process was disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new ApplicationException("Insufficient memory to decode the byte array.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new ApplicationException("Security error occurred during byte array decoding.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Malformed XML content detected after decoding byte array.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML from Byte array.", ex);
        }
    }

    public static String BuildXmlFromMobiFile(this FileInfo mobiFile)
    {
        ArgumentNullException.ThrowIfNull(mobiFile);
        // ----------------------------------------------------------------------------------------------------
        if (!mobiFile.Exists)
            throw new FileNotFoundException($"The specified MOBI file was not found at path: '{mobiFile.FullName}'. Please verify that the file exists and the path is correct.", mobiFile.FullName);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = File.ReadAllText(mobiFile.FullName);
            return content.BuildXmlFromMobiFile().ToString();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("path"))
        {
            throw new ApplicationException("The specified path to the MOBI file is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiFile"))
        {
            throw new ApplicationException("The MOBI file reference provided is null.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ApplicationException("The directory containing the MOBI file was not found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("The MOBI file was not found at the specified path.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ApplicationException("An I/O error occurred while accessing the MOBI file.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("The format of the MOBI file path is not supported.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Insufficient memory to read the MOBI file.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("The path to the MOBI file exceeds the system-defined maximum length.", ex);
        }
        catch (System.Security.SecurityException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("The application does not have permission to access the MOBI file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Malformed XML content detected during MOBI file processing.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Access to the MOBI file is denied due to insufficient permissions.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML from mobi FileInfo.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromMobiFile(this Stream mobiStream)
    {
        ArgumentNullException.ThrowIfNull(mobiStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            using StreamReader reader = new(mobiStream, System.Text.Encoding.UTF8, leaveOpen: true);
            String content = reader.ReadToEnd();
            return content.BuildXmlFromMobiFile();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("stream"))
        {
            throw new ApplicationException("The provided stream argument is invalid.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiStream"))
        {
            throw new ApplicationException("The input stream is null and cannot be processed.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("A decoding error occurred while reading the stream using UTF-8 encoding.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("An I/O error occurred while reading from the stream.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("The stream does not support reading.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new ApplicationException("The stream has already been disposed and cannot be accessed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Insufficient memory to process the content of the stream.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("Malformed XML content detected while processing the stream data.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("Unauthorized access detected during stream processing.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML from stream.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromMobiFile(this String mobiContent, String boundaryTag)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        ArgumentException.ThrowIfNullOrEmpty(boundaryTag);
        ArgumentException.ThrowIfNullOrWhiteSpace(boundaryTag);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement element = new(boundaryTag, new System.Xml.Linq.XElement("Content", mobiContent?.Trim() ?? String.Empty));
            return element;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("The input String 'mobiContent' is null, empty, or contains only whitespace.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundaryTag"))
        {
            throw new ApplicationException("The input String 'boundaryTag' is null, empty, or contains only whitespace.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("There is not enough memory to build the XML element with the provided content and tag.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("The boundary tag provided is not a valid XML element name.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML with custom boundary tag.", ex);
        }
    }

    public static String BuildXmlFromMobiFile(this String mobiContent, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] bytes = encoding.GetBytes(mobiContent);
            System.Xml.Linq.XDocument xml = bytes.BuildXmlFromMobiFile();
            return xml.ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("The input String 'mobiContent' is null, empty, or consists only of white-space characters.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new ApplicationException("The specified encoding is null and cannot be used to convert the content to bytes.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("The encoding fallback mechanism triggered an exception during String-to-byte conversion.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("The encoded MOBI content format is invalid and cannot be converted into a valid XML document.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new ApplicationException("There is not enough memory to convert the content to bytes or to build the XML document.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("The converted byte array could not be parsed as a valid XML structure.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("An invalid operation occurred during XML document generation from encoded String.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("Failed to build XML from encoded String.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromMobiFile(this String mobiContent, Boolean addTimestamp)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Boundary", new System.Xml.Linq.XElement("Content", mobiContent?.Trim() ?? String.Empty));

            if (addTimestamp)
                root.Add(new System.Xml.Linq.XAttribute("GeneratedOn", DateTime.UtcNow.ToString("o")));

            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("Invalid argument: mobiContent cannot be null, empty, or whitespace.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("ArgumentNullException: mobiContent must not be null.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("ArgumentOutOfRangeException: mobiContent is out of the expected range.", ex);
        }
        catch (FormatException ex)
        {
            throw new ApplicationException("FormatException: An invalid format occurred while processing mobiContent.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new ApplicationException("InvalidOperationException: The operation is not valid due to the current state of the object.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new ApplicationException("NotSupportedException: The operation is not supported in the current context.", ex);
        }
        catch (NullReferenceException ex)
        {
            throw new ApplicationException("NullReferenceException: A null reference was encountered during XML construction.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new ApplicationException("XmlException: Failed to create a valid XML document.", ex);
        }
        catch (System.Xml.Schema.XmlSchemaException ex)
        {
            throw new ApplicationException("XmlSchemaException: XML schema validation failed.", ex);
        }
        catch (System.Xml.XPath.XPathException ex)
        {
            throw new ApplicationException("XPathException: An XPath error occurred while processing the XML.", ex);
        }
        catch (System.Xml.Xsl.XsltException ex)
        {
            throw new ApplicationException("XsltException: An XSLT error occurred during transformation.", ex);
        }
        catch (IOException ex)
        {
            throw new ApplicationException("IOException: An I/O error occurred while accessing data.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new ApplicationException("SecurityException: A security error occurred during execution.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new ApplicationException("UnauthorizedAccessException: Access is denied to the resource.", ex);
        }
        catch (TypeInitializationException ex)
        {
            throw new ApplicationException("TypeInitializationException: A type initializer failed.", ex);
        }
        catch (TypeLoadException ex)
        {
            throw new ApplicationException("TypeLoadException: A type could not be loaded.", ex);
        }
        catch (TypeUnloadedException ex)
        {
            throw new ApplicationException("TypeUnloadedException: The type has been unloaded.", ex);
        }
        catch (InvalidCastException ex)
        {
            throw new ApplicationException("InvalidCastException: An invalid type cast was attempted.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new ApplicationException("OutOfMemoryException: The operation ran out of memory.", ex);
        }
        catch (StackOverflowException ex)
        {
            throw new ApplicationException("StackOverflowException: The execution stack overflowed due to excessive recursion.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while building the XML.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromMobiFile(this String mobiContent, System.Xml.XmlWriterSettings settings)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument doc = new();
            System.Xml.XmlDeclaration declaration = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(declaration);

            System.Xml.XmlElement boundary = doc.CreateElement("Boundary");
            System.Xml.XmlElement content = doc.CreateElement("Content");
            content.InnerText = mobiContent;
            boundary.AppendChild(content);

            doc.AppendChild(boundary);

            using (StringWriter stringWriter = new())
            using (System.Xml.XmlWriter xmlWriter = System.Xml.XmlWriter.Create(stringWriter, settings))
            {
                doc.WriteTo(xmlWriter);
                xmlWriter.Flush();
            }

            return doc;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("The provided mobiContent is null or empty.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("The provided mobiContent contains only white spaces.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("There was an error while creating or parsing the XML document.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new ApplicationException("An invalid operation occurred while manipulating the XML document.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ApplicationException("There was an error while writing to the XML document.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while building the XML document.", ex);
        }
    }

    public static System.Xml.Linq.XDocument BuildXmlFromMobiFile(this Dictionary<String, String> keyValuePairs)
    {
        ArgumentNullException.ThrowIfNull(keyValuePairs);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new("Boundary");

            foreach (KeyValuePair<String, String> pair in keyValuePairs)
                root.Add(new System.Xml.Linq.XElement(pair.Key, pair.Value));

            return new System.Xml.Linq.XDocument(root);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new ApplicationException("The provided key-value pairs dictionary is null.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("There was an error while building the XML structure from the key-value pairs.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("An invalid operation occurred while adding elements to the XML document.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("keyValuePairs"))
        {
            throw new ApplicationException("There was an issue with the provided key-value pairs (e.g., invalid format or empty key).", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new ApplicationException("There was an error while writing the XML document to the output stream.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while building the XML document from the key-value dictionary.", ex);
        }
    }

    public static String BuildXmlFromMobiFile(this String mobiContent, String boundaryTag, Boolean includeHash)
    {
        ArgumentException.ThrowIfNullOrEmpty(mobiContent);
        ArgumentException.ThrowIfNullOrWhiteSpace(mobiContent);
        ArgumentException.ThrowIfNullOrEmpty(boundaryTag);
        ArgumentException.ThrowIfNullOrWhiteSpace(boundaryTag);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XElement root = new(boundaryTag, new System.Xml.Linq.XElement("Content", mobiContent?.Trim() ?? String.Empty));

            if (includeHash)
            {
                String hash = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(mobiContent));
                root.Add(new System.Xml.Linq.XAttribute("Hash", hash));
            }

            return new System.Xml.Linq.XDocument(root).ToString(System.Xml.Linq.SaveOptions.DisableFormatting);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("mobiContent"))
        {
            throw new ApplicationException("The provided mobiContent is null, empty, or contains only white spaces.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("boundaryTag"))
        {
            throw new ApplicationException("The provided boundaryTag is null, empty, or contains only white spaces.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Convert"))
        {
            throw new ApplicationException("There was an error while converting the mobiContent to a Base64 string.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("There was an error while building the XML document with the provided boundaryTag and content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new ApplicationException("An invalid operation occurred while adding elements to the XML structure.", ex);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An unexpected error occurred while building the XML document with an optional hash.", ex);
        }
    }
}