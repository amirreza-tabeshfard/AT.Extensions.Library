using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlUsingZstdExtensions
{
    private static Byte[] ExecuteCompression(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException("Input cannot be null.");

            var xmlString = String.Empty;

            if (input is String s)
            {
                if (String.IsNullOrWhiteSpace(s))
                    throw new ArgumentException("Input XML String cannot be empty or whitespace.");
                xmlString = s;
            }
            else if (input is StringBuilder sb)
            {
                if (sb.Length == 0)
                    throw new ArgumentException("StringBuilder XML content cannot be empty.");
                xmlString = sb.ToString();
            }
            else if (input is XmlDocument xd)
                xmlString = xd.OuterXml;
            else if (input is XDocument xdoc)
                xmlString = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xel)
                xmlString = xel.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlNode node)
                xmlString = node.OuterXml;
            else if (input is XmlReader reader)
            {
                var doc = new XmlDocument();
                doc.Load(reader);
                xmlString = doc.OuterXml;
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("The provided stream is not readable.");
                
                using var sr = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xmlString = sr.ReadToEnd();
            }
            else if (input is Byte[] bytes)
            {
                if (bytes.Length == 0)
                    throw new ArgumentException("Byte array cannot be empty.");
                xmlString = Encoding.UTF8.GetString(bytes);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based URIs are supported.");
                xmlString = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
            }
            else if (input is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException("Specified XML file does not exist.");
                xmlString = File.ReadAllText(file.FullName, Encoding.UTF8);
            }
            else if (input is DirectoryInfo dir)
            {
                if (!dir.Exists)
                    throw new DirectoryNotFoundException("Specified directory does not exist.");
                throw new InvalidOperationException("Directory input is not supported for XML compression.");
            }
            else if (input is Object obj)
            {
                xmlString = obj.ToString();
                if (String.IsNullOrWhiteSpace(xmlString))
                    throw new InvalidOperationException("Object String representation resulted in empty XML content.");
            }
            else
                throw new NotSupportedException("Unsupported input type for XML compression.");

            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xmlString);

            var normalizedXml = xmlDoc.OuterXml;
            var xmlBytes = Encoding.UTF8.GetBytes(normalizedXml);

            using var outputStream = new MemoryStream();
            using (var zstdStream = new ZLibStream(outputStream, CompressionLevel.SmallestSize, true))
            {
                zstdStream.Write(xmlBytes, 0, xmlBytes.Length);
            }
            return outputStream.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input parameter is invalid for XML compression processing.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("bytes"))
        {
            throw new InvalidOperationException("The provided Byte array parameter is invalid for XML compression.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("s"))
        {
            throw new InvalidOperationException("The provided XML String parameter is invalid or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument cannot be null for XML compression.", ex);
        }
        catch (DecoderFallbackException ex)
        {
            throw new InvalidOperationException("The Byte sequence could not be decoded as valid UTF-8 XML content.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified directory for XML input was not found in the file system.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file could not be located on disk.", ex);
        }
        catch (InvalidDataException ex)
        {
            throw new InvalidOperationException("The compressed stream encountered invalid data during processing.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML reader encountered an invalid operation while loading XML content.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML content from the file system.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("The compression stream does not support the requested compression operation.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("The provided stream has already been disposed before XML compression.", ex);
        }
        catch (OutOfMemoryException ex)
        {
            throw new InvalidOperationException("The system ran out of memory while processing or compressing the XML content.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the specified XML file or directory was denied by the operating system.", ex);
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException("The XML content is malformed or violates XML syntax rules.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected error occurred during XML compression using Zstandard algorithm.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlUsingZstdExtensions
{
    public static Byte[] CompressXmlUsingZstd(this String xml)
    {
        return ExecuteCompression(xml);
    }

    public static Byte[] CompressXmlUsingZstd(this StringBuilder xmlBuilder)
    {
        return ExecuteCompression(xmlBuilder);
    }

    public static Byte[] CompressXmlUsingZstd(this XmlDocument xmlDocument)
    {
        return ExecuteCompression(xmlDocument);
    }

    public static Byte[] CompressXmlUsingZstd(this XDocument xDocument)
    {
        return ExecuteCompression(xDocument);
    }

    public static Byte[] CompressXmlUsingZstd(this XElement xElement)
    {
        return ExecuteCompression(xElement);
    }

    public static Byte[] CompressXmlUsingZstd(this XmlNode xmlNode)
    {
        return ExecuteCompression(xmlNode);
    }

    public static Byte[] CompressXmlUsingZstd(this XmlReader xmlReader)
    {
        return ExecuteCompression(xmlReader);
    }

    public static Byte[] CompressXmlUsingZstd(this TextReader textReader)
    {
        return ExecuteCompression(textReader);
    }

    public static Byte[] CompressXmlUsingZstd(this Stream stream)
    {
        return ExecuteCompression(stream);
    }

    public static Byte[] CompressXmlUsingZstd(this MemoryStream memoryStream)
    {
        return ExecuteCompression(memoryStream);
    }

    public static Byte[] CompressXmlUsingZstd(this Byte[] xmlBytes)
    {
        return ExecuteCompression(xmlBytes);
    }

    public static Byte[] CompressXmlUsingZstd(this Uri uri)
    {
        return ExecuteCompression(uri);
    }

    public static Byte[] CompressXmlUsingZstd(this FileInfo fileInfo)
    {
        return ExecuteCompression(fileInfo);
    }

    public static Byte[] CompressXmlUsingZstd(this DirectoryInfo directoryInfo)
    {
        return ExecuteCompression(directoryInfo);
    }

    public static Byte[] CompressXmlUsingZstd(this Object xmlObject)
    {
        return ExecuteCompression(xmlObject);
    }
}