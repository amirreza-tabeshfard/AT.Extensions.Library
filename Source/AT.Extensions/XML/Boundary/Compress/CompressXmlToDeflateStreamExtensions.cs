using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToDeflateStreamExtensions
{
    private static Byte[] CompressInternal(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input value cannot be null.");

            var xml = String.Empty;

            if (input is String s)
                xml = s;
            else if (input is StringBuilder sb)
                xml = sb.ToString();
            else if (input is XmlDocument xd)
                xml = xd.OuterXml;
            else if (input is XDocument xdoc)
                xml = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xe)
                xml = xe.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlElement xel)
                xml = xel.OuterXml;
            else if (input is XmlNode xn)
                xml = xn.OuterXml;
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xml = doc.OuterXml;
            }
            else if (input is TextReader tr)
                xml = tr.ReadToEnd();
            else if (input is Stream stm)
            {
                if (!stm.CanRead)
                    throw new InvalidOperationException("The provided stream is not readable.");

                using var reader = new StreamReader(stm, Encoding.UTF8, true, 1024, true);
                xml = reader.ReadToEnd();
            }
            else
                throw new NotSupportedException("The provided input type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xml))
                throw new InvalidOperationException("The XML content is empty or whitespace.");

            var rawBytes = Encoding.UTF8.GetBytes(xml);

            using var output = new MemoryStream();
            using (var deflate = new DeflateStream(output, CompressionLevel.Optimal, true))
                deflate.Write(rawBytes, 0, rawBytes.Length);

            return output.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new ArgumentNullException("The input argument was null and cannot be processed for XML compression.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Character decoding failed while converting XML content to UTF-8.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Character encoding failed while converting XML content to UTF-8.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("Deflate compression failed due to an invalid compression state.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML processing failed due to an invalid XML state.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new IOException("An I/O error occurred while reading XML content or writing compressed data.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new NotSupportedException("The provided stream or reader does not support the required operation.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new ObjectDisposedException("A disposed stream was accessed during XML compression.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new UnauthorizedAccessException("Access to the XML input source was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new XmlException("The XML content is malformed or invalid and cannot be compressed.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected internal error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToDeflateStreamExtensions
{
    public static Byte[] CompressXmlToDeflateStream(this String input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this StringBuilder input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlDocument input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XDocument input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XElement input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlElement input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlNode input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlReader input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlTextReader input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlTextWriter input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this TextReader input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this MemoryStream input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this Stream input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this Object input)
    {
        return CompressInternal(input);
    }

    public static Byte[] CompressXmlToDeflateStream(this XmlDeclaration input)
    {
        return CompressInternal(input);
    }
}