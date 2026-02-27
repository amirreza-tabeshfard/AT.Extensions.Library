using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlStringExtensions
{
    private static String Synchronize(Object input)
    {
        try
        {
            if (input is null)
                throw new ArgumentNullException(nameof(input), "Input reference cannot be null.");

            var xmlContent = String.Empty;

            if (input is String s)
                xmlContent = s;
            else if (input is StringBuilder sb)
                xmlContent = sb.ToString();
            else if (input is XmlDocument xd)
                xmlContent = xd.OuterXml;
            else if (input is XDocument xdoc)
                xmlContent = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xe)
                xmlContent = xe.ToString(SaveOptions.DisableFormatting);
            else if (input is XmlNode xn)
                xmlContent = xn.OuterXml;
            else if (input is XmlReader xr)
            {
                var doc = new XmlDocument();
                doc.Load(xr);
                xmlContent = doc.OuterXml;
            }
            else if (input is MemoryStream ms)
            {
                ms.Position = 0;
                using var reader = new StreamReader(ms, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is Stream st)
            {
                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is TextReader tr)
                xmlContent = tr.ReadToEnd();
            else if (input is FileInfo fi)
                xmlContent = File.ReadAllText(fi.FullName);
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based Uri instances are supported.");
             
                xmlContent = File.ReadAllText(uri.LocalPath);
            }
            else if (input is Byte[] bytes)
                xmlContent = Encoding.UTF8.GetString(bytes);
            else if (input is Char[] chars)
                xmlContent = new String(chars);
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            if (String.IsNullOrWhiteSpace(xmlContent))
                throw new InvalidDataException("XML content is empty or whitespace.");

            var settings = new XmlReaderSettings
            {
                IgnoreComments = true,
                IgnoreProcessingInstructions = true,
                IgnoreWhitespace = true
            };

            using var stringReader = new StringReader(xmlContent);
            using var xmlReader = XmlReader.Create(stringReader, settings);
            var outputSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = false
            };

            using var stringWriter = new StringWriter();
            using var xmlWriter = XmlWriter.Create(stringWriter, outputSettings);

            while (xmlReader.Read())
            {
                xmlWriter.WriteNode(xmlReader, false);
            }

            xmlWriter.Flush();
            return stringWriter.ToString();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(input)))
        {
            throw new InvalidOperationException("An invalid argument was provided to the XML compression pipeline.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals(nameof(input)))
        {
            throw new InvalidOperationException("The XML compression input reference was null.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("mscorlib"))
        {
            throw new InvalidOperationException("Byte decoding failed while converting binary data to XML string.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The directory for the XML source file could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The XML source file could not be found.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content format is invalid and cannot be parsed.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML data stream is invalid or corrupted.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred during compression.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O error occurred while reading XML content.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System"))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.ObjectName is not null && ex.ObjectName.Equals("Stream"))
        {
            throw new InvalidOperationException("A disposed stream was accessed during XML compression.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML source was denied.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML parser encountered a structural or syntax error.", ex);
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
public static partial class CompressXmlStringExtensions
{
    public static String CompressXmlString(this String input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this StringBuilder input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this XmlDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this XDocument input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this XElement input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this XmlNode input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this XmlReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this Stream input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this MemoryStream input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this TextReader input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this FileInfo input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this Uri input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this Byte[] input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this Char[] input)
    {
        return Synchronize(input);
    }

    public static String CompressXmlString(this Object input)
    {
        return Synchronize(input);
    }
}