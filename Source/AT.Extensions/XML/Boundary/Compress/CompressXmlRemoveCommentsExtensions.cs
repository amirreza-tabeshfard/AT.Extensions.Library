using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlRemoveCommentsExtensions
{
    private static String Process(Object input)
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
            else if (input is TextReader tr)
                xmlContent = tr.ReadToEnd();
            else if (input is MemoryStream ms)
                xmlContent = Encoding.UTF8.GetString(ms.ToArray());
            else if (input is Stream st)
            {
                using var reader = new StreamReader(st, Encoding.UTF8, true, 1024, true);
                xmlContent = reader.ReadToEnd();
            }
            else if (input is Byte[] bytes)
                xmlContent = Encoding.UTF8.GetString(bytes);
            else if (input is Char[] chars)
                xmlContent = new String(chars);
            else if (input is FileInfo fi)
            {
                if (!fi.Exists)
                    throw new FileNotFoundException("The specified XML file does not exist.", fi.FullName);
             
                xmlContent = File.ReadAllText(fi.FullName, Encoding.UTF8);
            }
            else if (input is Uri uri)
            {
                if (!uri.IsFile)
                    throw new InvalidOperationException("Only file-based URIs are supported for XML processing.");
                
                xmlContent = File.ReadAllText(uri.LocalPath, Encoding.UTF8);
            }
            else if (input is XmlDocument xd)
                xmlContent = xd.OuterXml;
            else if (input is XmlNode xn)
                xmlContent = xn.OuterXml;
            else if (input is XmlReader xr)
            {
                var doc = XDocument.Load(xr);
                xmlContent = doc.ToString(SaveOptions.DisableFormatting);
            }
            else if (input is XDocument xdoc)
                xmlContent = xdoc.ToString(SaveOptions.DisableFormatting);
            else if (input is XElement xe)
                xmlContent = xe.ToString(SaveOptions.DisableFormatting);
            else
                throw new NotSupportedException("The provided reference type is not supported for XML compression.");

            var document = XDocument.Parse(xmlContent, LoadOptions.None);

            document.DescendantNodes().Where(n => n.NodeType == XmlNodeType.Comment).Remove();

            return document.ToString(SaveOptions.DisableFormatting);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The input argument was null and cannot be processed.", ex);
        }
        catch (DecoderFallbackException ex) when (ex.Source is not null && ex.Source.Equals("System.Text.Encoding"))
        {
            throw new InvalidOperationException("The byte sequence could not be decoded using UTF-8 encoding.", ex);
        }
        catch (DirectoryNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The directory specified for the XML source could not be found.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The XML file specified for processing was not found.", ex);
        }
        catch (FormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content has an invalid format.", ex);
        }
        catch (InvalidDataException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML data stream contains invalid data.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.Linq"))
        {
            throw new InvalidOperationException("An invalid XML operation occurred during document processing.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("An I/O failure occurred while accessing XML content.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The provided input type is not supported for XML compression.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.IO"))
        {
            throw new InvalidOperationException("The XML source was accessed after it had been disposed.", ex);
        }
        catch (OutOfMemoryException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("Insufficient memory was available to process the XML document.", ex);
        }
        catch (PathTooLongException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The file path of the XML source exceeds the supported length.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML source was denied due to insufficient permissions.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml"))
        {
            throw new InvalidOperationException("The XML content is malformed or contains invalid syntax.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected failure occurred during XML compression processing.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlRemoveCommentsExtensions
{
    public static String CompressXmlRemoveComments(this String input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this StringBuilder input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this TextReader input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this Stream input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this MemoryStream input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this Byte[] input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this Char[] input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this XmlDocument input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this XmlNode input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this XmlReader input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this XDocument input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this XElement input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this FileInfo input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this Uri input)
    {
        return Process(input);
    }

    public static String CompressXmlRemoveComments(this Object input)
    {
        return Process(input);
    }
}