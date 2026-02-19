using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlNodeExtensions
{
    private static String Synchronize(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input instance cannot be null.");
            else if (input is XmlNode xmlNode)
                return NormalizeXml(xmlNode.OuterXml);
            else if (input is XmlDocument xmlDocument)
                return NormalizeXml(xmlDocument.OuterXml);
            else if (input is XElement xElement)
                return NormalizeXml(xElement.ToString(SaveOptions.DisableFormatting));
            else if (input is XDocument xDocument)
                return NormalizeXml(xDocument.ToString(SaveOptions.DisableFormatting));
            else if (input is String xmlText)
                return NormalizeXml(xmlText);
            else if (input is Stream stream)
            {
                using var reader = new StreamReader(stream, Encoding.UTF8, true, 1024, true);
                var content = reader.ReadToEnd();
                return NormalizeXml(content);
            }
            else if (input is TextReader textReader)
            {
                var content = textReader.ReadToEnd();
                return NormalizeXml(content);
            }
            else if (input is XmlReader xmlReader)
            {
                var document = new XmlDocument();
                document.Load(xmlReader);
                return NormalizeXml(document.OuterXml);
            }
            else if (input is XmlWriter)
                throw new InvalidOperationException("XmlWriter cannot be compressed because it does not expose readable XML content.");
            else if (input is FileInfo file)
            {
                if (!file.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", file.FullName);

                var content = File.ReadAllText(file.FullName, Encoding.UTF8);
                return NormalizeXml(content);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsAbsoluteUri)
                    throw new InvalidOperationException("The provided URI must be absolute.");

                var content = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
                return NormalizeXml(content);
            }
            else if (input is StringBuilder builder)
                return NormalizeXml(builder.ToString());
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input parameter was null and cannot be processed.", ex);
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xml"))
        {
            throw new InvalidOperationException("The provided XML content argument is invalid.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("The XML stream contains invalid byte sequences.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The directory of the XML file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file was not found.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content format is invalid.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation was performed.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided reference type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML stream or reader has already been disposed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML resource is denied.", ex);
        }
        catch (UriFormatException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided URI format is invalid.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content is not well-formed or is invalid.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected internal error.", ex);
        }
    }

    private static String NormalizeXml(String xml)
    {
        var document = new XmlDocument
        {
            PreserveWhitespace = false
        };
        document.LoadXml(xml);
        return document.OuterXml;
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlNodeExtensions
{
    public static String CompressXmlNode(this XmlNode node)
    {
        return Synchronize(node);
    }

    public static String CompressXmlNode(this XmlDocument document)
    {
        return Synchronize(document);
    }

    public static String CompressXmlNode(this XmlElement element)
    {
        return Synchronize(element);
    }

    public static String CompressXmlNode(this XDocument document)
    {
        return Synchronize(document);
    }

    public static String CompressXmlNode(this XElement element)
    {
        return Synchronize(element);
    }

    public static String CompressXmlNode(this String xml)
    {
        return Synchronize(xml);
    }

    public static String CompressXmlNode(this Stream stream)
    {
        return Synchronize(stream);
    }

    public static String CompressXmlNode(this MemoryStream stream)
    {
        return Synchronize(stream);
    }

    public static String CompressXmlNode(this TextReader reader)
    {
        return Synchronize(reader);
    }

    public static String CompressXmlNode(this XmlReader reader)
    {
        return Synchronize(reader);
    }

    public static String CompressXmlNode(this XmlWriter writer)
    {
        return Synchronize(writer);
    }

    public static String CompressXmlNode(this FileInfo file)
    {
        return Synchronize(file);
    }

    public static String CompressXmlNode(this Uri uri)
    {
        return Synchronize(uri);
    }

    public static String CompressXmlNode(this StringBuilder builder)
    {
        return Synchronize(builder);
    }

    public static String CompressXmlNode(this Object instance)
    {
        return Synchronize(instance);
    }
}