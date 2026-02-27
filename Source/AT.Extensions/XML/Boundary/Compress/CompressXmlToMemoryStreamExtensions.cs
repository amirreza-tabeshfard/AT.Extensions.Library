using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToMemoryStreamExtensions
{
    private static MemoryStream PerformCompression(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null");

            var memoryStream = new MemoryStream();

            if (input is String xmlString)
            {
                var bytes = Encoding.UTF8.GetBytes(xmlString);
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else if (input is XmlDocument xmlDoc)
            {
                using var stringWriter = new StringWriter();
                xmlDoc.Save(stringWriter);
                var bytes = Encoding.UTF8.GetBytes(stringWriter.ToString());
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else if (input is Stream stream)
            {
                stream.Position = 0;
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    stream.CopyTo(gzip);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else if (input is StringBuilder sb)
            {
                var bytes = Encoding.UTF8.GetBytes(sb.ToString());
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(bytes, 0, bytes.Length);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else if (input is Byte[] byteArray)
            {
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    gzip.Write(byteArray, 0, byteArray.Length);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else if (input is FileInfo fileInfo)
            {
                if (fileInfo.Exists == false)
                    throw new FileNotFoundException("File not found", fileInfo.FullName);

                using (var fileStream = fileInfo.OpenRead())
                using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
                {
                    fileStream.CopyTo(gzip);
                }
                memoryStream.Position = 0;
                return memoryStream;
            }
            else
                throw new NotSupportedException($"Compression for type {input.GetType().FullName} is not supported");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"Compression failed: The input parameter cannot be null. ParamName={ex.ParamName}", ex);
        }
        catch (FileNotFoundException ex) when (ex.FileName is not null && ex.FileName.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: The specified file was not found. FileName={ex.FileName}", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("Compression for type"))
        {
            throw new InvalidOperationException($"Compression failed: Unsupported input type. {ex.Message}", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Length > 0)
        {
            throw new InvalidOperationException($"Compression failed: IO error occurred during stream operations. Source={ex.Source}", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Message is not null && ex.Message.Contains("Access"))
        {
            throw new InvalidOperationException($"Compression failed: Access denied to the file or resource. {ex.Message}", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Message is not null && ex.Message.Contains("UTF8"))
        {
            throw new InvalidOperationException($"Compression failed: UTF8 encoding conversion failed. {ex.Message}", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message is not null && ex.Message.Contains("GZipStream"))
        {
            throw new InvalidOperationException($"Compression failed: GZipStream encountered an invalid operation. {ex.Message}", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Compression failed: An unexpected error occurred. {ex.Message}", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 14 )
/// </summary>
public static partial class CompressXmlToMemoryStreamExtensions
{
    public static MemoryStream CompressXmlToMemoryStream(this String xmlContent)
    {
        return PerformCompression(xmlContent);
    }

    public static MemoryStream CompressXmlToMemoryStream(this XmlDocument xmlDocument)
    {
        return PerformCompression(xmlDocument);
    }

    public static MemoryStream CompressXmlToMemoryStream(this XmlElement xmlElement)
    {
        return PerformCompression(xmlElement);
    }

    public static MemoryStream CompressXmlToMemoryStream(this Stream xmlStream)
    {
        return PerformCompression(xmlStream);
    }

    public static MemoryStream CompressXmlToMemoryStream(this TextReader textReader)
    {
        return PerformCompression(textReader);
    }

    public static MemoryStream CompressXmlToMemoryStream(this StringBuilder stringBuilder)
    {
        return PerformCompression(stringBuilder);
    }

    public static MemoryStream CompressXmlToMemoryStream(this Byte[] xmlBytes)
    {
        return PerformCompression(xmlBytes);
    }

    public static MemoryStream CompressXmlToMemoryStream(this XmlReader xmlReader)
    {
        return PerformCompression(xmlReader);
    }

    public static MemoryStream CompressXmlToMemoryStream(this FileInfo xmlFile)
    {
        return PerformCompression(xmlFile);
    }

    public static MemoryStream CompressXmlToMemoryStream(this Uri xmlUri)
    {
        return PerformCompression(xmlUri);
    }

    public static MemoryStream CompressXmlToMemoryStream(this Object xmlObject)
    {
        return PerformCompression(xmlObject);
    }

    public static MemoryStream CompressXmlToMemoryStream(this TextWriter textWriter)
    {
        return PerformCompression(textWriter);
    }

    public static MemoryStream CompressXmlToMemoryStream(this XmlNode xmlNode)
    {
        return PerformCompression(xmlNode);
    }

    public static MemoryStream CompressXmlToMemoryStream(this Object[] xmlObjects)
    {
        return PerformCompression(xmlObjects);
    }
}