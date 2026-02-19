using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlElementExtensions
{
    private static XmlDocument Synchronize(Object source)
    {
        try
        {
            if (source is null)
                throw new ArgumentNullException(nameof(source), "Input source cannot be null.");

            var xml = string.Empty;

            if (source is XmlDocument doc)
                xml = doc.OuterXml;
            else if (source is XmlElement element)
                xml = element.OuterXml;
            else if (source is XmlNode node)
                xml = node.OuterXml;
            else if (source is XDocument xdoc)
                xml = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (source is XElement xel)
                xml = xel.ToString(SaveOptions.DisableFormatting);
            else if (source is string text)
                xml = text;
            else if (source is MemoryStream ms)
            {
                if (!ms.CanRead)
                    throw new InvalidOperationException("The provided MemoryStream is not readable.");

                ms.Position = 0;
                using var reader = new StreamReader(ms, Encoding.UTF8, true, 1024, true);
                xml = reader.ReadToEnd();
            }
            else if (source is Stream stream)
            {
                if (!stream.CanRead)
                    throw new InvalidOperationException("The provided Stream is not readable.");

                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                xml = reader.ReadToEnd();
            }
            else if (source is TextReader tr)
                xml = tr.ReadToEnd();
            else if (source is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", fi.FullName);

                xml = File.ReadAllText(fi.FullName, Encoding.UTF8);
            }
            else if (source is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based Uri sources are supported.");

                var path = uri.LocalPath;
                if (!File.Exists(path))
                    throw new FileNotFoundException("The XML file specified by the Uri does not exist.", path);

                xml = File.ReadAllText(path, Encoding.UTF8);
            }
            else if (source is byte[] bytes)
            {
                if (bytes.Length == 0)
                    throw new InvalidDataException("The byte array is empty.");

                xml = Encoding.UTF8.GetString(bytes);
            }
            else if (source is StringBuilder sb)
                xml = sb.ToString();
            else if (source is XmlReader xr)
            {
                var temp = new XmlDocument();
                temp.Load(xr);
                xml = temp.OuterXml;
            }
            else if (source is XmlDocumentFragment frag)
                xml = frag.OuterXml;
            else
                throw new NotSupportedException("The provided source type is not supported for XML compression.");

            if (string.IsNullOrWhiteSpace(xml))
                throw new InvalidDataException("The XML content is empty or whitespace.");

            var result = new XmlDocument
            {
                PreserveWhitespace = false
            };

            result.LoadXml(xml);

            using var sw = new StringWriter();
            using (var xw = XmlWriter.Create(
                sw,
                new XmlWriterSettings
                {
                    Indent = false,
                    OmitXmlDeclaration = false
                }))
            {
                result.Save(xw);
            }

            var normalized = sw.ToString();

            var finalDoc = new XmlDocument
            {
                PreserveWhitespace = false
            };

            finalDoc.LoadXml(normalized);
            return finalDoc;
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The input source argument is null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("Byte to UTF8 String conversion failed due to invalid encoding.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.File"))
        {
            throw new InvalidOperationException("The specified XML file could not be found on disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML content.", ex);
        }
        catch (InvalidDataException ex) when (ex.Message.Equals("The XML content is empty or whitespace."))
        {
            throw new InvalidOperationException("The provided XML content is empty or contains only whitespace.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Only file-based Uri sources are supported."))
        {
            throw new InvalidOperationException("The provided Uri is not file-based and cannot be processed.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("The provided source type is not supported for XML compression."))
        {
            throw new InvalidOperationException("The input source type is not supported by the XML compression algorithm.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The underlying stream or reader has already been disposed.", ex);
        }
        catch (UriFormatException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided Uri format is invalid.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content is malformed or cannot be parsed.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML synchronization and compression failed due to an unexpected error: " + ex.Message, ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlElementExtensions
{
    public static XmlDocument CompressXmlElement(this XmlDocument source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XmlElement source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XmlNode source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XDocument source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XElement source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this String source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this Stream source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this MemoryStream source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this TextReader source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this FileInfo source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this Uri source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this Byte[] source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this StringBuilder source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XmlReader source)
    {
        return Synchronize(source);
    }

    public static XmlDocument CompressXmlElement(this XmlDocumentFragment source)
    {
        return Synchronize(source);
    }
}