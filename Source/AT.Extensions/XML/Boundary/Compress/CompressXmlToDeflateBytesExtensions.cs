using System.IO.Compression;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToDeflateBytesExtensions
{
    private static Byte[] CompressInternal(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input value cannot be null.");

            var xmlString = String.Empty;

            if (input is String s)
                xmlString = s;
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else if (input is XmlDocument xd)
                xmlString = xd.OuterXml;
            else if (input is XmlElement xe)
                xmlString = xe.OuterXml;
            else if (input is XmlNode xn)
                xmlString = xn.OuterXml;
            else if (input is XDocument xdoc)
                xmlString = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xel)
                xmlString = xel.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xmlString = doc.OuterXml;
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is MemoryStream ms)
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            else if (input is Stream st)
            {
                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xmlString = reader.ReadToEnd();
            }
            else if (input is Byte[] bytes)
                xmlString = Encoding.UTF8.GetString(bytes);
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", fi.FullName);

                xmlString = File.ReadAllText(fi.FullName, Encoding.UTF8);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based URIs are supported.");

                xmlString = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
            }
            else
                throw new NotSupportedException("The provided input type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlString))
                throw new InvalidDataException("XML content is empty or whitespace.");

            var rawBytes = Encoding.UTF8.GetBytes(xmlString);

            using var output = new MemoryStream();
            using (var deflate = new DeflateStream(output, CompressionLevel.Optimal, true))
            {
                deflate.Write(rawBytes, 0, rawBytes.Length);
            }

            return output.ToArray();
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument was null and cannot be processed.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file could not be found on disk.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML content is invalid or contains only whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.Compression"))
        {
            throw new InvalidOperationException("The compression stream encountered an invalid operation.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading or writing XML data.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("A stream or reader was accessed after being disposed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML file was denied due to insufficient permissions.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content is malformed or invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected processing error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlToDeflateBytesExtensions
{
    public static Byte[] CompressXmlToDeflateBytes(this String xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this StringBuilder xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XmlDocument xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XmlNode xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XDocument xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XElement xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XmlReader xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this Stream xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this MemoryStream xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this TextReader xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this Byte[] xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this FileInfo xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this Uri xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this Object xml)
    {
        return CompressInternal(xml);
    }

    public static Byte[] CompressXmlToDeflateBytes(this XmlElement xml)
    {
        return CompressInternal(xml);
    }
}