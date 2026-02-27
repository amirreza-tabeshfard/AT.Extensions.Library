using System.IO.Compression;
using System.Text;
using System.Xml;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlToStreamExtensions
{
    private static MemoryStream CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            var memoryStream = new MemoryStream();
            var xmlString = String.Empty;

            if (input is XmlDocument xmlDoc)
                xmlString = xmlDoc.OuterXml;
            else if (input is XmlNode xmlNode)
                xmlString = xmlNode.OuterXml;
            else if (input is XmlNodeList xmlNodeList)
            {
                var stringBuilder = new StringBuilder();
                foreach (XmlNode node in xmlNodeList)
                    stringBuilder.Append(node.OuterXml);
                xmlString = stringBuilder.ToString();
            }
            else if (input is XmlElement xmlElement)
                xmlString = xmlElement.OuterXml;
            else if (input is XmlAttribute xmlAttribute)
                xmlString = xmlAttribute.OuterXml;
            else if (input is String s)
                xmlString = s;
            else if (input is TextReader textReader)
                xmlString = textReader.ReadToEnd();
            else if (input is TextWriter textWriter)
            {
                textWriter.Flush();
                throw new ArgumentException("Cannot read XML from TextWriter directly.", nameof(input));
            }
            else if (input is StreamReader sr)
                xmlString = sr.ReadToEnd();
            else if (input is StreamWriter sw)
            {
                sw.Flush();
                throw new ArgumentException("Cannot read XML from StreamWriter directly.", nameof(input));
            }
            else if (input is Stream stm)
            {
                if (!stm.CanRead)
                    throw new ArgumentException("Stream is not readable.", nameof(input));

                using var reader = new StreamReader(stm, leaveOpen: true);
                xmlString = reader.ReadToEnd();
                stm.Position = 0;
            }
            else if (input is XmlReader xmlReader)
            {
                var doc = new XmlDocument();
                doc.Load(xmlReader);
                xmlString = doc.OuterXml;
            }
            else if (input is XmlWriter)
                throw new ArgumentException("Cannot read XML from XmlWriter directly.", nameof(input));
            else
                throw new ArgumentException("Unsupported input type.", nameof(input));

            var xmlBytes = Encoding.UTF8.GetBytes(xmlString);

            using (var gzip = new GZipStream(memoryStream, CompressionMode.Compress, true))
            {
                gzip.Write(xmlBytes, 0, xmlBytes.Length);
            }

            memoryStream.Position = 0;
            return memoryStream;
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("ArgumentException occurred due to invalid input parameter.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("ArgumentNullException occurred: Input cannot be null.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("IOException occurred while reading from Stream or TextReader.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("Cannot read XML from XmlWriter"))
        {
            throw new InvalidOperationException("NotSupportedException occurred: Cannot read XML from XmlWriter.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message is not null && ex.Message.Contains("Cannot read XML from StreamWriter"))
        {
            throw new InvalidOperationException("NotSupportedException occurred: Cannot read XML from StreamWriter.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("XmlException occurred while loading or parsing XML content.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message is not null && ex.Message.Contains("Stream is not readable"))
        {
            throw new InvalidOperationException("InvalidOperationException occurred: Provided Stream cannot be read.", ex);
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
public static partial class CompressXmlToStreamExtensions
{
    public static MemoryStream CompressXmlToStream(this XmlDocument xmlDoc)
    {
        return CompressInternal(xmlDoc);
    }

    public static MemoryStream CompressXmlToStream(this String xmlContent)
    {
        return CompressInternal(xmlContent);
    }

    public static MemoryStream CompressXmlToStream(this XmlNode xmlNode)
    {
        return CompressInternal(xmlNode);
    }

    public static MemoryStream CompressXmlToStream(this XmlElement xmlElement)
    {
        return CompressInternal(xmlElement);
    }

    public static MemoryStream CompressXmlToStream(this XmlAttribute xmlAttribute)
    {
        return CompressInternal(xmlAttribute);
    }

    public static MemoryStream CompressXmlToStream(this XmlNodeList xmlNodeList)
    {
        return CompressInternal(xmlNodeList);
    }

    public static MemoryStream CompressXmlToStream(this XmlReader xmlReader)
    {
        return CompressInternal(xmlReader);
    }

    public static MemoryStream CompressXmlToStream(this XmlWriter xmlWriter)
    {
        return CompressInternal(xmlWriter);
    }

    public static MemoryStream CompressXmlToStream(this StringReader stringReader)
    {
        return CompressInternal(stringReader);
    }

    public static MemoryStream CompressXmlToStream(this StringWriter stringWriter)
    {
        return CompressInternal(stringWriter);
    }

    public static MemoryStream CompressXmlToStream(this Stream stream)
    {
        return CompressInternal(stream);
    }

    public static MemoryStream CompressXmlToStream(this StreamReader streamReader)
    {
        return CompressInternal(streamReader);
    }

    public static MemoryStream CompressXmlToStream(this StreamWriter streamWriter)
    {
        return CompressInternal(streamWriter);
    }

    public static MemoryStream CompressXmlToStream(this TextReader textReader)
    {
        return CompressInternal(textReader);
    }

    public static MemoryStream CompressXmlToStream(this TextWriter textWriter)
    {
        return CompressInternal(textWriter);
    }
}