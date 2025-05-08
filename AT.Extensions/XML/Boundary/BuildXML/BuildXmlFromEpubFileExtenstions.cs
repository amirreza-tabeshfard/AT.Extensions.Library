namespace AT.Extensions.XML.Boundary.BuildXML;
public static class BuildXmlFromEpubFileExtenstions
    : Object
{
    public static System.Xml.Linq.XDocument BuildXmlFromEpubFile(this String epubPath)
    {
        ArgumentException.ThrowIfNullOrEmpty(epubPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(epubPath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String xmlContent = File.ReadAllText(epubPath);
            System.Xml.Linq.XDocument xmlDoc = System.Xml.Linq.XDocument.Parse(xmlContent);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new ArgumentNullException("The EPUB file path cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new ArgumentException("The EPUB file path is invalid or empty.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(epubPath))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new DirectoryNotFoundException("The directory containing the EPUB file does not exist.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new UnauthorizedAccessException("Access to the EPUB file is denied.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new PathTooLongException("The EPUB file path exceeds the maximum length allowed by the system.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new NotSupportedException("The EPUB file path format is not supported.", ex);
        }
        catch (IOException ex)
        {
            throw new IOException("An I/O error occurred while reading the EPUB file.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Data at the root level is invalid"))
        {
            throw new System.Xml.XmlException("The EPUB file does not contain valid XML content.", ex);
        }
        catch (System.Xml.XmlException ex) when (ex.Message.Contains("Unexpected end of file"))
        {
            throw new System.Xml.XmlException("The EPUB file is corrupted or incomplete.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new System.Xml.XmlException("An error occurred while parsing the XML content of the EPUB file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file.", ex);
        }
    }

    public static System.Xml.XmlDocument BuildXmlFromEpubFile(this String epubPath, Boolean preserveWhitespace)
    {
        ArgumentException.ThrowIfNullOrEmpty(epubPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(epubPath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.XmlDocument xmlDoc = new()
            {
                PreserveWhitespace = preserveWhitespace
            };
            xmlDoc.Load(epubPath);
            return xmlDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new ArgumentNullException("The EPUB file path cannot be null.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new ArgumentException("The EPUB file path is invalid or empty.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(epubPath))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message.Contains("Access to the path"))
        {
            throw new InvalidOperationException($"Access to the EPUB file is denied. Ensure the file is accessible: {epubPath}.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The EPUB file is not a valid XML document.", ex);
        }
        catch (IOException ex) when (ex.Message.Contains("The process cannot access the file"))
        {
            throw new InvalidOperationException("The EPUB file is being used by another process, making it inaccessible.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file as XmlDocument.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file.", ex);
        }
    }

    public static String BuildXmlFromEpubFile(this String epubPath, System.Text.Encoding encoding)
    {
        ArgumentException.ThrowIfNullOrEmpty(epubPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(epubPath);
        ArgumentNullException.ThrowIfNull(encoding);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return File.ReadAllText(epubPath, encoding);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new InvalidOperationException("Invalid EPUB file path specified.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new InvalidOperationException("EPUB file path is either null or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("encoding"))
        {
            throw new InvalidOperationException("Encoding cannot be null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(epubPath))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Permission denied to access the EPUB file.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("I/O error occurred while reading the EPUB file.", ex);
        }
        catch (FormatException ex)
        {
            throw new InvalidOperationException("The EPUB file format is invalid or corrupted.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file.", ex);
        }
    }

    public static Boolean BuildXmlFromEpubFile(this String epubPath, out System.Xml.Linq.XDocument? xmlDoc)
    {
        xmlDoc = default;
        // ----------------------------------------------------------------------------------------------------
        if (String.IsNullOrEmpty(epubPath) || String.IsNullOrWhiteSpace(epubPath))
            return false;

        if (!File.Exists(epubPath))
            return false;
        // ----------------------------------------------------------------------------------------------------
        try
        {
            xmlDoc = System.Xml.Linq.XDocument.Load(epubPath);
            return true;
        }
        catch
        {
            return false;
        }
    }

    public static MemoryStream BuildXmlFromEpubFile(this String epubPath, Boolean compress, Int32 bufferSize)
    {
        ArgumentException.ThrowIfNullOrEmpty(epubPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(epubPath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            Byte[] data = File.ReadAllBytes(epubPath);
            using MemoryStream stream = new();

            if (compress)
            {
                using System.IO.Compression.GZipStream gzip = new(stream, System.IO.Compression.CompressionMode.Compress, true);
                gzip.Write(data, 0, data.Length);
            }
            else
                stream.Write(data, 0, Math.Min(bufferSize, data.Length));

            stream.Position = 0;
            return stream;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new InvalidOperationException("EPUB file path is invalid or empty.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(epubPath))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An error occurred while reading the EPUB file.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the EPUB file is denied.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("Insufficient memory to process the EPUB file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while converting the EPUB file to stream.", ex);
        }
    }

    public static FileStream BuildXmlFromEpubFile(this String epubPath, FileMode mode)
    {
        ArgumentException.ThrowIfNullOrEmpty(epubPath);
        ArgumentException.ThrowIfNullOrWhiteSpace(epubPath);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            return new FileStream(epubPath, mode);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubPath"))
        {
            throw new InvalidOperationException("EPUB file path is invalid or empty.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Equals(epubPath))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && !File.Exists(ex.FileName))
        {
            throw new FileNotFoundException("The specified EPUB file could not be found.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the EPUB file is denied.", ex);
        }
        catch (PathTooLongException ex)
        {
            throw new InvalidOperationException("The path to the EPUB file is too long.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An error occurred while opening the EPUB file.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The file path format is not supported.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("You do not have the required permissions to access the EPUB file.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file.", ex);
        }
    }

    public static System.Xml.Linq.XElement BuildXmlFromEpubFile(this Stream epubStream)
    {
        ArgumentNullException.ThrowIfNull(epubStream);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            System.Xml.Linq.XDocument doc = System.Xml.Linq.XDocument.Load(epubStream);
            return doc.Root;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubStream"))
        {
            throw new InvalidOperationException("EPUB stream is null.", ex);
        }
        catch (System.Xml.XmlException ex)
        {
            throw new InvalidOperationException("The EPUB stream is not a valid XML document.", ex);
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException("The EPUB stream cannot be processed as an XML document.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("An error occurred while reading the EPUB stream.", ex);
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new InvalidOperationException("Access to the EPUB stream is denied.", ex);
        }
        catch (System.Security.SecurityException ex)
        {
            throw new InvalidOperationException("Security error while accessing the EPUB stream.", ex);
        }
        catch (NotSupportedException ex)
        {
            throw new InvalidOperationException("The EPUB stream format is not supported.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB stream.", ex);
        }
    }

    public static String BuildXmlFromEpubFile(this Byte[] epubBytes)
    {
        ArgumentNullException.ThrowIfNull(epubBytes);
        // ----------------------------------------------------------------------------------------------------
        try
        {
            String content = System.Text.Encoding.UTF8.GetString(epubBytes);
            return content;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubBytes"))
        {
            throw new InvalidOperationException("The provided EPUB byte array is null.", ex);
        }
        catch (System.Text.DecoderFallbackException ex) when (ex.Data != null && ex.Data.Contains("Encoding"))
        {
            throw new InvalidOperationException("Error occurred while decoding the EPUB bytes to a string due to an unsupported character encoding.", ex);
        }
        catch (FormatException ex) when (ex.Message.Contains("invalid character"))
        {
            throw new InvalidOperationException("The EPUB byte array has an invalid character format.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("epubBytes"))
        {
            throw new InvalidOperationException("The EPUB byte array length is out of valid range.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Contains("Error while converting EPUB bytes to String"))
        {
            throw new InvalidOperationException("General error occurred while converting EPUB bytes to string.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred while processing the EPUB file.", ex);
        }
    }
}