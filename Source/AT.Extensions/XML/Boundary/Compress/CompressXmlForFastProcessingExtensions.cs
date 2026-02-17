using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace AT.Extensions.XML.Boundary.Compress;
/// <summary>
/// Private Mathod(s)
/// </summary>
public static partial class CompressXmlForFastProcessingExtensions
{
    private static String CompressInternal(Object input)
    {
        try
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "Input cannot be null.");

            var readerSettings = new XmlReaderSettings
            {
                IgnoreWhitespace = true,
                DtdProcessing = DtdProcessing.Ignore
            };

            XmlReader? reader = null;

            if (input is String)
            {
                var text = input as String;
                
                if (String.IsNullOrWhiteSpace(text))
                    throw new ArgumentException("Input String is empty or whitespace.");
                
                reader = XmlReader.Create(new StringReader(text), readerSettings);
            }
            else if (input is XDocument)
            {
                var doc = input as XDocument;
                reader = doc.CreateReader();
            }
            else if (input is XElement)
            {
                var el = input as XElement;
                reader = el.CreateReader();
            }
            else if (input is XmlDocument)
            {
                var doc = input as XmlDocument;
                reader = new XmlNodeReader(doc);
            }
            else if (input is XmlElement)
            {
                var el = input as XmlElement;
                reader = new XmlNodeReader(el);
            }
            else if (input is XmlNode)
            {
                var node = input as XmlNode;
                reader = new XmlNodeReader(node);
            }
            else if (input is Stream)
            {
                var stream = input as Stream;
                
                if (!stream.CanRead)
                    throw new InvalidOperationException("Stream is not readable.");
                
                reader = XmlReader.Create(stream, readerSettings);
            }
            else if (input is TextReader)
            {
                var tr = input as TextReader;
                reader = XmlReader.Create(tr, readerSettings);
            }
            else if (input is FileInfo)
            {
                var fi = input as FileInfo;
                
                if (!fi.Exists)
                    throw new FileNotFoundException("XML file was not found.", fi.FullName);
                
                reader = XmlReader.Create(fi.FullName, readerSettings);
            }
            else if (input is Uri)
            {
                var uri = input as Uri;
                reader = XmlReader.Create(uri.AbsoluteUri, readerSettings);
            }
            else if (input is StringBuilder)
            {
                var sb = input as StringBuilder;
                
                if (sb.Length == 0)
                    throw new ArgumentException("StringBuilder is empty.");
                
                reader = XmlReader.Create(new StringReader(sb.ToString()), readerSettings);
            }
            else if (input is XmlReader)
            {
                reader = input as XmlReader;
            }
            else if (input is TextWriter)
            {
                throw new InvalidOperationException("TextWriter cannot be used as an XML input source.");
            }
            else if (input is Byte[])
            {
                var bytes = input as Byte[];
                
                if (bytes.Length == 0)
                    throw new ArgumentException("Byte array is empty.");
                
                reader = XmlReader.Create(new MemoryStream(bytes), readerSettings);
            }
            else
                throw new NotSupportedException("Unsupported input type for XML compression.");

            var writerSettings = new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                Indent = false
            };

            using var sw = new StringWriter();
            using var writer = XmlWriter.Create(sw, writerSettings);

            writer.WriteNode(reader, true);
            writer.Flush();

            return sw.ToString();
        }
        catch (ArgumentException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The provided argument is invalid for XML compression.", ex);
        }
        catch (ArgumentNullException ex) when (ex.ParamName is not null && ex.ParamName.Equals("input"))
        {
            throw new InvalidOperationException("The XML input reference is null.", ex);
        }
        catch (FileNotFoundException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("The specified XML file could not be located on disk.", ex);
        }
        catch (IOException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("A low-level I/O failure occurred while accessing the XML source.", ex);
        }
        catch (InvalidOperationException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.ReaderWriter"))
        {
            throw new InvalidOperationException("The XML reader entered an invalid operational state.", ex);
        }
        catch (NotSupportedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The supplied input type is not supported by the XML compression engine.", ex);
        }
        catch (ObjectDisposedException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.CoreLib"))
        {
            throw new InvalidOperationException("The underlying stream or reader was disposed before XML processing completed.", ex);
        }
        catch (UnauthorizedAccessException ex) when (ex.Source is not null && ex.Source.Equals("System.IO.FileSystem"))
        {
            throw new InvalidOperationException("Access to the XML resource was denied by the operating system.", ex);
        }
        catch (UriFormatException ex) when (ex.Source is not null && ex.Source.Equals("System.Private.Uri"))
        {
            throw new InvalidOperationException("The provided URI is not in a valid format for XML loading.", ex);
        }
        catch (XmlException ex) when (ex.Source is not null && ex.Source.Equals("System.Xml.ReaderWriter"))
        {
            throw new InvalidOperationException("The XML payload is malformed or violates XML well-formedness rules.", ex);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("An unexpected failure occurred during XML compression.", ex);
        }
    }
}

/// <summary>
/// Input Argument (Count): 1
/// ( Reference Types )
/// ( Total Methods: 15 )
/// </summary>
public static partial class CompressXmlForFastProcessingExtensions
{
    public static String CompressXmlForFastProcessing(this String source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XDocument source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XElement source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XmlDocument source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this Stream source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this MemoryStream source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this TextReader source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this FileInfo source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this Uri source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this StringBuilder source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XmlReader source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XmlNode source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this TextWriter source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this Byte[] source)
    {
        return CompressInternal(source);
    }

    public static String CompressXmlForFastProcessing(this XmlElement source)
    {
        return CompressInternal(source);
    }
}