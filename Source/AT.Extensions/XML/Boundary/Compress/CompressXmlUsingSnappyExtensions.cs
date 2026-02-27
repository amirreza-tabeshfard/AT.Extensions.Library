using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlUsingSnappyExtensions
{
    private static Byte[] ExecuteCompression(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException("Input cannot be null.");

            var xmlContent = String.Empty;

            if (input is String str)
                xmlContent = str;
            else if (input is XmlDocument xmlDoc)
                xmlContent = xmlDoc.OuterXml;
            else if (input is XDocument xDoc)
                xmlContent = xDoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlNode xmlNode)
                xmlContent = xmlNode.OuterXml;
            else if (input is XElement xElement)
                xmlContent = xElement.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlElement xmlElement)
                xmlContent = xmlElement.OuterXml;
            else if (input is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.Load(xmlReader);
                xmlContent = doc.OuterXml;
            }
            else if (input is MemoryStream memoryStream)
            {
                memoryStream.Position = 0;
                using var reader = new StreamReader(memoryStream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("Provided stream is not readable.");

                stream.Position = 0;
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is StringBuilder sb)
                xmlContent = sb.ToString();
            else if (input is TextReader textReader)
                xmlContent = textReader.ReadToEnd();
            else if (input is Byte[] buffer)
                xmlContent = Encoding.UTF8.GetString(buffer);
            else if (input is FileInfo fileInfo)
            {
                if (!fileInfo.Exists)
                    throw new FileNotFoundException("The specified file does not exist.", fileInfo.FullName);

                xmlContent = File.ReadAllText(fileInfo.FullName, Encoding.UTF8);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file URI is supported.");

                var path = uri.LocalPath;
                if (!File.Exists(path))
                    throw new FileNotFoundException("The specified file URI does not exist.", path);

                xmlContent = File.ReadAllText(path, Encoding.UTF8);
            }
            else if (input is Object obj)
                xmlContent = obj.ToString() ?? throw new InvalidOperationException("Object.ToString() returned null.");
            else
                throw new NotSupportedException("Unsupported input type for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlContent))
                throw new InvalidOperationException("XML content is empty or whitespace.");

            var xmlDocValidation = new XmlDocument();
            xmlDocValidation.LoadXml(xmlContent);

            var inputBytes = Encoding.UTF8.GetBytes(xmlContent);

            using var outputStream = new MemoryStream();
            using (var compressionStream = new DeflateStream(outputStream, CompressionLevel.Optimal, true))
            {
                compressionStream.Write(inputBytes, 0, inputBytes.Length);
            }

            return outputStream.ToArray();
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException("The provided content is not a valid XML document.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentException("The input parameter is invalid or malformed for XML compression.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentNullException("input", "The input parameter cannot be null for XML compression.");
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Failed to decode Byte array into UTF-8 XML content.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new DirectoryNotFoundException("The specified directory for the XML file was not found.", ex);
        }
        catch (FileLoadException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new FileLoadException("The XML file was found but could not be loaded into memory.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new FileNotFoundException("The specified XML file could not be found.", ex.FileName);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("The compression stream encountered invalid data during XML compression.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Object.ToString() returned null."))
        {
            throw new InvalidOperationException("The provided Object did not produce a valid XML String representation.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new IOException("An I/O error occurred while accessing the XML source.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("Unsupported input type for XML compression."))
        {
            throw new NotSupportedException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new ObjectDisposedException("Stream", "The provided stream has already been disposed before compression.");
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new OutOfMemoryException("Insufficient memory available to compress the XML content.");
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new UnauthorizedAccessException("Access to the XML file or resource was denied.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during XML compression.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlUsingSnappyExtensions
{
    public static Byte[] CompressXmlUsingSnappy(this String xml)
    {
        return ExecuteCompression(xml);
    }

    public static Byte[] CompressXmlUsingSnappy(this XmlDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlUsingSnappy(this XDocument xDocument)
    {
        return ExecuteCompression(xDocument);
    }

    public static Byte[] CompressXmlUsingSnappy(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Byte[] CompressXmlUsingSnappy(this XElement xElement)
    {
        return ExecuteCompression(xElement);
    }

    public static Byte[] CompressXmlUsingSnappy(this XmlElement xmlElement)
    {
        return ExecuteCompression(xmlElement);
    }

    public static Byte[] CompressXmlUsingSnappy(this XmlReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Byte[] CompressXmlUsingSnappy(this MemoryStream memoryStream)
    {
        return ExecuteCompression(memoryStream);
    }

    public static Byte[] CompressXmlUsingSnappy(this Stream stream)
    {
        return ExecuteCompression(stream);
    }

    public static Byte[] CompressXmlUsingSnappy(this StringBuilder stringBuilder)
    {
        return ExecuteCompression(stringBuilder);
    }

    public static Byte[] CompressXmlUsingSnappy(this TextReader textReader)
    {
        return ExecuteCompression(textReader);
    }

    public static Byte[] CompressXmlUsingSnappy(this Byte[] buffer)
    {
        return ExecuteCompression(buffer);
    }

    public static Byte[] CompressXmlUsingSnappy(this FileInfo fileInfo)
    {
        return ExecuteCompression(fileInfo);
    }

    public static Byte[] CompressXmlUsingSnappy(this Uri uri)
    {
        return ExecuteCompression(uri);
    }

    public static Byte[] CompressXmlUsingSnappy(this Object obj)
    {
        return ExecuteCompression(obj);
    }
}
