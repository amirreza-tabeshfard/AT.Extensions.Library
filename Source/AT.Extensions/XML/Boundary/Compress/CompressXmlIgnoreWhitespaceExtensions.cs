using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlIgnoreWhitespaceExtensions
{
    private static T HandleCompression<T>(T input)
    {
        try
        {
            if (input is null)
            {
                throw new ArgumentNullException(nameof(input), "Input cannot be null for XML compression");
            }

            if (input is String str)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(str);
                RemoveWhitespaceNodes(xmlDoc);
                var stringWriter = new StringWriter();
                xmlDoc.Save(stringWriter);
                return (T)(Object)stringWriter.ToString();
            }
            else if (input is XmlDocument doc)
            {
                RemoveWhitespaceNodes(doc);
                return input;
            }
            else if (input is XDocument xDoc)
            {
                RemoveWhitespaceNodes(xDoc);
                return input;
            }
            else if (input is Stream stream)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(stream);
                RemoveWhitespaceNodes(xmlDoc);
                var memoryStream = new MemoryStream();
                xmlDoc.Save(memoryStream);
                memoryStream.Position = 0;
                return (T)(Object)memoryStream;
            }
            else if (input is TextReader reader)
            {
                var xmlDoc = new XmlDocument();
                xmlDoc.Load(reader);
                RemoveWhitespaceNodes(xmlDoc);
                var stringReader = new StringReader(xmlDoc.OuterXml);
                return (T)(Object)stringReader;
            }
            else
                throw new NotSupportedException($"Type '{input.GetType().FullName}' is not supported for XML compression");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Invalid argument value provided for XML compression.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input argument was null during XML compression.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Invalid XML operation occurred during compression.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("I/O failure occurred while processing XML streams.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Unsupported input type was used for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("A disposed stream was accessed during XML compression.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Insufficient memory while compressing XML content.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("Unauthorized access occurred during XML compression.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML detected during compression.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Unexpected error occurred during XML compression.", ex);
        }
    }

    private static void RemoveWhitespaceNodes(XmlDocument doc)
    {
        var nodes = doc.SelectNodes("//text()[normalize-space(.)='']");
     
        if (nodes != null)
            foreach (XmlNode node in nodes)
                node.ParentNode?.RemoveChild(node);
    }

    private static void RemoveWhitespaceNodes(XDocument xDoc)
    {
        foreach (var node in xDoc.DescendantNodes().OfType<XText>().Where(t => String.IsNullOrWhiteSpace(t.Value)).ToList())
            node.Remove();
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 5 )
/// </summary>
public static partial class CompressXmlIgnoreWhitespaceExtensions
{
    public static String CompressXmlIgnoreWhitespace(this String xml)
    {
        return HandleCompression(xml);
    }

    public static XmlDocument CompressXmlIgnoreWhitespace(this XmlDocument xmlDoc)
    {
        return HandleCompression(xmlDoc);
    }

    public static XDocument CompressXmlIgnoreWhitespace(this XDocument xDoc)
    {
        return HandleCompression(xDoc);
    }

    public static Stream CompressXmlIgnoreWhitespace(this Stream xmlStream)
    {
        return HandleCompression(xmlStream);
    }

    public static TextReader CompressXmlIgnoreWhitespace(this TextReader reader)
    {
        return HandleCompression(reader);
    }
}