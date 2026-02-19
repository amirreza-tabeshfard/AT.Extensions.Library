using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlForStorageExtensions
{
    private static Byte[] CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            var xmlContent = string.Empty;

            if (input is string)
                xmlContent = input as string ?? throw new InvalidOperationException("String casting failed.");
            else if (input is XmlDocument)
            {
                var doc = input as XmlDocument;
                xmlContent = doc?.OuterXml ?? throw new InvalidOperationException("XmlDocument conversion failed.");
            }
            else if (input is XDocument)
            {
                var doc = input as XDocument;
                xmlContent = doc?.ToString(SaveOptions.DisableFormatting) ?? throw new InvalidOperationException("XDocument conversion failed.");
            }
            else if (input is XElement)
            {
                var element = input as XElement;
                xmlContent = element?.ToString(SaveOptions.DisableFormatting) ?? throw new InvalidOperationException("XElement conversion failed.");
            }
            else if (input is XmlNode)
            {
                var node = input as XmlNode;
                xmlContent = node?.OuterXml ?? throw new InvalidOperationException("XmlNode conversion failed.");
            }
            else if (input is XmlReader)
            {
                var reader = input as XmlReader;
                var doc = new XmlDocument();
                doc.Load(reader ?? throw new InvalidOperationException("XmlReader casting failed."));
                xmlContent = doc.OuterXml;
            }
            else if (input is TextReader)
            {
                var reader = input as TextReader;
                xmlContent = reader?.ReadToEnd() ?? throw new InvalidOperationException("TextReader conversion failed.");
            }
            else if (input is Stream)
            {
                var stream = input as Stream;
                using var reader = new StreamReader(stream ?? throw new InvalidOperationException("Stream casting failed."), Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            var inputBytes = Encoding.UTF8.GetBytes(xmlContent);

            using var outputStream = new MemoryStream();
            using (var gzip = new GZipStream(outputStream, CompressionLevel.Optimal, true))
            {
                gzip.Write(inputBytes, 0, inputBytes.Length);
            }

            return outputStream.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(input)))
        {
            throw new InvalidOperationException("Compression failed because the input reference was null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Compression failed due to invalid UTF-8 byte sequence during decoding.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Compression failed due to invalid UTF-8 character encoding.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("Compression failed because the GZip stream received invalid data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Compression failed due to an invalid XML operation.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to an I/O operation error.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Compression failed because the provided input type is not supported.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("Compression failed because the input stream was already disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("Compression failed due to insufficient memory during processing.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Compression failed due to unauthorized access to a stream resource.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Compression failed because the XML content is malformed.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Compression failed due to an unexpected error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlForStorageExtensions
{
    public static Byte[] CompressXmlForStorage(this XmlDocument value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XDocument value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XElement value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlNode value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlElement value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlText value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlCDataSection value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlComment value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlProcessingInstruction value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlAttribute value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlDeclaration value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this XmlReader value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this TextReader value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this Stream value)
    {
        return CompressInternal(value);
    }

    public static Byte[] CompressXmlForStorage(this string value)
    {
        return CompressInternal(value);
    }
}