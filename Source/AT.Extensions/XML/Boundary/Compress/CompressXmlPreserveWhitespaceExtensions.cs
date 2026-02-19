using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlPreserveWhitespaceExtensions
{
    private static String Synchronize(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");
            else if (input is String s)
                return Normalize(s);
            else if (input is XmlDocument xd)
                return Normalize(xd.OuterXml);
            else if (input is XmlNode xn)
                return Normalize(xn.OuterXml);
            else if (input is XElement xe)
                return Normalize(xe.ToString(SaveOptions.DisableFormatting));
            else if (input is XDocument xdoc)
                return Normalize(xdoc.ToString(SaveOptions.DisableFormatting));
            else if (input is StringBuilder sb)
                return Normalize(sb.ToString());
            else if (input is TextReader tr)
                return Normalize(tr.ReadToEnd());
            else if (input is MemoryStream ms)
                return Normalize(Encoding.UTF8.GetString(ms.ToArray()));
            else if (input is Stream st)
            {
                if (!st.CanRead)
                    throw new InvalidOperationException("Stream must be readable.");

                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                return Normalize(reader.ReadToEnd());
            }
            else if (input is Byte[] bytes)
                return Normalize(Encoding.UTF8.GetString(bytes));
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("XML file does not exist.", fi.FullName);

                return Normalize(File.ReadAllText(fi.FullName));
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based URIs are supported.");

                return Normalize(File.ReadAllText(uri.LocalPath));
            }
            else if (input is XmlReader xr)
            {
                using var sw = new StringWriter();
                using var xw = XmlWriter.Create(sw, new XmlWriterSettings { OmitXmlDeclaration = true });
                xw.WriteNode(xr, true);
                xw.Flush();
                return Normalize(sw.ToString());
            }
            else if (input is XmlWriter)
                throw new InvalidOperationException("XmlWriter cannot be compressed because it is a forward-only writer.");
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("xml"))
        {
            throw new InvalidOperationException("XML content is invalid or empty.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("Input reference was null during XML synchronization.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Byte to string decoding failed due to invalid encoding.", ex);
        }
        catch (EncoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("String to byte encoding failed due to invalid characters.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The XML file could not be found on the specified path.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation was performed during processing.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML content.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("AT.Extensions.XML.Boundary.Compress"))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An attempt was made to access a disposed stream or reader.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML file or stream was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("Malformed XML structure detected during parsing.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("XML compression failed due to an unexpected internal error.", ex);
        }
    }

    private static String Normalize(String xml)
    {
        if (String.IsNullOrWhiteSpace(xml))
            throw new ArgumentException("XML content cannot be null or whitespace.");

        var document = new XmlDocument
        {
            PreserveWhitespace = true
        };
        document.LoadXml(xml);

        using var stringWriter = new StringWriter();
        using var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings
        {
            Indent = false,
            NewLineHandling = NewLineHandling.None,
            OmitXmlDeclaration = true
        });

        document.Save(xmlWriter);
        xmlWriter.Flush();

        return stringWriter.ToString();
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlPreserveWhitespaceExtensions
{
    public static String CompressXmlPreserveWhitespace(this String input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XmlDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XmlNode input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XElement input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this StringBuilder input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this TextReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this Stream input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this Byte[] input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this FileInfo input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this Uri input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XmlReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this XmlWriter input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this MemoryStream input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlPreserveWhitespace(this Object input)
    {
        return Synchronize(input);
    }
}