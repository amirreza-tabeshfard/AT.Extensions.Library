using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlDocumentExtensions
{
    private static XmlDocument Synchronize(Object source)
    {
        try
        {
            var document = new XmlDocument
            {
                PreserveWhitespace = false
            };

            if (source is XmlDocument)
            {
                var d = source as XmlDocument;
                document.LoadXml(d!.OuterXml);
            }
            else if (source is XDocument)
            {
                var d = source as XDocument;
                var xml = d!.ToString(SaveOptions.DisableFormatting);
                document.LoadXml(xml);
            }
            else if (source is XElement)
            {
                var e = source as XElement;
                var xml = e!.ToString(SaveOptions.DisableFormatting);
                document.LoadXml(xml);
            }
            else if (source is String)
            {
                var s = source as String;
                document.LoadXml(s!);
            }
            else if (source is FileInfo)
            {
                var f = source as FileInfo;
                using var stream = f!.OpenRead();
                document.Load(stream);
            }
            else if (source is Uri)
            {
                var u = source as Uri;
                document.Load(u!.AbsoluteUri);
            }
            else if (source is Byte[])
            {
                var b = source as Byte[];
                using var ms = new MemoryStream(b!);
                document.Load(ms);
            }
            else if (source is MemoryStream)
            {
                var ms = source as MemoryStream;
                ms!.Position = 0;
                document.Load(ms);
            }
            else if (source is Stream)
            {
                var st = source as Stream;
                document.Load(st!);
            }
            else if (source is TextReader)
            {
                var tr = source as TextReader;
                document.Load(tr!);
            }
            else if (source is XmlReader)
            {
                var xr = source as XmlReader;
                document.Load(xr!);
            }
            else if (source is XmlElement)
            {
                var xe = source as XmlElement;
                document.LoadXml(xe!.OuterXml);
            }
            else if (source is XmlNode)
            {
                var xn = source as XmlNode;
                document.LoadXml(xn!.OuterXml);
            }
            else if (source is StringBuilder)
            {
                var sb = source as StringBuilder;
                document.LoadXml(sb!.ToString());
            }
            else
                throw new NotSupportedException("The provided source type is not supported for XML compression.");

            using var sw = new StringWriter();
            var settings = new XmlWriterSettings
            {
                Indent = false,
                NewLineHandling = NewLineHandling.None,
                OmitXmlDeclaration = false
            };

            using (var writer = XmlWriter.Create(sw, settings))
            {
                document.Save(writer);
            }

            var compressed = sw.ToString();
            var result = new XmlDocument
            {
                PreserveWhitespace = false
            };
            result.LoadXml(compressed);
            return result;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The provided source argument is invalid or malformed.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("source"))
        {
            throw new InvalidOperationException("The provided source is null. A valid XML input is required.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The directory for the specified XML file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file could not be located.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred during compression.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading the XML source.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML format or source type is not supported by the compression algorithm.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The underlying stream or reader was disposed before XML processing completed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML source was denied due to insufficient permissions.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content is not well-formed or contains invalid characters.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected runtime error.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlDocumentExtensions
{
    public static XmlDocument CompressXmlDocument(this XmlDocument source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "XmlDocument source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XDocument source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "XDocument source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XElement source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "XElement source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this String source)
    {
        if (String.IsNullOrWhiteSpace(source)) 
            throw new ArgumentException("String source cannot be null or empty.", nameof(source));

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this Stream source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "Stream source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this TextReader source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "TextReader source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this FileInfo source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "FileInfo source cannot be null.");

        if (!source.Exists) 
            throw new FileNotFoundException("The specified XML file does not exist.", source.FullName);

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this Uri source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "Uri source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this Byte[] source)
    {
        if (source == null || source.Length == 0) 
            throw new ArgumentException("Byte array source cannot be null or empty.", nameof(source));

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XmlReader source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source), "XmlReader source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XmlNode source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "XmlNode source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XmlElement source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "XmlElement source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this MemoryStream source)
    {
        if (source == null) 
            throw new ArgumentNullException(nameof(source), "MemoryStream source cannot be null.");

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this StringBuilder source)
    {
        if (source == null || source.Length == 0)
            throw new ArgumentException("StringBuilder source cannot be null or empty.", nameof(source));

        return Synchronize(source);
    }

    public static XmlDocument CompressXmlDocument(this XmlTextReader source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source), "XmlTextReader source cannot be null.");

        return Synchronize(source);
    }
}