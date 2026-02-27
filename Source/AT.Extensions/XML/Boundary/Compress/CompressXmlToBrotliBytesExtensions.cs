using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToBrotliBytesExtensions
{
    private static Byte[] ConvertAndCompress(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input XML reference cannot be null.");

            Byte[] rawBytes;

            if (input is String s)
                rawBytes = Encoding.UTF8.GetBytes(s);
            else if (input is XDocument xd)
                rawBytes = Encoding.UTF8.GetBytes(xd.ToString(SaveOptions.DisableFormatting));
            else if (input is XElement xe)
                rawBytes = Encoding.UTF8.GetBytes(xe.ToString(SaveOptions.DisableFormatting));
            else if (input is XmlDocument xdoc)
                rawBytes = Encoding.UTF8.GetBytes(xdoc.OuterXml);
            else if (input is XmlElement xel)
                rawBytes = Encoding.UTF8.GetBytes(xel.OuterXml);
            else if (input is XmlNode xn)
                rawBytes = Encoding.UTF8.GetBytes(xn.OuterXml);
            else if (input is MemoryStream ms)
                rawBytes = ms.ToArray();
            else if (input is Stream stream)
            {
                using var buffer = new MemoryStream();
                stream.CopyTo(buffer);
                rawBytes = buffer.ToArray();
            }
            else if (input is TextReader reader)
                rawBytes = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            else if (input is Byte[] bytes)
                rawBytes = bytes;
            else if (input is FileInfo file)
                rawBytes = File.ReadAllBytes(file.FullName);
            else if (input is Uri uri)
                rawBytes = Encoding.UTF8.GetBytes(uri.ToString());
            else if (input is StringBuilder sb)
                rawBytes = Encoding.UTF8.GetBytes(sb.ToString());
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                rawBytes = Encoding.UTF8.GetBytes(doc.OuterXml);
            }
            else
                throw new InvalidOperationException("Unsupported XML reference type for compression.");

            using var output = new MemoryStream();
            using (var brotli = new BrotliStream(output, CompressionLevel.Optimal, true))
            {
                brotli.Write(rawBytes, 0, rawBytes.Length);
            }

            return output.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input reference is null and cannot be processed for compression.", ex);
        }
        catch (ArgumentOutOfRangeException ex) when (ex.ParamName is not null && ex.ParamName.Equals("count"))
        {
            throw new InvalidOperationException("Byte range is invalid during compression operation.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("UTF-8 decoding failed due to invalid byte sequence.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("UTF-8 encoding failed due to invalid character sequence.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The specified file for compression was not found.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading data for compression.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML processing failed due to an invalid operation.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The provided stream type is not supported for compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("A stream was accessed after it was disposed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Access to the specified resource was denied during compression.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XML content is malformed or invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred during XML to Brotli compression.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToBrotliBytesExtensions
{
    public static Byte[] CompressXmlToBrotliBytes(this String xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XDocument xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XElement xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XmlDocument xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XmlElement xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XmlNode xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this Stream xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this TextReader xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this Byte[] xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this MemoryStream xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this FileInfo xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this Uri xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this StringBuilder xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this XmlReader xml)
    {
        return ConvertAndCompress(xml);
    }

    public static Byte[] CompressXmlToBrotliBytes(this Object xml)
    {
        return ConvertAndCompress(xml);
    }
}