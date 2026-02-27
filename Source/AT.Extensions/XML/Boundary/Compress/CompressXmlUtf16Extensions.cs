using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlUtf16Extensions
{
    private static String CompressXmlPrivate(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            String xmlString = null;

            if (input is String s)
                xmlString = s;
            else if (input is XElement xe)
                xmlString = xe.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlDocument xd)
            {
                using var sw = new StringWriter();
                xd.Save(sw);
                xmlString = sw.ToString();
            }
            else if (input is XmlNode xn)
                xmlString = xn.OuterXml;
            else if (input is Stream stream)
            {
                using var sr = new StreamReader(stream, Encoding.UTF8, true, 1024, leaveOpen: true);
                xmlString = sr.ReadToEnd();
                stream.Position = 0;
            }
            else if (input is TextReader tr)
                xmlString = tr.ReadToEnd();
            else if (input is TextWriter tw)
                xmlString = tw.ToString();
            else if (input is FileInfo fi)
            {
                using var fr = fi.OpenText();
                xmlString = fr.ReadToEnd();
            }
            else if (input is Uri uri)
            {
                using var wc = new System.Net.WebClient();
                xmlString = wc.DownloadString(uri);
            }
            else if (input is MemoryStream ms)
                xmlString = Encoding.UTF8.GetString(ms.ToArray());
            else if (input is Byte[] bytes)
                xmlString = Encoding.UTF8.GetString(bytes);
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xmlString = doc.OuterXml;
            }
            else if (input is XmlWriter xw)
                throw new NotSupportedException("Cannot compress directly from XmlWriter.");
            else if (input is StringBuilder sb)
                xmlString = sb.ToString();
            else
                throw new NotSupportedException($"Type '{input.GetType().FullName}' is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlString))
                throw new InvalidOperationException("Input XML is empty or contains only whitespace.");

            var docCompressed = XDocument.Parse(xmlString);
            return docCompressed.ToString(SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException($"XML compression failed: Input argument '{ex.ParamName}' cannot be null.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Message.Equals("Input XML is empty or contains only whitespace."))
        {
            throw new InvalidOperationException("XML compression failed: The provided XML content is empty or contains only whitespace.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.Equals("Cannot compress directly from XmlWriter."))
        {
            throw new InvalidOperationException("XML compression failed: Direct compression from XmlWriter is not supported.", ex);
        }
        catch (NotSupportedException ex) when (ex.Message.StartsWith("Type"))
        {
            throw new InvalidOperationException($"XML compression failed: Unsupported type for XML compression - {ex.Message}.", ex);
        }
        catch (XmlException ex) when (ex.Message.Contains("Data at the root level"))
        {
            throw new InvalidOperationException("XML compression failed: The XML content is not well-formed or has invalid root elements.", ex);
        }
        catch (XmlException ex) when (ex.Message.Contains("Unexpected end of file"))
        {
            throw new InvalidOperationException("XML compression failed: The XML content ended unexpectedly and is incomplete.", ex);
        }
        catch (XmlException ex)
        {
            throw new InvalidOperationException($"XML compression failed: XML parsing error occurred - {ex.Message}.", ex);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException($"XML compression failed: I/O error occurred - {ex.Message}.", ex);
        }
        catch (System.Net.WebException ex)
        {
            throw new InvalidOperationException($"XML compression failed: Failed to download XML from URI - {ex.Message}.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"XML compression failed: An unexpected error occurred - {ex.Message}.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlUtf16Extensions
{
    public static String CompressXmlUtf16(this String xml)
    {
        return CompressXmlPrivate(xml);
    }

    public static String CompressXmlUtf16(this XElement xmlElement)
    {
        return CompressXmlPrivate(xmlElement);
    }

    public static String CompressXmlUtf16(this XmlDocument xmlDoc)
    {
        return CompressXmlPrivate(xmlDoc);
    }

    public static String CompressXmlUtf16(this XmlNode xmlNode)
    {
        return CompressXmlPrivate(xmlNode);
    }

    public static String CompressXmlUtf16(this Stream xmlStream)
    {
        return CompressXmlPrivate(xmlStream);
    }

    public static String CompressXmlUtf16(this TextReader reader)
    {
        return CompressXmlPrivate(reader);
    }

    public static String CompressXmlUtf16(this TextWriter writer)
    {
        return CompressXmlPrivate(writer);
    }

    public static String CompressXmlUtf16(this FileInfo file)
    {
        return CompressXmlPrivate(file);
    }

    public static String CompressXmlUtf16(this Uri uri)
    {
        return CompressXmlPrivate(uri);
    }

    public static String CompressXmlUtf16(this MemoryStream memoryStream)
    {
        return CompressXmlPrivate(memoryStream);
    }

    public static String CompressXmlUtf16(this Byte[] byteArray)
    {
        return CompressXmlPrivate(byteArray);
    }

    public static String CompressXmlUtf16(this Object xmlObject)
    {
        return CompressXmlPrivate(xmlObject);
    }

    public static String CompressXmlUtf16(this XmlReader xmlReader)
    {
        return CompressXmlPrivate(xmlReader);
    }

    public static String CompressXmlUtf16(this XmlWriter xmlWriter)
    {
        return CompressXmlPrivate(xmlWriter);
    }

    public static String CompressXmlUtf16(this StringBuilder stringBuilder)
    {
        return CompressXmlPrivate(stringBuilder);
    }
}