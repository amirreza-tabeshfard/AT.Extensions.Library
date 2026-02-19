using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlForTransmissionExtensions
{
    private static Byte[] CompressInternal(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input reference cannot be null.");

            Byte[] rawData;

            if (source is String s)
                rawData = Encoding.UTF8.GetBytes(s);
            else if (source is XmlDocument xd)
                rawData = Encoding.UTF8.GetBytes(xd.OuterXml);
            else if (source is XDocument xdoc)
                rawData = Encoding.UTF8.GetBytes(xdoc.ToString(SaveOptions.DisableFormatting));
            else if (source is XElement xe)
                rawData = Encoding.UTF8.GetBytes(xe.ToString(SaveOptions.DisableFormatting));
            else if (source is TextReader tr)
                rawData = Encoding.UTF8.GetBytes(tr.ReadToEnd());
            else if (source is StringBuilder sb)
                rawData = Encoding.UTF8.GetBytes(sb.ToString());
            else if (source is Byte[] bytes)
                rawData = bytes;
            else if (source is MemoryStream ms)
                rawData = ms.ToArray();
            else if (source is Stream stream)
            {
                using var buffer = new MemoryStream();
                stream.CopyTo(buffer);
                rawData = buffer.ToArray();
            }
            else if (source is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based URIs are supported for XML compression.");

                var text = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
                rawData = Encoding.UTF8.GetBytes(text);
            }
            else if (source is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", fi.FullName);

                var text = File.ReadAllText(fi.FullName, Encoding.UTF8);
                rawData = Encoding.UTF8.GetBytes(text);
            }
            else if (source is Char[] chars)
            {
                rawData = Encoding.UTF8.GetBytes(chars);
            }
            else if (source is XmlReader xr)
            {
                var doc = XDocument.Load(xr);
                rawData = Encoding.UTF8.GetBytes(doc.ToString(SaveOptions.DisableFormatting));
            }
            else if (source is XmlNode xn)
                rawData = Encoding.UTF8.GetBytes(xn.OuterXml);
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            using var output = new MemoryStream();
            using (var gzip = new GZipStream(output, CompressionLevel.Optimal, true))
            {
                gzip.Write(rawData, 0, rawData.Length);
            }

            return output.ToArray();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An invalid argument value was provided for XML compression.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A required input reference was null during XML compression.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("UTF-8 decoding failed while processing XML content.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("UTF-8 encoding failed while processing XML content.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The XML file required for compression could not be found.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The compression stream received invalid data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An invalid operation occurred during XML compression workflow.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML data for compression.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("A required stream was disposed before XML compression completed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("Access to the XML source was denied during compression.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.ReaderWriter", StringComparison.Ordinal))
        {
            throw new InvalidOperationException("The XML content is malformed or invalid.", ex);
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
public static partial class CompressXmlForTransmissionExtensions
{
    public static Byte[] CompressXmlForTransmission(this String source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this XmlDocument source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this XDocument source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this XElement source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this Stream source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this TextReader source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this StringBuilder source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this Byte[] source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this Uri source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this FileInfo source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this MemoryStream source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this Char[] source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this XmlReader source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this XmlNode source)
    {
        return CompressInternal(source);
    }

    public static Byte[] CompressXmlForTransmission(this Object source)
    {
        return CompressInternal(source);
    }
}